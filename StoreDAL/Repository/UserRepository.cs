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
    /// Represents the repository for managing <see cref="User"/> entities.
    /// </summary>
    public class UserRepository : AbstractRepository, IUserRepository
    {
        private readonly DbSet<User> dbSet;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        public UserRepository(StoreDbContext context)
            : base(context)
        {
            ArgumentNullException.ThrowIfNull(context);
            this.dbSet = context.Set<User>();
        }

        /// <summary>
        /// Adds a new user entity.
        /// </summary>
        /// <param name="entity">The user entity to add.</param>
        public void Add(User entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            entity.Password = BCrypt.Net.BCrypt.HashPassword(entity.Password);
            this.dbSet.Add(entity);
            this.context.SaveChanges();
        }

        /// <summary>
        /// Deletes a user entity.
        /// </summary>
        /// <param name="entity">The user entity to delete.</param>
        public void Delete(User entity)
        {
            this.dbSet.Remove(entity);
            this.context.SaveChanges();
        }

        /// <summary>
        /// Deletes a user entity by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the user entity to delete.</param>
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
        /// Gets all user entities.
        /// </summary>
        /// <returns>An enumerable collection of user entities.</returns>
        public IEnumerable<User> GetAll()
        {
            return this.dbSet.ToList();
        }

        /// <summary>
        /// Gets a paginated list of user entities.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="rowCount">The number of rows per page.</param>
        /// <returns>An enumerable collection of user entities.</returns>
        public IEnumerable<User> GetAll(int pageNumber, int rowCount)
        {
            return this.dbSet.Skip((pageNumber - 1) * rowCount).Take(rowCount).ToList();
        }

        /// <summary>
        /// Gets a user entity by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the user entity.</param>
        /// <returns>The user entity with the specified identifier.</returns>
        public User GetById(int id)
        {
            return this.dbSet.Find(id) ?? throw new InvalidOperationException("User not found.");
        }

        /// <summary>
        /// Gets a user entity by its login.
        /// </summary>
        /// <param name="login">The login of the user.</param>
        /// <returns>The user entity with the specified login.</returns>
        public User GetByLogin(string login)
        {
            return this.dbSet.FirstOrDefault(user => user.Login == login)
            ?? throw new InvalidOperationException("User not found.");
        }

        /// <summary>
        /// Updates a user entity.
        /// </summary>
        /// <param name="entity">The user entity to update.</param>
        public void Update(User entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            this.dbSet.Update(entity);
            this.context.SaveChanges();
        }
    }
}
