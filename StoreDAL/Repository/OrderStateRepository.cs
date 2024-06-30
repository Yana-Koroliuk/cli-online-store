namespace StoreDAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StoreDAL.Data;
using StoreDAL.Entities;
using StoreDAL.Interfaces;

public class OrderStateRepository : AbstractRepository, IOrderStateRepository
{
    /// <summary>
    /// Represents the repository for managing <see cref="OrderState"/> entities.
    /// </summary>
    private readonly DbSet<OrderState> dbSet;

    /// <summary>
    /// Initializes a new instance of the <see cref="OrderStateRepository"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public OrderStateRepository(StoreDbContext context)
        : base(context)
    {
        ArgumentNullException.ThrowIfNull(context);
        this.dbSet = context.Set<OrderState>();
    }

    /// <summary>
    /// Adds a new order state entity.
    /// </summary>
    /// <param name="entity">The order state entity to add.</param>
    public void Add(OrderState entity)
    {
        this.dbSet.Add(entity);
        this.context.SaveChanges();
    }

    /// <summary>
    /// Deletes an order state entity.
    /// </summary>
    /// <param name="entity">The order state entsity to delete.</param>
    public void Delete(OrderState entity)
    {
        this.dbSet.Remove(entity);
        this.context.SaveChanges();
    }

    /// <summary>
    /// Deletes an order state entity by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the order state entity to delete.</param>
    public void DeleteById(int id)
    {
        var entity = this.dbSet.Find(id);
        if (entity != null)
        {
            this.dbSet.Remove(entity);
            this.context.SaveChanges();
        }
    }

    /// <summary>
    /// Gets all order state entities.
    /// </summary>
    /// <returns>An enumerable collection of order state entities.</returns>
    public IEnumerable<OrderState> GetAll()
    {
        return this.dbSet.ToList();
    }

    /// <summary>
    /// Gets a paginated list of order state entities.
    /// </summary>
    /// <param name="pageNumber">The page number.</param>
    /// <param name="rowCount">The number of rows per page.</param>
    /// <returns>An enumerable collection of order state entities.</returns>
    public IEnumerable<OrderState> GetAll(int pageNumber, int rowCount)
    {
        return this.dbSet.Skip((pageNumber - 1) * rowCount).Take(rowCount).ToList();
    }

    /// <summary>
    /// Gets an order state entity by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the order state entity.</param>
    /// <returns>The order state entity with the specified identifier.</returns>
    public OrderState GetById(int id)
    {
        return this.dbSet.Find(id) ?? throw new InvalidOperationException("Order state not found.");
    }

    /// <summary>
    /// Updates an order state entity.
    /// </summary>
    /// <param name="entity">The order state entity to update.</param>
    public void Update(OrderState entity)
    {
        this.dbSet.Update(entity);
        this.context.SaveChanges();
    }
}