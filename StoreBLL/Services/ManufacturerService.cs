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
/// Provides services for managing manufacturers.
/// </summary>
public class ManufacturerService : ICrud
{
    private readonly IManufacturerRepository repository;

    /// <summary>
    /// Initializes a new instance of the <see cref="ManufacturerService"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public ManufacturerService(StoreDbContext context)
    {
        this.repository = new ManufacturerRepository(context);
    }

    /// <summary>
    /// Adds a new manufacturer.
    /// </summary>
    /// <param name="model">The manufacturer model to add.</param>
    public void Add(AbstractModel model)
    {
        var manufacturerModel = (ManufacturerModel)model;
        var manufacturer = new Manufacturer(manufacturerModel.Id, manufacturerModel.Name);
        this.repository.Add(manufacturer);
    }

    /// <summary>
    /// Deletes a manufacturer by ID.
    /// </summary>
    /// <param name="modelId">The ID of the manufacturer to delete.</param>
    public void Delete(int modelId)
    {
        this.repository.DeleteById(modelId);
    }

    /// <summary>
    /// Gets all manufacturers.
    /// </summary>
    /// <returns>A collection of manufacturer models.</returns>
    public IEnumerable<AbstractModel> GetAll()
    {
        return this.repository.GetAll().Select(manufacturer => new ManufacturerModel(manufacturer.Id, manufacturer.Name));
    }

    /// <summary>
    /// Gets a manufacturer by ID.
    /// </summary>
    /// <param name="id">The ID of the manufacturer to retrieve.</param>
    /// <returns>The manufacturer model.</returns>
    public AbstractModel GetById(int id)
    {
        var manufacturer = this.repository.GetById(id);
        return new ManufacturerModel(manufacturer.Id, manufacturer.Name);
    }

    /// <summary>
    /// Updates a manufacturer.
    /// </summary>
    /// <param name="model">The manufacturer model to update.</param>
    public void Update(AbstractModel model)
    {
        var manufacturerModel = (ManufacturerModel)model;
        var manufacturer = this.repository.GetById(manufacturerModel.Id);
        if (manufacturer != null)
        {
            manufacturer.Name = manufacturerModel.Name;
            this.repository.Update(manufacturer);
        }
    }
}