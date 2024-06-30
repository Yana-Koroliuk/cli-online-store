namespace StoreBLL.Models;
using System;
using System.Collections.Generic;

/// <summary>
/// Represents an order detail model.
/// </summary>
public class OrderDetailModel : AbstractModel
{
    /// <summary>
    /// Initializes a new instance of the <see cref="OrderDetailModel"/> class.
    /// </summary>
    /// <param name="id">The ID of the order detail.</param>
    /// <param name="orderId">The ID of the customer order.</param>
    /// <param name="productId">The ID of the product.</param>
    /// <param name="price">The price of the product.</param>
    /// <param name="amount">The amount of the product.</param>
    public OrderDetailModel(int id, int orderId, int productId, decimal price, int amount)
        : base(id)
    {
        this.OrderId = orderId;
        this.ProductId = productId;
        this.Price = price;
        this.ProductAmount = amount;
    }

    /// <summary>
    /// Gets or sets the ID of the customer order.
    /// </summary>
    public int OrderId { get; set; }

    /// <summary>
    /// Gets or sets the ID of the product.
    /// </summary>
    public int ProductId { get; set; }

    /// <summary>
    /// Gets or sets the price of the product.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Gets or sets the amount of the product.
    /// </summary>
    public int ProductAmount { get; set; }

    /// <summary>
    /// Returns a string representation of the order detail model.
    /// </summary>
    /// <returns>A string representing the order detail model.</returns>
    public override string ToString()
    {
        return $"Id:{this.Id} OrderId:{this.OrderId} ProductId:{this.ProductId} Price:{this.Price} Amount:{this.ProductAmount}";
    }
}