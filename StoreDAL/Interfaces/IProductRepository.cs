namespace StoreDAL.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using StoreDAL.Entities;

/// <summary>
/// Represents a repository interface for Product entities.
/// </summary>
public interface IProductRepository : IRepository<Product>
{
}
