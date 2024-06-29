namespace StoreDAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

/// <summary>
/// Represents a user.
/// </summary>
[Table("users")]
public class User : BaseEntity
{
    /// <summary>
    /// Initializes a new instance of the <see cref="User"/> class.
    /// </summary>
    public User()
        : base()
    {
        this.Name = string.Empty;
        this.LastName = string.Empty;
        this.Login = string.Empty;
        this.Password = string.Empty;
        this.RoleId = 0;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="User"/> class with the specified details.
    /// </summary>
    /// <param name="id">The ID of the user.</param>
    /// <param name="name">The first name of the user.</param>
    /// <param name="lastName">The last name of the user.</param>
    /// <param name="login">The login of the user.</param>
    /// <param name="password">The password of the user.</param>
    /// <param name="roleId">The ID of the user role.</param>
    public User(int id, string name, string lastName, string login, string password, int roleId)
        : base(id)
    {
        this.Name = name;
        this.LastName = lastName;
        this.Login = login;
        this.Password = password;
        this.RoleId = roleId;
    }

    /// <summary>
    /// Gets or sets the first name of the user.
    /// </summary>
    [Column("first_name")]
    [Required]
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the last name of the user.
    /// </summary>
    [Column("last_name")]
    [Required]
    public string LastName { get; set; }

    /// <summary>
    /// Gets or sets the login of the user.
    /// </summary>
    [Column("login")]
    [Required]
    public string Login { get; set; }

    /// <summary>
    /// Gets or sets the password of the user.
    /// </summary>
    [Column("Password")]
    [Required]
    public string Password { get; set; }

    /// <summary>
    /// Gets or sets the ID of the user role.
    /// </summary>
    [Column("user_role_id"), ForeignKey(nameof(UserRole))]
    [Required]
    public int RoleId { get; set; }

    /// <summary>
    /// Gets or sets the user role.
    /// </summary>
    public UserRole? Role { get; set; }

    /// <summary>
    /// Gets the list of orders placed by the user.
    /// </summary>
    public virtual IList<CustomerOrder>? Order { get; }
}
