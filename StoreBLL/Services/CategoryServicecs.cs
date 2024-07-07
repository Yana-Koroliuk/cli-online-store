namespace StoreBLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreBLL.Interfaces;
using StoreBLL.Models;
using StoreDAL.Data;
using StoreDAL.Entities;
using StoreDAL.Interfaces;
using StoreDAL.Repository;

/// <summary>
/// Provides services for managing categories.
/// </summary>
public class CategoryService : IExtendedCrud
{
    private readonly ICategoryRepository repository;

    /// <summary>
    /// Initializes a new instance of the <see cref="CategoryService"/> class.
    /// </summary>
    /// <param name="repository">The category repository.</param>
    public CategoryService(ICategoryRepository repository)
    {
        this.repository = repository;
    }

    /// <summary>
    /// Adds a new category.
    /// </summary>
    /// <param name="model">The category model to add.</param>
    public void Add(AbstractModel model)
    {
        var categoryModel = (CategoryModel)model;
        var category = new Category(categoryModel.Id, categoryModel.Name);
        this.repository.Add(category);
    }

    /// <summary>
    /// Deletes a category by Id.
    /// </summary>
    /// <param name="modelId">The ID of the category to delete.</param>
    public void Delete(int modelId)
    {
        this.repository.DeleteById(modelId);
    }

    /// <summary>
    /// Gets all categories.
    /// </summary>
    /// <returns>A collection of category models.</returns>
    public IEnumerable<AbstractModel> GetAll()
    {
        return this.repository.GetAll().Select(category => new CategoryModel(category.Id, category.Name));
    }

    /// <summary>
    /// Gets a category by Id.
    /// </summary>
    /// <param name="id">The ID of the category to retrieve.</param>
    /// <returns>The category model.</returns>
    public AbstractModel GetById(int id)
    {
        var category = this.repository.GetById(id);
        return new CategoryModel(category.Id, category.Name);
    }

    /// <summary>
    /// Gets a category by its name.
    /// </summary>
    /// <param name="name">The name of the category to retrieve.</param>
    /// <returns>The category model.</returns>
    public AbstractModel? GetByName(string name)
    {
        var category = this.repository.GetAll()
            .FirstOrDefault(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        return category == null ? null : new CategoryModel(category.Id, category.Name);
    }

    /// <summary>
    /// Creates a new category.
    /// </summary>
    /// <param name="name">The name of the category to create.</param>
    /// <returns>The created category model.</returns>
    public AbstractModel Create(string name)
    {
        var newCategory = new Category(0, name);
        this.repository.Add(newCategory);
        var createdCategory = this.repository.GetAll().Last();
        return new CategoryModel(createdCategory.Id, createdCategory.Name);
    }

    /// <summary>
    /// Updates a category.
    /// </summary>
    /// <param name="model">The category model to update.</param>
    public void Update(AbstractModel model)
    {
        var categoryModel = (CategoryModel)model;
        var category = this.repository.GetById(categoryModel.Id);
        if (category != null)
        {
            category.Name = categoryModel.Name;
            this.repository.Update(category);
        }
    }
}
