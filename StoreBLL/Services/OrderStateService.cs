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
/// Provides services for managing order states.
/// </summary>
public class OrderStateService : ICrud
{
    private readonly IOrderStateRepository repository;

    /// <summary>
    /// Initializes a new instance of the <see cref="OrderStateService"/> class.
    /// </summary>
    /// <param name="repository">The  orderState repository.</param>
    public OrderStateService(IOrderStateRepository repository)
    {
        this.repository = repository;
    }

    /// <summary>
    /// Adds a new order state.
    /// </summary>
    /// <param name="model">The order state model to add.</param>
    public void Add(AbstractModel model)
    {
        var x = (OrderStateModel)model;
        this.repository.Add(new OrderState(x.Id, x.StateName));
    }

    /// <summary>
    /// Deletes an order state by ID.
    /// </summary>
    /// <param name="modelId">The ID of the order state to delete.</param>
    public void Delete(int modelId)
    {
        this.repository.DeleteById(modelId);
    }

    /// <summary>
    /// Gets all order states.
    /// </summary>
    /// <returns>A collection of order state models.</returns>
    public IEnumerable<AbstractModel> GetAll()
    {
        return this.repository.GetAll().Select(x => new OrderStateModel(x.Id, x.StateName));
    }

    /// <summary>
    /// Gets an order state by ID.
    /// </summary>
    /// <param name="id">The ID of the order state to retrieve.</param>
    /// <returns>The order state model.</returns>
    public AbstractModel GetById(int id)
    {
        var res = this.repository.GetById(id);
        return new OrderStateModel(res.Id, res.StateName);
    }

    /// <summary>
    /// Retrieves the allowed to change status IDs for a given current status ID.
    /// </summary>
    /// <param name="currentStatusId">The current status ID.</param>
    /// <returns>A list of allowed status IDs.</returns>
    public List<int> GetChangeToStatusIds(int currentStatusId)
    {
        var allowedStatusIds = new List<int>();
        var stateTransitions = new Dictionary<int, int>
            {
                { 1, 4 },
                { 4, 5 },
                { 5, 6 },
                { 6, 7 },
            };
        var allStates = this.repository.GetAll().ToList();
        var cancelledState = allStates.Find(os =>
        os.StateName.Equals("Cancelled by administrator", StringComparison.OrdinalIgnoreCase));

        if (stateTransitions.TryGetValue(currentStatusId, out var nextStatusId))
        {
            allowedStatusIds.Add(nextStatusId);
        }

        if (cancelledState != null && currentStatusId != 8)
        {
            allowedStatusIds.Add(cancelledState.Id);
        }

        return allowedStatusIds;
    }

    /// <summary>
    /// Updates an order state.
    /// </summary>
    /// <param name="model">The order state model to update.</param>
    public void Update(AbstractModel model)
    {
        var x = (OrderStateModel)model;
        var orderState = this.repository.GetById(x.Id);
        if (orderState != null)
        {
            orderState.StateName = x.StateName;
            this.repository.Update(orderState);
        }
    }
}