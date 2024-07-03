namespace StoreDAL.Data.InitDataFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreDAL.Entities;

/// <summary>
/// Abstract factory for initializing data.
/// </summary>
public abstract class AbstractDataFactory
{
    /// <summary>
    /// Gets the category data.
    /// </summary>
    /// <returns>An array of Category entities.</returns>
    public abstract Category[] GetCategoryData();

    /// <summary>
    /// Gets the customer order data.
    /// </summary>
    /// <returns>An array of CustomerOrder entities.</returns>
    public abstract CustomerOrder[] GetCustomerOrderData();

    /// <summary>
    /// Gets the manufacturer data.
    /// </summary>
    /// <returns>An array of Manufacturer entities.</returns>
    public abstract Manufacturer[] GetManufacturerData();

    /// <summary>
    /// Gets the order detail data.
    /// </summary>
    /// <returns>An array of OrderDetail entities.</returns>
    public abstract OrderDetail[] GetOrderDetailData();

    /// <summary>
    /// Gets the order state data.
    /// </summary>
    /// <returns>An array of OrderState entities.</returns>
    public abstract OrderState[] GetOrderStateData();

    /// <summary>
    /// Gets the product data.
    /// </summary>
    /// <returns>An array of Product entities.</returns>
    public abstract Product[] GetProductData();

    /// <summary>
    /// Gets the product title data.
    /// </summary>
    /// <returns>An array of ProductTitle entities.</returns>
    public abstract ProductTitle[] GetProductTitleData();

    /// <summary>
    /// Gets the user data.
    /// </summary>
    /// <returns>An array of User entities.</returns>
    public abstract User[] GetUserData();

    /// <summary>
    /// Gets the user role data.
    /// </summary>
    /// <returns>An array of UserRole entities.</returns>
    public abstract UserRole[] GetUserRoleData();
}
