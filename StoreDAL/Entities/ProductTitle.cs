namespace StoreDAL.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/// <summary>
/// Represents a product title.
/// </summary>
[Table("product_titles")]
public class ProductTitle : BaseEntity
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ProductTitle"/> class.
    /// </summary>
    public ProductTitle()
        : base()
    {
        this.Title = string.Empty;
        this.CategoryId = 0;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ProductTitle"/> class with the specified ID, title, and category ID.
    /// </summary>
    /// <param name="id">The ID of the product title.</param>
    /// <param name="title">The title of the product.</param>
    /// <param name="categoryId">The ID of the category.</param>
    public ProductTitle(int id, string title, int categoryId)
        : base(id)
    {
        this.Title = title;
        this.CategoryId = categoryId;
    }

    /// <summary>
    /// Gets or sets the title of the product.
    /// </summary>
    [Column("product_title")]
    [Required]
    public string Title { get; set; }

    /// <summary>
    /// Gets or sets the ID of the category.
    /// </summary>
    [Column("category_id"), ForeignKey(nameof(Category))]
    [Required]
    public int CategoryId { get; set; }

    /// <summary>
    /// Gets or sets the category.
    /// </summary>
    public Category? Category { get; set; }

    /// <summary>
    /// Gets the list of products with this title.
    /// </summary>
    public virtual IList<Product>? Products { get; }
}
