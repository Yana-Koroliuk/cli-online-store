namespace StoreBLL.Models;
using System;
using System.Collections.Generic;

/// <summary>
/// Represents a product title model.
/// </summary>
public class ProductTitleModel : AbstractModel
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ProductTitleModel"/> class.
    /// </summary>
    /// <param name="id">The ID of the product title.</param>
    /// <param name="title">The title of the product.</param>
    /// <param name="categoryId">The category ID of the product title.</param>
    public ProductTitleModel(int id, string title, int categoryId)
        : base(id)
    {
        this.Id = id;
        this.Title = title;
        this.CategoryId = categoryId;
    }

    /// <summary>
    /// Gets or sets the title of the product.
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Gets or sets the category ID of the product title.
    /// </summary>
    public int CategoryId { get; set; }

    /// <summary>
    /// Returns a string that represents the product title model.
    /// </summary>
    /// <returns>A string representation of the product title model.</returns>
    public override string ToString()
    {
        return $"Id: {this.Id}, Title: {this.Title}, CategoryId: {this.CategoryId}";
    }
}