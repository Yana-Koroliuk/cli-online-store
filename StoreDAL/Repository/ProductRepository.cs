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
    /// Represents the repository for managing <see cref="Product"/> entities.
    /// </summary>
    public class ProductRepository : AbstractRepository, IProductRepository
    {
        private readonly DbSet<Product> dbSet;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductRepository"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        public ProductRepository(StoreDbContext context)
            : base(context)
        {
            ArgumentNullException.ThrowIfNull(context);
            this.dbSet = context.Set<Product>();
        }

        /// <summary>
        /// Adds a new product entity.
        /// </summary>
        /// <param name="entity">The product entity to add.</param>
        public void Add(Product entity)
        {
            this.dbSet.Add(entity);
            this.Context.SaveChanges();
        }

        /// <summary>
        /// Deletes a product entity.
        /// </summary>
        /// <param name="entity">The product entity to delete.</param>
        public void Delete(Product entity)
        {
            this.dbSet.Remove(entity);
            this.Context.SaveChanges();
        }

        /// <summary>
        /// Deletes a product entity by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the product entity to delete.</param>
        public void DeleteById(int id)
        {
            var entity = this.dbSet.Find(id);
            if (entity != null)
            {
                this.dbSet.Remove(entity);
                this.Context.SaveChanges();
            }
        }

        /// <summary>
        /// Gets all product entities.
        /// </summary>
        /// <returns>An enumerable collection of product entities.</returns>
        public IEnumerable<Product> GetAll()
        {
            return this.dbSet.ToList();
        }

        /// <summary>
        /// Gets a paginated list of product entities.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="rowCount">The number of rows per page.</param>
        /// <returns>An enumerable collection of product entities.</returns>
        public IEnumerable<Product> GetAll(int pageNumber, int rowCount)
        {
            return this.dbSet.Skip((pageNumber - 1) * rowCount).Take(rowCount).ToList();
        }

        /// <summary>
        /// Gets a product entity by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the product entity.</param>
        /// <returns>The product entity with the specified identifier.</returns>
        public Product GetById(int id)
        {
            return this.dbSet.Find(id) ?? throw new InvalidOperationException("Product not found.");
        }

        /// <summary>
        /// Updates a product entity.
        /// </summary>
        /// <param name="entity">The product entity to update.</param>
        public void Update(Product entity)
        {
            this.dbSet.Update(entity);
            this.Context.SaveChanges();
        }
    }
}