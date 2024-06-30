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
/// Provides services for managing customer orders.
/// </summary>
public class CustomerOrderService : ICrud
{
    private readonly ICustomerOrderRepository repository;

    /// <summary>
    /// Initializes a new instance of the <see cref="CustomerOrderService"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public CustomerOrderService(StoreDbContext context)
    {
        this.repository = new CustomerOrderRepository(context);
    }

    /// <summary>
    /// Adds a new customer order.
    /// </summary>
    /// <param name="model">The customer order model to add.</param>
    public void Add(AbstractModel model)
    {
        var customerOrderModel = (CustomerOrderModel)model;
        var customerOrder = new CustomerOrder(customerOrderModel.Id, customerOrderModel.OperationTime, customerOrderModel.UserId, customerOrderModel.OrderStateId);
        this.repository.Add(customerOrder);
    }

    /// <summary>
    /// Deletes a customer order by ID.
    /// </summary>
    /// <param name="modelId">The ID of the customer order to delete.</param>
    public void Delete(int modelId)
    {
        this.repository.DeleteById(modelId);
    }

    /// <summary>
    /// Gets all customer orders.
    /// </summary>
    /// <returns>A collection of customer order models.</returns>
    public IEnumerable<AbstractModel> GetAll()
    {
        return this.repository.GetAll().Select(x => new CustomerOrderModel(x.Id, x.OperationTime, x.UserId, x.OrderStateId));
    }

    /// <summary>
    /// Gets a customer order by ID.
    /// </summary>
    /// <param name="id">The ID of the customer order to retrieve.</param>
    /// <returns>The customer order model.</returns>
    public AbstractModel GetById(int id)
    {
        var res = this.repository.GetById(id);
        return new CustomerOrderModel(res.Id, res.OperationTime, res.UserId, res.OrderStateId);
    }

    /// <summary>
    /// Updates a customer order.
    /// </summary>
    /// <param name="model">The customer order model to update.</param>
    public void Update(AbstractModel model)
    {
        var customerOrderModel = (CustomerOrderModel)model;
        var customerOrder = this.repository.GetById(customerOrderModel.Id);
        if (customerOrder != null)
        {
            customerOrder.OperationTime = customerOrderModel.OperationTime;
            customerOrder.UserId = customerOrderModel.UserId;
            customerOrder.OrderStateId = customerOrderModel.OrderStateId;
            this.repository.Update(customerOrder);
        }
    }
}