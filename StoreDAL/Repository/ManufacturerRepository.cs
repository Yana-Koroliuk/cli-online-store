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
    /// Represents the repository for managing <see cref="Manufacturer"/> entities.
    /// </summary>
    public class ManufacturerRepository : AbstractRepository, IManufacturerRepository
    {
        private readonly DbSet<Manufacturer> dbSet;

        /// <summary>
        /// Initializes a new instance of the <see cref="ManufacturerRepository"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        public ManufacturerRepository(StoreDbContext context)
            : base(context)
        {
            ArgumentNullException.ThrowIfNull(context);
            this.dbSet = context.Set<Manufacturer>();
        }

        /// <summary>
        /// Adds a new manufacturer entity.
        /// </summary>
        /// <param name="entity">The manufacturer entity to add.</param>
        public void Add(Manufacturer entity)
        {
            this.dbSet.Add(entity);
            this.Context.SaveChanges();
        }

        /// <summary>
        /// Deletes a manufacturer entity.
        /// </summary>
        /// <param name="entity">The manufacturer entity to delete.</param>
        public void Delete(Manufacturer entity)
        {
            this.dbSet.Remove(entity);
            this.Context.SaveChanges();
        }

        /// <summary>
        /// Deletes a manufacturer entity by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the manufacturer entity to delete.</param>
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
        /// Gets all manufacturer entities.
        /// </summary>
        /// <returns>An enumerable collection of manufacturer entities.</returns>
        public IEnumerable<Manufacturer> GetAll()
        {
            return this.dbSet.ToList();
        }

        /// <summary>
        /// Gets a paginated list of manufacturer entities.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="rowCount">The number of rows per page.</param>
        /// <returns>An enumerable collection of manufacturer entities.</returns>
        public IEnumerable<Manufacturer> GetAll(int pageNumber, int rowCount)
        {
            return this.dbSet.Skip((pageNumber - 1) * rowCount).Take(rowCount).ToList();
        }

        /// <summary>
        /// Gets a manufacturer entity by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the manufacturer entity.</param>
        /// <returns>The manufacturer entity with the specified identifier.</returns>
        public Manufacturer GetById(int id)
        {
            return this.dbSet.Find(id) ?? throw new InvalidOperationException("Manufacturer not found.");
        }

        /// <summary>
        /// Updates a manufacturer entity.
        /// </summary>
        /// <param name="entity">The manufacturer entity to update.</param>
        public void Update(Manufacturer entity)
        {
            this.dbSet.Update(entity);
            this.Context.SaveChanges();
        }
    }
}
