namespace StoreDAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Represents the state of a customer order.
/// </summary>
[Table("order_states")]
public class OrderState : BaseEntity
{
    /// <summary>
    /// Initializes a new instance of the <see cref="OrderState"/> class.
    /// </summary>
    public OrderState()
        : base()
    {
        this.StateName = string.Empty;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="OrderState"/> class with the specified ID and state name.
    /// </summary>
    /// <param name="id">The ID of the order state.</param>
    /// <param name="stateName">The name of the order state.</param>
    public OrderState(int id, string stateName)
        : base(id)
    {
        this.StateName = stateName;
    }

    /// <summary>
    /// Gets or sets the name of the order state.
    /// </summary>
    [Column("state_name")]
    [Required]
    public string StateName { get; set; }

    /// <summary>
    /// Gets the list of customer orders in this state.
    /// </summary>
    public virtual IList<CustomerOrder>? Order { get; }
}
