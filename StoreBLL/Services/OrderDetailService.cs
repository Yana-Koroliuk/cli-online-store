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
/// Provides services for managing order details.
/// </summary>
public class OrderDetailService : ICrud
{
    private readonly IOrderDetailRepository repository;

    /// <summary>
    /// Initializes a new instance of the <see cref="OrderDetailService"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public OrderDetailService(StoreDbContext context)
    {
        this.repository = new OrderDetailRepository(context);
    }

    /// <summary>
    /// Adds a new order detail.
    /// </summary>
    /// <param name="model">The order detail model to add.</param>
    public void Add(AbstractModel model)
    {
        var orderDetailModel = (OrderDetailModel)model;
        var orderDetail = new OrderDetail(orderDetailModel.Id, orderDetailModel.OrderId, orderDetailModel.ProductId, orderDetailModel.Price, orderDetailModel.ProductAmount);
        this.repository.Add(orderDetail);
    }

    /// <summary>
    /// Deletes an order detail by ID.
    /// </summary>
    /// <param name="modelId">The ID of the order detail to delete.</param>
    public void Delete(int modelId)
    {
        this.repository.DeleteById(modelId);
    }

    /// <summary>
    /// Gets all order details.
    /// </summary>
    /// <returns>A collection of order detail models.</returns>
    public IEnumerable<AbstractModel> GetAll()
    {
        return this.repository.GetAll().Select(x => new OrderDetailModel(x.Id, x.OrderId, x.ProductId, x.Price, x.ProductAmount));
    }

    /// <summary>
    /// Gets an order detail by ID.
    /// </summary>
    /// <param name="id">The ID of the order detail to retrieve.</param>
    /// <returns>The order detail model.</returns>
    public AbstractModel GetById(int id)
    {
        var res = this.repository.GetById(id);
        return new OrderDetailModel(res.Id, res.OrderId, res.ProductId, res.Price, res.ProductAmount);
    }

    /// <summary>
    /// Updates an order detail.
    /// </summary>
    /// <param name="model">The order detail model to update.</param>
    public void Update(AbstractModel model)
    {
        var orderDetailModel = (OrderDetailModel)model;
        var orderDetail = this.repository.GetById(orderDetailModel.Id);
        if (orderDetail != null)
        {
            orderDetail.OrderId = orderDetailModel.OrderId;
            orderDetail.ProductId = orderDetailModel.ProductId;
            orderDetail.Price = orderDetailModel.Price;
            orderDetail.ProductAmount = orderDetailModel.ProductAmount;
            this.repository.Update(orderDetail);
        }
    }
}