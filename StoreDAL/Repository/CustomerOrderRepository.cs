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
    /// Represents a repository for managing customer orders.
    /// </summary>
    public class CustomerOrderRepository : AbstractRepository, ICustomerOrderRepository
    {
        private readonly DbSet<CustomerOrder> dbSet;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerOrderRepository"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        public CustomerOrderRepository(StoreDbContext context)
            : base(context)
        {
            ArgumentNullException.ThrowIfNull(context);
            this.dbSet = context.Set<CustomerOrder>();
        }

        /// <summary>
        /// Adds a new customer order.
        /// </summary>
        /// <param name="entity">The customer order entity to add.</param>
        public void Add(CustomerOrder entity)
        {
            this.dbSet.Add(entity);
            this.context.SaveChanges();
        }

        /// <summary>
        /// Deletes a customer order.
        /// </summary>
        /// <param name="entity">The customer order entity to delete.</param>
        public void Delete(CustomerOrder entity)
        {
            this.dbSet.Remove(entity);
            this.context.SaveChanges();
        }

        /// <summary>
        /// Deletes a customer order by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the customer order to delete.</param>
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
        /// Gets all customer orders.
        /// </summary>
        /// <returns>A collection of all customer orders.</returns>
        public IEnumerable<CustomerOrder> GetAll()
        {
            return this.dbSet.ToList();
        }

        /// <summary>
        /// Gets a paginated list of customer orders.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="rowCount">The number of rows per page.</param>
        /// <returns>A collection of customer orders.</returns>
        public IEnumerable<CustomerOrder> GetAll(int pageNumber, int rowCount)
        {
            return this.dbSet.Skip((pageNumber - 1) * rowCount).Take(rowCount).ToList();
        }

        /// <summary>
        /// Gets a customer order by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the customer order to retrieve.</param>
        /// <returns>The customer order with the specified identifier.</returns>
        public CustomerOrder GetById(int id)
        {
            return this.dbSet.Find(id) ?? throw new InvalidOperationException("Customer order not found.");
        }

        /// <summary>
        /// Updates a customer order.
        /// </summary>
        /// <param name="entity">The customer order entity to update.</param>
        public void Update(CustomerOrder entity)
        {
            this.dbSet.Update(entity);
            this.context.SaveChanges();
        }
    }
}
