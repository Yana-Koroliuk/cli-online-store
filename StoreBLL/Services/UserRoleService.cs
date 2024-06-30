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
/// Provides services for managing user roles.
/// </summary>
public class UserRoleService : ICrud
{
    private readonly IUserRoleRepository repository;

    /// <summary>
    /// Initializes a new instance of the <see cref="UserRoleService"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public UserRoleService(StoreDbContext context)
    {
        this.repository = new UserRoleRepository(context);
    }

    /// <summary>
    /// Adds a new user role.
    /// </summary>
    /// <param name="model">The user role model to add.</param>
    public void Add(AbstractModel model)
    {
        var x = (UserRoleModel)model;
        this.repository.Add(new UserRole(x.Id, x.RoleName));
    }

    /// <summary>
    /// Deletes a user role by ID.
    /// </summary>
    /// <param name="modelId">The ID of the user role to delete.</param>
    public void Delete(int modelId)
    {
        this.repository.DeleteById(modelId);
    }

    /// <summary>
    /// Gets all user roles.
    /// </summary>
    /// <returns>A collection of user role models.</returns>
    public IEnumerable<AbstractModel> GetAll()
    {
        return this.repository.GetAll().Select(x => new UserRoleModel(x.Id, x.RoleName));
    }

    /// <summary>
    /// Gets a user role by ID.
    /// </summary>
    /// <param name="id">The ID of the user role to retrieve.</param>
    /// <returns>The user role model.</returns>
    public AbstractModel GetById(int id)
    {
        var res = this.repository.GetById(id);
        return new UserRoleModel(res.Id, res.RoleName);
    }

    /// <summary>
    /// Updates a user role.
    /// </summary>
    /// <param name="model">The user role model to update.</param>
    public void Update(AbstractModel model)
    {
        var x = (UserRoleModel)model;
        var userRole = this.repository.GetById(x.Id);
        if (userRole != null)
        {
            userRole.RoleName = x.RoleName;
            this.repository.Update(userRole);
        }
    }
}
