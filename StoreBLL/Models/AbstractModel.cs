namespace StoreBLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Represents an abstract model with an ID property.
/// </summary>
public abstract class AbstractModel
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AbstractModel"/> class.
    /// </summary>
    /// <param name="id">The ID of the model.</param>
    protected AbstractModel(int id)
    {
        this.Id = id;
    }

    /// <summary>
    /// Gets or sets the ID of the model.
    /// </summary>
    public int Id { get; set; }
}
