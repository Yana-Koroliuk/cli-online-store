namespace StoreDAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Represents a user role.
/// </summary>
[Table("user_roles")]
public class UserRole : BaseEntity
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UserRole"/> class.
    /// </summary>
    public UserRole()
    {
        this.RoleName = string.Empty;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="UserRole"/> class with the specified ID and role name.
    /// </summary>
    /// <param name="id">The ID of the user role.</param>
    /// <param name="roleName">The name of the user role.</param>
    public UserRole(int id, string roleName)
        : base(id)
    {
        this.RoleName = roleName;
    }

    /// <summary>
    /// Gets or sets the name of the user role.
    /// </summary>
    [Column("user_role_name")]
    [Required]
    public string RoleName { get; set; }

    /// <summary>
    /// Gets the list of users with this role.
    /// </summary>
    public virtual IList<User>? User { get; }
}
