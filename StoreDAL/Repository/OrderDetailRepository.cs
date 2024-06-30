using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StoreDAL.Data;
using StoreDAL.Entities;
using StoreDAL.Interfaces;

namespace StoreDAL.Repository
{
    /// <summary>
    /// Represents a repository for managing order details.
    /// </summary>
    public class OrderDetailRepository : AbstractRepository, IOrderDetailRepository
    {
        private readonly DbSet<OrderDetail> dbSet;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderDetailRepository"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        public OrderDetailRepository(StoreDbContext context)
            : base(context)
        {
            ArgumentNullException.ThrowIfNull(context);
            this.dbSet = context.Set<OrderDetail>();
        }

        /// <summary>
        /// Adds a new order detail.
        /// </summary>
        /// <param name="entity">The order detail entity to add.</param>
        public void Add(OrderDetail entity)
        {
            this.dbSet.Add(entity);
            this.context.SaveChanges();
        }

        /// <summary>
        /// Deletes an order detail.
        /// </summary>
        /// <param name="entity">The order detail entity to delete.</param>
        public void Delete(OrderDetail entity)
        {
            this.dbSet.Remove(entity);
            this.context.SaveChanges();
        }

        /// <summary>
        /// Deletes an order detail by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the order detail to delete.</param>
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
        /// Gets all order details.
        /// </summary>
        /// <returns>A collection of all order details.</returns>
        public IEnumerable<OrderDetail> GetAll()
        {
            return this.dbSet.ToList();
        }

        /// <summary>
        /// Gets a paginated list of order details.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="rowCount">The number of rows per page.</param>
        /// <returns>A collection of order details.</returns>
        public IEnumerable<OrderDetail> GetAll(int pageNumber, int rowCount)
        {
            return this.dbSet.Skip((pageNumber - 1) * rowCount).Take(rowCount).ToList();
        }

        /// <summary>
        /// Gets an order detail by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the order detail to retrieve.</param>
        /// <returns>The order detail with the specified identifier.</returns>
        public OrderDetail GetById(int id)
        {
            return this.dbSet.Find(id) ?? throw new InvalidOperationException("Order detail not found.");
        }

        /// <summary>
        /// Updates an order detail.
        /// </summary>
        /// <param name="entity">The order detail entity to update.</param>
        public void Update(OrderDetail entity)
        {
            this.dbSet.Update(entity);
            this.context.SaveChanges();
        }
    }
}
