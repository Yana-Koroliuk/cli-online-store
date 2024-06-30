namespace StoreBLL.Models;
using System;
using System.Collections.Generic;

/// <summary>
/// Represents a customer order model.
/// </summary>
public class CustomerOrderModel : AbstractModel
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CustomerOrderModel"/> class.
    /// </summary>
    /// <param name="id">The ID of the order.</param>
    /// <param name="operationTime">The operation time of the order.</param>
    /// <param name="userId">The ID of the user who placed the order.</param>
    /// <param name="orderStateId">The ID of the order state.</param>
    public CustomerOrderModel(int id, string operationTime, int userId, int orderStateId)
        : base(id)
    {
        this.OperationTime = operationTime;
        this.UserId = userId;
        this.OrderStateId = orderStateId;
    }

    /// <summary>
    /// Gets or sets the ID of the user who placed the order.
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// Gets or sets the operation time of the order.
    /// </summary>
    public string OperationTime { get; set; }

    /// <summary>
    /// Gets or sets the ID of the order state.
    /// </summary>
    public int OrderStateId { get; set; }

    /// <summary>
    /// Returns a string representation of the customer order model.
    /// </summary>
    /// <returns>A string representing the customer order model.</returns>
    public override string ToString()
    {
        return $"Id:{this.Id} UserId:{this.UserId} OperationTime:{this.OperationTime} OrderStateId:{this.OrderStateId}";
    }
}