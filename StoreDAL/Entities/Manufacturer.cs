namespace StoreDAL.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using StoreDAL.Entities;

/// <summary>
/// Represents a product manufacturer.
/// </summary>
[Table("manufacturers")]
public class Manufacturer : BaseEntity
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Manufacturer"/> class.
    /// </summary>
    public Manufacturer()
        : base()
    {
        this.Name = string.Empty;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Manufacturer"/> class with the specified ID and name.
    /// </summary>
    /// <param name="id">The ID of the manufacturer.</param>
    /// <param name="name">The name of the manufacturer.</param>
    public Manufacturer(int id, string name)
        : base(id)
    {
        this.Name = name;
    }

    /// <summary>
    /// Gets or sets the name of the manufacturer.
    /// </summary>
    [Column("manufacturer_name")]
    [Required]
    public string Name { get; set; }
}
