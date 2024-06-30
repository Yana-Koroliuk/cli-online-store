namespace StoreBLL.Models;
using StoreDAL.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography.X509Certificates;

/// <summary>
/// Represents a category model.
/// </summary>
public class CategoryModel : AbstractModel
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CategoryModel"/> class.
    /// </summary>
    /// <param name="id">The ID of the category.</param>
    /// <param name="name">The name of the category.</param>
    public CategoryModel(int id, string name)
        : base(id)
    {
        this.Id = id;
        this.Name = name;
    }

    /// <summary>
    /// Gets or sets the name of the category.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Returns a string that represents the category model.
    /// </summary>
    /// <returns>A string representation of the category model.</returns>
    public override string ToString()
    {
        return $"Id: {this.Id}, Name: {this.Name}";
    }
}
