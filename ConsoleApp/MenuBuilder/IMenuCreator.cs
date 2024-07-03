/*
Yuriy Antonov copyright 2018-2020
*/
using StoreDAL.Data;

namespace ConsoleMenu.Builder;

/// <summary>
/// Interface for menu creators.
/// </summary>
public interface IMenuCreator
{
    /// <summary>
    /// Creates a menu.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <returns>The created menu.</returns>
    Menu Create(StoreDbContext context);
}