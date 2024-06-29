namespace StoreDAL.Entities;

using Microsoft.CodeAnalysis;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/// <summary>
/// Represents the details of a customer order.
/// </summary>
[Table("customer_order_details")]
public class OrderDetail : BaseEntity
{
    /// <summary>
    /// Initializes a new instance of the <see cref="OrderDetail"/> class.
    /// </summary>
    public OrderDetail()
        : base()
    {
        this.OrderId = 0;
        this.ProductId = 0;
        this.Price = 0;
        this.ProductAmount = 0;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="OrderDetail"/> class with the specified details.
    /// </summary>
    /// <param name="id">The ID of the order detail.</param>
    /// <param name="orderId">The ID of the customer order.</param>
    /// <param name="productId">The ID of the product.</param>
    /// <param name="price">The price of the product.</param>
    /// <param name="amount">The amount of the product.</param>
    public OrderDetail(int id, int orderId, int productId, decimal price, int amount)
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
    [Column("customer_order_id"), ForeignKey(nameof(CustomerOrder))]
    [Required]
    public int OrderId { get; set; }

    /// <summary>
    /// Gets or sets the ID of the product.
    /// </summary>
    [Column("product_id"), ForeignKey(nameof(Product))]
    [Required]
    public int ProductId { get; set; }

    /// <summary>
    /// Gets or sets the price of the product.
    /// </summary>
    [Column("price")]
    [Required]
    public decimal Price { get; set; }

    /// <summary>
    /// Gets or sets the amount of the product.
    /// </summary>
    [Column("product_amount")]
    [Required]
    public int ProductAmount { get; set; }

    /// <summary>
    /// Gets or sets the customer order.
    /// </summary>
    public CustomerOrder? Order { get; set; }

    /// <summary>
    /// Gets or sets the product.
    /// </summary>
    public Product? Product { get; set; }
}
