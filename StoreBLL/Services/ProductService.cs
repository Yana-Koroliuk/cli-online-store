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
/// Provides services for managing products.
/// </summary>
public class ProductService : ICrud
{
    private readonly IProductRepository repository;

    /// <summary>
    /// Initializes a new instance of the <see cref="ProductService"/> class.
    /// </summary>
    /// <param name="repository">The product repository.</param>
    public ProductService(IProductRepository repository)
    {
        this.repository = repository;
    }

    /// <summary>
    /// Adds a new product.
    /// </summary>
    /// <param name="model">The product model to add.</param>
    public void Add(AbstractModel model)
    {
        var productModel = (ProductModel)model;
        var product = new Product(productModel.Id, productModel.TitleId, productModel.ManufacturerId, productModel.Description, productModel.UnitPrice);
        this.repository.Add(product);
    }

    /// <summary>
    /// Deletes a product by ID.
    /// </summary>
    /// <param name="modelId">The ID of the product to delete.</param>
    public void Delete(int modelId)
    {
        this.repository.DeleteById(modelId);
    }

    /// <summary>
    /// Gets all products.
    /// </summary>
    /// <returns>A collection of product models.</returns>
    public IEnumerable<AbstractModel> GetAll()
    {
        return this.repository.GetAll().Select(x => new ProductModel(x.Id, x.TitleId, x.ManufacturerId, x.Description, x.UnitPrice));
    }

    /// <summary>
    /// Gets a product by ID.
    /// </summary>
    /// <param name="id">The ID of the product to retrieve.</param>
    /// <returns>The product model.</returns>
    public AbstractModel GetById(int id)
    {
        var res = this.repository.GetById(id);
        return new ProductModel(res.Id, res.TitleId, res.ManufacturerId, res.Description, res.UnitPrice);
    }

    /// <summary>
    /// Updates a product.
    /// </summary>
    /// <param name="model">The product model to update.</param>
    public void Update(AbstractModel model)
    {
        var productModel = (ProductModel)model;
        var product = this.repository.GetById(productModel.Id);
        if (product != null)
        {
            product.TitleId = productModel.TitleId;
            product.ManufacturerId = productModel.ManufacturerId;
            product.Description = productModel.Description;
            product.UnitPrice = productModel.UnitPrice;
            this.repository.Update(product);
        }
    }
}
