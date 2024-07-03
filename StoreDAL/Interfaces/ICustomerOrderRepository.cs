namespace StoreDAL.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using StoreDAL.Entities;

/// <summary>
/// Represents a repository interface for CustomerOrder entities.
/// </summary>
public interface ICustomerOrderRepository : IRepository<CustomerOrder>
{
}
