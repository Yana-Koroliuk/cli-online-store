namespace StoreBLL.Models;
using System;
using System.Collections.Generic;

/// <summary>
/// Represents a product model.
/// </summary>
public class ProductModel : AbstractModel
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ProductModel"/> class.
    /// </summary>
    /// <param name="id">The ID of the product.</param>
    /// <param name="titleId">The title ID of the product.</param>
    /// <param name="manufacturerId">The manufacturer ID of the product.</param>
    /// <param name="description">The description of the product.</param>
    /// <param name="unitPrice">The unit price of the product.</param>
    public ProductModel(int id, int titleId, int? manufacturerId, string description, decimal unitPrice)
        : base(id)
    {
        this.TitleId = titleId;
        this.ManufacturerId = manufacturerId;
        this.Description = description;
        this.UnitPrice = unitPrice;
    }

    /// <summary>
    /// Gets or sets the title ID of the product.
    /// </summary>
    public int TitleId { get; set; }

    /// <summary>
    /// Gets or sets the manufacturer ID of the product.
    /// </summary>
    public int? ManufacturerId { get; set; }

    /// <summary>
    /// Gets or sets the description of the product.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Gets or sets the unit price of the product.
    /// </summary>
    public decimal UnitPrice { get; set; }

    /// <summary>
    /// Returns a string representation of the product model.
    /// </summary>
    /// <returns>A string representing the product model.</returns>
    public override string ToString()
    {
        return $"Id:{this.Id} Description: {this.Description}, UnitPrice: ${this.UnitPrice}";
    }
}
