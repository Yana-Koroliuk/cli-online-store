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
    /// Represents the repository for managing <see cref="ProductTitle"/> entities.
    /// </summary>
    public class ProductTitleRepository : AbstractRepository, IProductTitleRepository
    {
        private readonly DbSet<ProductTitle> dbSet;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductTitleRepository"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        public ProductTitleRepository(StoreDbContext context)
            : base(context)
        {
            ArgumentNullException.ThrowIfNull(context);
            this.dbSet = context.Set<ProductTitle>();
        }

        /// <summary>
        /// Adds a new product title entity.
        /// </summary>
        /// <param name="entity">The product title entity to add.</param>
        public void Add(ProductTitle entity)
        {
            this.dbSet.Add(entity);
            this.context.SaveChanges();
        }

        /// <summary>
        /// Deletes a product title entity.
        /// </summary>
        /// <param name="entity">The product title entity to delete.</param>
        public void Delete(ProductTitle entity)
        {
            this.dbSet.Remove(entity);
            this.context.SaveChanges();
        }

        /// <summary>
        /// Deletes a product title entity by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the product title entity to delete.</param>
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
        /// Gets all product title entities.
        /// </summary>
        /// <returns>An enumerable collection of product title entities.</returns>
        public IEnumerable<ProductTitle> GetAll()
        {
            return this.dbSet.ToList();
        }

        /// <summary>
        /// Gets a paginated list of product title entities.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="rowCount">The number of rows per page.</param>
        /// <returns>An enumerable collection of product title entities.</returns>
        public IEnumerable<ProductTitle> GetAll(int pageNumber, int rowCount)
        {
            return this.dbSet.Skip((pageNumber - 1) * rowCount).Take(rowCount).ToList();
        }

        /// <summary>
        /// Gets a product title entity by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the product title entity.</param>
        /// <returns>The product title entity with the specified identifier.</returns>
        public ProductTitle GetById(int id)
        {
            return this.dbSet.Find(id) ?? throw new InvalidOperationException("Product title not found");
        }

        /// <summary>
        /// Updates a product title entity.
        /// </summary>
        /// <param name="entity">The product title entity to update.</param>
        public void Update(ProductTitle entity)
        {
            this.dbSet.Update(entity);
            this.context.SaveChanges();
        }
    }
}
