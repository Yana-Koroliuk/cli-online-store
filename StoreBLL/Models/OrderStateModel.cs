namespace StoreBLL.Models;
using System;
using System.Collections.Generic;

/// <summary>
/// Represents an order state model.
/// </summary>
public class OrderStateModel : AbstractModel
{
    /// <summary>
    /// Initializes a new instance of the <see cref="OrderStateModel"/> class.
    /// </summary>
    /// <param name="id">The ID of the order state.</param>
    /// <param name="stateName">The name of the order state.</param>
    public OrderStateModel(int id, string stateName)
        : base(id)
    {
        this.Id = id;
        this.StateName = stateName;
    }

    /// <summary>
    /// Gets or sets the name of the order state.
    /// </summary>
    public string StateName { get; set; }

    /// <summary>
    /// Returns a string representation of the order state model.
    /// </summary>
    /// <returns>A string representing the order state model.</returns>
    public override string ToString()
    {
        return $"Id:{this.Id} {this.StateName}";
    }
}