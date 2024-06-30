namespace StoreBLL.Models;
using System;
using System.Collections.Generic;

/// <summary>
/// Represents a user role model.
/// </summary>
public class UserRoleModel : AbstractModel
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UserRoleModel"/> class.
    /// </summary>
    /// <param name="id">The ID of the user role.</param>
    /// <param name="roleName">The name of the user role.</param>
    public UserRoleModel(int id, string roleName)
        : base(id)
    {
        this.Id = id;
        this.RoleName = roleName;
    }

    /// <summary>
    /// Gets or sets the name of the user role.
    /// </summary>
    public string RoleName { get; set; }

    /// <summary>
    /// Returns a string representation of the user role model.
    /// </summary>
    /// <returns>A string representing the user role model.</returns>
    public override string ToString()
    {
        return $"Id:{this.Id} {this.RoleName}";
    }
}
