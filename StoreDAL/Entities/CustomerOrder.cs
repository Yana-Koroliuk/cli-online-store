namespace StoreDAL.Entities;

using StoreDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Represents a customer order.
/// </summary>
[Table("customer_orders")]
public class CustomerOrder : BaseEntity
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CustomerOrder"/> class.
    /// </summary>
    public CustomerOrder()
     : base()
    {
        this.OperationTime = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture);
        this.UserId = 0;
        this.OrderStateId = 1;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CustomerOrder"/> class with the specified details.
    /// </summary>
    /// <param name="id">The ID of the order.</param>
    /// <param name="operationTime">The operation time of the order.</param>
    /// <param name="userId">The ID of the user who placed the order.</param>
    /// <param name="orderStateId">The ID of the order state.</param>
    public CustomerOrder(int id, string operationTime, int userId, int orderStateId)
        : base(id)
    {
        this.OperationTime = operationTime;
        this.UserId = userId;
        this.OrderStateId = orderStateId;
    }

    /// <summary>
    /// Gets or sets the ID of the user who placed the order.
    /// </summary>
    [Column("customer_id"), ForeignKey(nameof(User))]
    [Required]
    public int UserId { get; set; }

    /// <summary>
    /// Gets or sets the operation time of the order.
    /// </summary>
    [Column("operation_time")]
    [Required]
    public string OperationTime { get; set; }

    /// <summary>
    /// Gets or sets the ID of the order state.
    /// </summary>
    [Column("order_state_id"), ForeignKey(nameof(OrderState))]
    [Required]
    public int OrderStateId { get; set; }

    /// <summary>
    /// Gets or sets the user who placed the order.
    /// </summary>
    public User? User { get; set; }

    /// <summary>
    /// Gets or sets the state of the order.
    /// </summary>
    public OrderState? State { get; set; }

    /// <summary>
    /// Gets the list of order details.
    /// </summary>
    public virtual IList<OrderDetail>? Details { get; }
}