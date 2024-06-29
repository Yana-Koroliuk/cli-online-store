namespace StoreDAL.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using StoreDAL.Entities;

/// <summary>
/// Represents a repository interface for User entities.
/// </summary>
public interface IUserRepository : IRepository<User>
{
    /// <summary>
    /// Gets a user by login.
    /// </summary>
    /// <param name="login">The login of the user.</param>
    /// <returns>The user with the specified login.</returns>
    User GetByLogin(string login);
}
