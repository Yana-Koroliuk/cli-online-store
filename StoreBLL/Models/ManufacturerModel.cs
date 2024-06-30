namespace StoreBLL.Models;
using StoreDAL.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography.X509Certificates;

/// <summary>
/// Represents a manufacturer model.
/// </summary>
public class ManufacturerModel : AbstractModel
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ManufacturerModel"/> class.
    /// </summary>
    /// <param name="id">The ID of the manufacturer.</param>
    /// <param name="name">The name of the manufacturer.</param>
    public ManufacturerModel(int id, string name)
        : base(id)
    {
        this.Id = id;
        this.Name = name;
    }

    /// <summary>
    /// Gets or sets the name of the manufacturer.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Returns a string that represents the manufacturer model.
    /// </summary>
    /// <returns>A string representation of the manufacturer model.</returns>
    public override string ToString()
    {
        return $"Id: {this.Id}, Name: {this.Name}";
    }
}
