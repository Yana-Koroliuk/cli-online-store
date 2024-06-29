namespace StoreDAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Represents the base entity with common properties.
/// </summary>
public abstract class BaseEntity
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BaseEntity"/> class with the specified ID.
    /// </summary>
    /// <param name="id">The ID of the entity.</param>
    protected BaseEntity(int id)
    {
        this.Id = id;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BaseEntity"/> class.
    /// </summary>
    protected BaseEntity()
    {
        this.Id = 0;
    }

    /// <summary>
    /// Gets or sets the ID of the entity.
    /// </summary>
    [Column("id")]
    public int Id { get; set; }
}
