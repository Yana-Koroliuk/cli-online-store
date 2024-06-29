namespace StoreDAL.Data.InitDataFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreDAL.Entities;

/// <summary>
/// Provides test data for initializing the database.
/// </summary>
public class TestDataFactory : AbstractDataFactory
{
    /// <summary>
    /// Gets the initial category data.
    /// </summary>
    /// <returns>An array of categories.</returns>
    public override Category[] GetCategoryData()
    {
        return new[]
        {
            new Category(1, "fruits"),
            new Category(2, "water"),
            new Category(3, "vegetables"),
            new Category(4, "seafood"),
            new Category(5, "meet"),
            new Category(6, "grocery"),
            new Category(7, "milk food"),
            new Category(8, "smartphones"),
            new Category(9, "laptop"),
            new Category(10, "photocameras"),
            new Category(11, "kitchen accesories"),
            new Category(12, "spices"),
            new Category(13, "Juice"),
            new Category(14, "alcohol drinks"),
        };
    }

    /// <summary>
    /// Gets the initial customer order data.
    /// </summary>
    /// <returns>An array of customer orders.</returns>
    public override CustomerOrder[] GetCustomerOrderData()
    {
        return new[]
        {
            new CustomerOrder(1, "2023-01-01T12:00:00", 1, 1),
            new CustomerOrder(2, "2023-01-02T13:00:00", 2, 2),
            new CustomerOrder(3, "2023-01-03T14:00:00", 3, 3),
            new CustomerOrder(4, "2023-01-04T15:00:00", 4, 4),
            new CustomerOrder(5, "2023-01-05T16:00:00", 1, 5),
            new CustomerOrder(6, "2023-01-06T17:00:00", 2, 6),
            new CustomerOrder(7, "2023-01-07T18:00:00", 3, 7),
            new CustomerOrder(8, "2023-01-08T19:00:00", 4, 8),
        };
    }

    /// <summary>
    /// Gets the initial order detail data.
    /// </summary>
    /// <returns>An array of order details.</returns>
    public override Manufacturer[] GetManufacturerData()
    {
        return new[]
        {
            new Manufacturer(1, "Manufacturer 1"),
            new Manufacturer(2, "Manufacturer 2"),
            new Manufacturer(3, "Manufacturer 3"),
        };
    }

    /// <summary>
    /// Gets the initial order detail data.
    /// </summary>
    /// <returns>An array of order details.</returns>
    public override OrderDetail[] GetOrderDetailData()
    {
        return new[]
        {
            new OrderDetail(1, 1, 1, 10.99m, 2),
            new OrderDetail(2, 1, 2, 5.99m, 1),
            new OrderDetail(3, 2, 3, 15.49m, 3),
            new OrderDetail(4, 3, 4, 7.49m, 2),
            new OrderDetail(5, 4, 5, 8.99m, 1),
            new OrderDetail(6, 5, 6, 9.99m, 4),
            new OrderDetail(7, 6, 7, 14.99m, 1),
            new OrderDetail(8, 7, 8, 19.99m, 3),
            new OrderDetail(9, 8, 9, 24.99m, 2),
        };
    }

    /// <summary>
    /// Gets the initial order state data.
    /// </summary>
    /// <returns>An array of order states.</returns>
    public override OrderState[] GetOrderStateData()
    {
        return new[]
        {
            new OrderState(1, "New Order"),
            new OrderState(2, "Cancelled by user"),
            new OrderState(3, "Cancelled by administrator"),
            new OrderState(4, "Confirmed"),
            new OrderState(5, "Moved to delivery company"),
            new OrderState(6, "In delivery"),
            new OrderState(7, "Delivered to client"),
            new OrderState(8, "Delivery confirmed by client"),
        };
    }

    /// <summary>
    /// Gets the initial product data.
    /// </summary>
    /// <returns>An array of products.</returns>
    public override Product[] GetProductData()
    {
        return new[]
        {
            new Product(1, 1, 1, "A tasty fruit", 1.99m),
            new Product(2, 2, 2, "A refreshing drink", 0.99m),
            new Product(3, 3, 3, "A healthy vegetable", 2.49m),
            new Product(4, 4, 1, "A gourmet seafood", 12.99m),
            new Product(5, 5, 2, "A premium meat", 8.99m),
            new Product(6, 6, 3, "Organic grocery item", 3.49m),
            new Product(7, 7, 1, "Fresh milk", 1.29m),
            new Product(8, 8, 2, "Latest smartphone", 699.99m),
            new Product(9, 9, 3, "High-performance laptop", 999.99m),
            new Product(10, 10, 1, "High-resolution camera", 499.99m),
            new Product(11, 11, 2, "Kitchen tool", 24.99m),
            new Product(12, 12, 3, "Exotic spices", 5.99m),
            new Product(13, 13, 1, "Freshly squeezed juice", 2.99m),
            new Product(14, 14, 2, "Fine wine", 19.99m),
        };
    }

    /// <summary>
    /// Gets the initial product title data.
    /// </summary>
    /// <returns>An array of product titles.</returns>
    public override ProductTitle[] GetProductTitleData()
    {
        return new[]
        {
            new ProductTitle(1, "Apple", 1),
            new ProductTitle(2, "Water Bottle", 2),
            new ProductTitle(3, "Carrot", 3),
            new ProductTitle(4, "Salmon", 4),
            new ProductTitle(5, "Beef Steak", 5),
            new ProductTitle(6, "Organic Pasta", 6),
            new ProductTitle(7, "Whole Milk", 7),
            new ProductTitle(8, "iPhone", 8),
            new ProductTitle(9, "MacBook Pro", 9),
            new ProductTitle(10, "Canon DSLR", 10),
            new ProductTitle(11, "Blender", 11),
            new ProductTitle(12, "Turmeric", 12),
            new ProductTitle(13, "Orange Juice", 13),
            new ProductTitle(14, "Red Wine", 14),
        };
    }

    /// <summary>
    /// Gets the initial user data.
    /// </summary>
    /// <returns>An array of users.</returns>
    public override User[] GetUserData()
    {
        return new[]
        {
            new User(1, "John", "Doe", "john.doe", "password123", 2),
            new User(2, "Jane", "Doe", "jane.doe", "password456", 2),
            new User(3, "Admin", "User", "admin", "adminpass", 1),
            new User(4, "Guest", "User", "guest", "guestpass", 3),
        };
    }

    /// <summary>
    /// Gets the initial user role data.
    /// </summary>
    /// <returns>An array of user roles.</returns>
    public override UserRole[] GetUserRoleData()
    {
        return new[]
        {
            new UserRole(1, "Admin"),
            new UserRole(2, "Registered"),
            new UserRole(3, "Guest"),
        };
    }
}
