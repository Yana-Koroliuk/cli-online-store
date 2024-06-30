namespace StoreBLL.Models;
using System;
using System.Collections.Generic;

/// <summary>
/// Represents a user model.
/// </summary>
public class UserModel : AbstractModel
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UserModel"/> class.
    /// </summary>
    /// <param name="id">The ID of the user.</param>
    /// <param name="name">The name of the user.</param>
    /// <param name="lastName">The last name of the user.</param>
    /// <param name="login">The login of the user.</param>
    /// <param name="password">The password of the user.</param>
    /// <param name="roleId">The role ID of the user.</param>
    public UserModel(int id, string name, string lastName, string login, string password, int roleId)
        : base(id)
    {
        this.Name = name;
        this.LastName = lastName;
        this.Login = login;
        this.Password = password;
        this.RoleId = roleId;
    }

    /// <summary>
    /// Gets or sets the name of the user.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the last name of the user.
    /// </summary>
    public string LastName { get; set; }

    /// <summary>
    /// Gets or sets the login of the user.
    /// </summary>
    public string Login { get; set; }

    /// <summary>
    /// Gets or sets the password of the user.
    /// </summary>
    public string Password { get; set; }

    /// <summary>
    /// Gets or sets the role ID of the user.
    /// </summary>
    public int RoleId { get; set; }

    /// <summary>
    /// Returns a string representation of the user model.
    /// </summary>
    /// <returns>A string representing the user model.</returns>
    public override string ToString()
    {
        return $"Id:{this.Id} Name:{this.Name} LastName:{this.LastName} Login:{this.Login}";
    }
}
