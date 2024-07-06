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
    /// Represents the repository for managing <see cref="Category"/> entities.
    /// </summary>
    public class CategoryRepository : AbstractRepository, ICategoryRepository
    {
        private readonly DbSet<Category> dbSet;

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryRepository"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        public CategoryRepository(StoreDbContext context)
            : base(context)
        {
            ArgumentNullException.ThrowIfNull(context);
            this.dbSet = context.Set<Category>();
        }

        /// <summary>
        /// Adds a new category entity.
        /// </summary>
        /// <param name="entity">The category entity to add.</param>
        public void Add(Category entity)
        {
            this.dbSet.Add(entity);
            this.Context.SaveChanges();
        }

        /// <summary>
        /// Deletes a category entity.
        /// </summary>
        /// <param name="entity">The category entity to delete.</param>
        public void Delete(Category entity)
        {
            this.dbSet.Remove(entity);
            this.Context.SaveChanges();
        }

        /// <summary>
        /// Deletes a category entity by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the category entity to delete.</param>
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
        /// Gets all category entities.
        /// </summary>
        /// <returns>An enumerable collection of category entities.</returns>
        public IEnumerable<Category> GetAll()
        {
            return this.dbSet.ToList();
        }

        /// <summary>
        /// Gets a paginated list of category entities.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="rowCount">The number of rows per page.</param>
        /// <returns>An enumerable collection of category entities.</returns>
        public IEnumerable<Category> GetAll(int pageNumber, int rowCount)
        {
            return this.dbSet.Skip((pageNumber - 1) * rowCount).Take(rowCount).ToList();
        }

        /// <summary>
        /// Gets a category entity by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the category entity.</param>
        /// <returns>The category entity with the specified identifier.</returns>
        public Category GetById(int id)
        {
            return this.dbSet.Find(id) ?? throw new InvalidOperationException("Category not found.");
        }

        /// <summary>
        /// Updates a category entity.
        /// </summary>
        /// <param name="entity">The category entity to update.</param>
        public void Update(Category entity)
        {
            this.dbSet.Update(entity);
            this.Context.SaveChanges();
        }
    }
}
