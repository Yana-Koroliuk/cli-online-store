namespace StoreBLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreBLL.Interfaces;
using StoreBLL.Models;
using StoreDAL.Data;
using StoreDAL.Entities;
using StoreDAL.Interfaces;
using StoreDAL.Repository;

/// <summary>
/// Provides services for managing users.
/// </summary>
public class UserService : ICrud
{
    private readonly IUserRepository repository;

    /// <summary>
    /// Initializes a new instance of the <see cref="UserService"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public UserService(StoreDbContext context)
    {
        this.repository = new UserRepository(context);
    }

    /// <summary>
    /// Adds a new user.
    /// </summary>
    /// <param name="model">The user model to add.</param>
    public void Add(AbstractModel model)
    {
        var userModel = (UserModel)model;
        var user = new User(userModel.Id, userModel.Name, userModel.LastName, userModel.Login, userModel.Password, userModel.RoleId);
        this.repository.Add(user);
    }

    /// <summary>
    /// Deletes a user by ID.
    /// </summary>
    /// <param name="modelId">The ID of the user to delete.</param>
    public void Delete(int modelId)
    {
        this.repository.DeleteById(modelId);
    }

    /// <summary>
    /// Gets all users.
    /// </summary>
    /// <returns>A collection of user models.</returns>
    public IEnumerable<AbstractModel> GetAll()
    {
        return this.repository.GetAll().Select(user => new UserModel(user.Id, user.Name, user.LastName, user.Login, user.Password, user.RoleId));
    }

    /// <summary>
    /// Gets a user by ID.
    /// </summary>
    /// <param name="id">The ID of the user to retrieve.</param>
    /// <returns>The user model.</returns>
    public AbstractModel GetById(int id)
    {
        var user = this.repository.GetById(id);
        return new UserModel(user.Id, user.Name, user.LastName, user.Login, user.Password, user.RoleId);
    }

    /// <summary>
    /// Authenticates a user by login and password.
    /// </summary>
    /// <param name="login">The login of the user.</param>
    /// <param name="password">The password of the user.</param>
    /// <returns>The authenticated user model.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the login or password is invalid.</exception>
    public UserModel Authenticate(string login, string password)
    {
        var user = this.repository.GetByLogin(login);
        if (user != null && BCrypt.Net.BCrypt.Verify(password, user.Password))
        {
            return new UserModel(user.Id, user.Name, user.LastName, user.Login, user.Password, user.RoleId);
        }

        throw new InvalidOperationException("Invalid login or password.");
    }

    /// <summary>
    /// Updates a user.
    /// </summary>
    /// <param name="model">The user model to update.</param>
    public void Update(AbstractModel model)
    {
        var userModel = (UserModel)model;
        var user = this.repository.GetById(userModel.Id);
        if (user != null)
        {
            user.Name = userModel.Name;
            user.LastName = userModel.LastName;
            user.Login = userModel.Login;
            user.Password = userModel.Password;
            user.RoleId = userModel.RoleId;
            this.repository.Update(user);
        }
    }
}
