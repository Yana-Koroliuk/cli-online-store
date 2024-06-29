namespace StoreDAL.Entities;
using StoreDAL.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/// <summary>
/// Represents a product category.
/// </summary>
[Table("categories")]
public class Category : BaseEntity
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Category"/> class.
    /// </summary>
    public Category()
        : base()
    {
        this.Name = string.Empty;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Category"/> class with the specified ID and name.
    /// </summary>
    /// <param name="id">The ID of the category.</param>
    /// <param name="name">The name of the category.</param>
    public Category(int id, string name)
        : base(id)
    {
        this.Name = name;
    }

    /// <summary>
    /// Gets or sets the name of the category.
    /// </summary>
    [Column("category_name")]
    [Required]
    public string Name { get; set; }

    /// <summary>
    /// Gets the list of product titles in the category.
    /// </summary>
    public virtual IList<ProductTitle>? Titles { get; }
}
