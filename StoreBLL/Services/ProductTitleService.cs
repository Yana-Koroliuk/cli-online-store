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
/// Provides services for managing product titles.
/// </summary>
public class ProductTitleService : IProductTitleService
{
    private readonly IProductTitleRepository repository;

    /// <summary>
    /// Initializes a new instance of the <see cref="ProductTitleService"/> class.
    /// </summary>
    /// <param name="repository">The productTitle repository.</param>
    public ProductTitleService(IProductTitleRepository repository)
    {
        this.repository = repository;
    }

    /// <summary>
    /// Adds a new product title.
    /// </summary>
    /// <param name="model">The product title model to add.</param>
    public void Add(AbstractModel model)
    {
        var productTitleModel = (ProductTitleModel)model;
        var productTitle = new ProductTitle(productTitleModel.Id, productTitleModel.Title, productTitleModel.CategoryId);
        this.repository.Add(productTitle);
    }

    /// <summary>
    /// Deletes a product title by ID.
    /// </summary>
    /// <param name="modelId">The ID of the product title to delete.</param>
    public void Delete(int modelId)
    {
        this.repository.DeleteById(modelId);
    }

    /// <summary>
    /// Gets all product titles.
    /// </summary>
    /// <returns>A collection of product title models.</returns>
    public IEnumerable<AbstractModel> GetAll()
    {
        return this.repository.GetAll().Select(productTitle => new ProductTitleModel(productTitle.Id, productTitle.Title, productTitle.CategoryId));
    }

    /// <summary>
    /// Gets a product title by ID.
    /// </summary>
    /// <param name="id">The ID of the product title to retrieve.</param>
    /// <returns>The product title model.</returns>
    public AbstractModel GetById(int id)
    {
        var productTitle = this.repository.GetById(id);
        return new ProductTitleModel(productTitle.Id, productTitle.Title, productTitle.CategoryId);
    }

    /// <summary>
    /// Gets a product title by name and category ID.
    /// </summary>
    /// <param name="name">The name of the product title.</param>
    /// <param name="categoryId">The ID of the category.</param>
    /// <returns>The product title model if found, otherwise null.</returns>
    public ProductTitleModel? GetByNameAndCategoryId(string name, int categoryId)
    {
        var productTitle = this.repository.GetAll()
            .FirstOrDefault(pt => pt.Title.Equals(name, StringComparison.OrdinalIgnoreCase) && pt.CategoryId == categoryId);

        return productTitle != null ? new ProductTitleModel(productTitle.Id, productTitle.Title, productTitle.CategoryId) : null;
    }

    /// <summary>
    /// Creates a new product title.
    /// </summary>
    /// <param name="name">The name of the product title.</param>
    /// <param name="categoryId">The ID of the category to which the product title belongs.</param>
    /// <returns>The created product title model.</returns>
    public AbstractModel Create(string name, int categoryId)
    {
        var productTitle = new ProductTitle(0, name, categoryId);
        this.repository.Add(productTitle);
        var createdProductTitle = this.repository.GetAll().Last();
        return new ProductTitleModel(createdProductTitle.Id, createdProductTitle.Title, createdProductTitle.CategoryId);
    }

    /// <summary>
    /// Updates a product title.
    /// </summary>
    /// <param name="model">The product title model to update.</param>
    public void Update(AbstractModel model)
    {
        var productTitleModel = (ProductTitleModel)model;
        var productTitle = this.repository.GetById(productTitleModel.Id);
        if (productTitle != null)
        {
            productTitle.Title = productTitleModel.Title;
            productTitle.CategoryId = productTitleModel.CategoryId;
            this.repository.Update(productTitle);
        }
    }
}
