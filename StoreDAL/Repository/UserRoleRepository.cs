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

/// <summary>
/// Represents the repository for managing <see cref="UserRole"/> entities.
/// </summary>
public class UserRoleRepository : AbstractRepository, IUserRoleRepository
{
    private readonly DbSet<UserRole> dbSet;

    /// <summary>
    /// Initializes a new instance of the <see cref="UserRoleRepository"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public UserRoleRepository(StoreDbContext context)
        : base(context)
    {
        ArgumentNullException.ThrowIfNull(context);
        this.dbSet = context.Set<UserRole>();
    }

    /// <summary>
    /// Adds a new user role entity.
    /// </summary>
    /// <param name="entity">The user role entity to add.</param>
    public void Add(UserRole entity)
    {
        this.dbSet.Add(entity);
        this.Context.SaveChanges();
    }

    /// <summary>
    /// Deletes a user role entity.
    /// </summary>
    /// <param name="entity">The user role entity to delete.</param>
    public void Delete(UserRole entity)
    {
        this.dbSet.Remove(entity);
        this.Context.SaveChanges();
    }

    /// <summary>
    /// Deletes a user role entity by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the user role entity to delete.</param>
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
    /// Gets all user role entities.
    /// </summary>
    /// <returns>An enumerable collection of user role entities.</returns>
    public IEnumerable<UserRole> GetAll()
    {
        return this.dbSet.ToList();
    }

    /// <summary>
    /// Gets a paginated list of user role entities.
    /// </summary>
    /// <param name="pageNumber">The page number.</param>
    /// <param name="rowCount">The number of rows per page.</param>
    /// <returns>An enumerable collection of user role entities.</returns>
    public IEnumerable<UserRole> GetAll(int pageNumber, int rowCount)
    {
        return this.dbSet.Skip((pageNumber - 1) * rowCount).Take(rowCount).ToList();
    }

    /// <summary>
    /// Gets a user role entity by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the user role entity.</param>
    /// <returns>The user role entity with the specified identifier.</returns>
    public UserRole GetById(int id)
    {
        return this.dbSet.Find(id) ?? throw new InvalidOperationException("User role not found.");
    }

    /// <summary>
    /// Updates a user role entity.
    /// </summary>
    /// <param name="entity">The user role entity to update.</param>
    public void Update(UserRole entity)
    {
        this.dbSet.Update(entity);
        this.Context.SaveChanges();
    }
}