namespace StoreDAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Represents a product.
/// </summary>
[Table("products")]
public class Product : BaseEntity
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Product"/> class.
    /// </summary>
    public Product()
        : base()
    {
        this.TitleId = 0;
        this.ManufacturerId = 0;
        this.Description = string.Empty;
        this.UnitPrice = 0;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Product"/> class with the specified details.
    /// </summary>
    /// <param name="id">The ID of the product.</param>
    /// <param name="titleId">The ID of the product title.</param>
    /// <param name="manufacturerId">The ID of the manufacturer.</param>
    /// <param name="description">The description of the product.</param>
    /// <param name="price">The price of the product.</param>
    public Product(int id, int titleId, int manufacturerId, string description, decimal price)
        : base(id)
    {
        this.TitleId = titleId;
        this.ManufacturerId = manufacturerId;
        this.Description = description;
        this.UnitPrice = price;
    }

    /// <summary>
    /// Gets or sets the ID of the product title.
    /// </summary>
    [Column("product_title_id"), ForeignKey(nameof(ProductTitle))]
    [Required]
    public int TitleId { get; set; }

    /// <summary>
    /// Gets or sets the ID of the manufacturer.
    /// </summary>
    [Column("manufacturer_id"), ForeignKey(nameof(Manufacturer))]
    [Required]
    public int ManufacturerId { get; set; }

    /// <summary>
    /// Gets or sets the unit price of the product.
    /// </summary>
    [Column("unit_price")]
    [Required]
    public decimal UnitPrice { get; set; }

    /// <summary>
    /// Gets or sets the description of the product.
    /// </summary>
    [Column("comment")]
    [Required]
    public string Description { get; set; }

    /// <summary>
    /// Gets or sets the product title.
    /// </summary>
    public ProductTitle? Title { get; set; }

    /// <summary>
    /// Gets or sets the manufacturer.
    /// </summary>
    public Manufacturer? Manufacturer { get; set; }

    /// <summary>
    /// Gets the list of order details for this product.
    /// </summary>
    public virtual IList<OrderDetail>? OrderDetails { get; }
}