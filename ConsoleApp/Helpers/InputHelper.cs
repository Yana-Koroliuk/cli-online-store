namespace ConsoleApp.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ConsoleApp1;
using StoreBLL.Models;
using StoreDAL.Entities;

/// <summary>
/// Helper class for reading input.
/// </summary>
internal static class InputHelper
{
    /// <summary>
    /// Reads an OrderStateModel from input.
    /// </summary>
    /// <returns>The read OrderStateModel.</returns>
    public static OrderStateModel ReadOrderStateModel()
    {
        var name = ReadStringInput("Input State Name");
        return new OrderStateModel(0, name);
    }

    /// <summary>
    /// Reads a UserRoleModel from input.
    /// </summary>
    /// <returns>The read UserRoleModel.</returns>
    public static UserRoleModel ReadUserRoleModel()
    {
        var name = ReadStringInput("Input User Role Name");
        return new UserRoleModel(0, name);
    }

    /// <summary>
    /// Reads a new UserModel from input.
    /// </summary>
    /// <returns>The read UserModel.</returns>
    public static UserModel ReadUserModel()
    {
        var name = ReadStringInput("Name");
        var lastName = ReadStringInput("Last Name");
        var login = ReadStringInput("Login");
        var password = ReadStringInput("Password");
        return new UserModel(0, name, lastName, login, password, (int)UserRoles.RegistredCustomer);
    }

    /// <summary>
    /// Reads and updates a UserModel from input.
    /// </summary>
    /// <param name="currentUser">The current user model.</param>
    /// <returns>The updated UserModel.</returns>
    public static UserModel ReadUserModel(UserModel currentUser)
    {
        currentUser.Name = ReadNewValue("Name", currentUser.Name);
        currentUser.LastName = ReadNewValue("Last Name", currentUser.LastName);
        currentUser.Login = ReadNewValue("Login", currentUser.Login);
        Console.WriteLine("New Password (leave empty to keep current): ");
        var newPassword = Console.ReadLine();
        currentUser.Password = string.IsNullOrEmpty(newPassword) ? currentUser.Password : newPassword;
        return currentUser;
    }

    /// <summary>
    /// Reads the user ID from input.
    /// </summary>
    /// <returns>The read user ID.</returns>
    public static int ReadUserId()
    {
        return ReadIntInput("Enter the ID of the user to update:", "User ID");
    }

    /// <summary>
    /// Reads the customer order ID from input.
    /// </summary>
    /// <returns>The read customer order ID.</returns>
    public static int ReadCustomerOrderId()
    {
        return ReadIntInput("Enter the ID of the customer order:", "Order ID");
    }

    /// <summary>
    /// Reads the product ID from input.
    /// </summary>
    /// <returns>The read product ID.</returns>
    public static int ReadProductId()
    {
        return ReadIntInput("Enter product ID to update:", "Product ID");
    }

    /// <summary>
    /// Reads the status name from input.
    /// </summary>
    /// <returns>The read status name.</returns>
    public static string ReadStatusName()
    {
        return ReadStringInput("Select new status by name:");
    }

    /// <summary>
    /// Reads data for adding a new user from input.
    /// </summary>
    /// <returns>A tuple containing user data.</returns>
    public static (string login, string password) ReadLoginData()
    {
        var login = ReadStringInput("Login: ");
        var password = ReadStringInput("Password: ");
        return (login, password);
    }

    /// <summary>
    /// Reads a CustomerOrderModel from input.
    /// </summary>
    /// <returns>The read CustomerOrderModel.</returns>
    public static CustomerOrderModel ReadCustomerOrderModel()
    {
        Console.WriteLine("Creating a new customer order:");
        var orderDate = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture);
        var userId = ReadIntInput("Enter User ID: ", "User ID");
        var orderStateId = ReadIntInput("Enter Order State ID: ", "Order State ID");
        return new CustomerOrderModel(0, orderDate, userId, orderStateId);
    }

    /// <summary>
    /// Reads an OrderDetailModel from input.
    /// </summary>
    /// <returns>The read OrderDetailModel.</returns>
    public static OrderDetailModel ReadOrderDetailModel()
    {
        Console.WriteLine("Add product details to order:");
        var productId = ReadIntInput("Enter Product ID: ", "Product ID");
        var amount = ReadIntInput("Amount: ", "Amount");
        return new OrderDetailModel(0, 0, productId, 0, amount);
    }

    /// <summary>
    /// Reads whether to add another product to the order.
    /// </summary>
    /// <returns>1 if adding another product, otherwise 0.</returns>
    public static int ReadProductsInOrder()
    {
        Console.WriteLine("Add another product? (yes/no)");
        var another = Console.ReadLine();
        return another != null && another == "yes" ? 1 : 0;
    }

    /// <summary>
    /// Reads data for adding a new product from input.
    /// </summary>
    /// <returns>A tuple containing product data.</returns>
    public static (string productName, string categoryName, string description, decimal unitPrice) ReadDataAddProduct()
    {
        Console.WriteLine("Add new product:");
        var productName = ReadStringInput("Product Name");
        var categoryName = ReadStringInput("Category Name");
        var description = ReadStringInput("Description");
        var unitPrice = ReadDecimalInput("Unit Price");
        return (productName, categoryName, description, unitPrice);
    }

    /// <summary>
    /// Reads data for updating an existing product from input.
    /// </summary>
    /// <returns>A tuple containing updated product data.</returns>
    public static (string productName, string categoryName, string manufacturer, string description, decimal unitPrice) ReadDataUpdateProduct()
    {
        var productName = ReadOptionalStringInput("Enter new product name (leave empty to keep current): ");
        var categoryName = ReadOptionalStringInput("Enter new category name (leave empty to keep current): ");
        var manufacturer = ReadOptionalStringInput("Enter new manufacturer name (leave empty to keep current): ");
        var description = ReadOptionalStringInput("Enter new description (leave empty to keep current): ");
        var unitPrice = ReadOptionalDecimalInput("Enter new unit price (leave empty to keep current): ");
        return (productName, categoryName, manufacturer, description, unitPrice);
    }

    /// <summary>
    /// Reads an integer input from the console.
    /// </summary>
    /// <param name="message">The message to display to the user.</param>
    /// <param name="fieldName">The name of the field being read.</param>
    /// <returns>The read integer value.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the input is empty or not a valid integer.</exception>
    public static int ReadIntInput(string message, string fieldName)
    {
        Console.WriteLine(message);
        var input = Console.ReadLine();
        if (string.IsNullOrEmpty(input) || !int.TryParse(input, out var value))
        {
            throw new InvalidOperationException($"{fieldName} cannot be empty and must be a valid integer.");
        }

        return value;
    }

    /// <summary>
    /// Reads a string input from the console.
    /// </summary>
    /// <param name="message">The message to display to the user.</param>
    /// <returns>The read string value.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the input is empty.</exception>
    private static string ReadStringInput(string message)
    {
        Console.WriteLine(message);
        var input = Console.ReadLine();
        if (string.IsNullOrEmpty(input))
        {
            throw new InvalidOperationException("Input data cannot be empty.");
        }

        return input;
    }

    /// <summary>
    /// Reads an optional string input from the console.
    /// </summary>
    /// <param name="message">The message to display to the user.</param>
    /// <returns>The read string value or an empty string if the input is empty.</returns>
    private static string ReadOptionalStringInput(string message)
    {
        Console.WriteLine(message);
        return Console.ReadLine() ?? string.Empty;
    }

    /// <summary>
    /// Reads a decimal input from the console.
    /// </summary>
    /// <param name="message">The message to display to the user.</param>
    /// <returns>The read decimal value.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the input is empty or not a valid decimal.</exception>
    private static decimal ReadDecimalInput(string message)
    {
        Console.WriteLine(message);
        var input = Console.ReadLine();
        if (string.IsNullOrEmpty(input) || !decimal.TryParse(input, out var value))
        {
            throw new InvalidOperationException("Input data cannot be empty and must be a valid decimal.");
        }

        return value;
    }

    /// <summary>
    /// Reads an optional decimal input from the console.
    /// </summary>
    /// <param name="prompt">The message to display to the user.</param>
    /// <returns>The read decimal value or 0 if the input is empty or not a valid decimal.</returns>
    private static decimal ReadOptionalDecimalInput(string prompt)
    {
        Console.WriteLine(prompt);
        var input = Console.ReadLine();
        return decimal.TryParse(input, out var value) ? value : 0m;
    }

    /// <summary>
    /// Reads a new value for a specified field from the console.
    /// </summary>
    /// <param name="fieldName">The name of the field being updated.</param>
    /// <param name="currentValue">The current value of the field.</param>
    /// <returns>The new value for the field or the current value if the input is empty.</returns>
    private static string ReadNewValue(string fieldName, string currentValue)
    {
        Console.WriteLine($"Current {fieldName}: {currentValue}");
        Console.WriteLine($"New {fieldName} (leave empty to keep current): ");
        var newValue = Console.ReadLine();
        return string.IsNullOrEmpty(newValue) ? currentValue : newValue;
    }
}
