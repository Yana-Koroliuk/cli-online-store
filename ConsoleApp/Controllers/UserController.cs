namespace ConsoleApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1;
using ConsoleApp.Controllers;
using ConsoleApp.Handlers.ContextMenu;
using ConsoleApp.Helpers;
using ConsoleMenu;
using StoreDAL.Data;
using StoreBLL.Models;
using StoreBLL.Services;

/// <summary>
/// Provides methods for managing users.
/// </summary>
public static class UserController
{
    private static StoreDbContext context = UserMenuController.Context;

    /// <summary>
    /// Registers a new user.
    /// </summary>
    public static void AddUser()
    {
        Console.WriteLine("Register new user:");
        Console.WriteLine("Name: ");
        var name = Console.ReadLine();
        Console.WriteLine("Last Name: ");
        var lastName = Console.ReadLine();
        Console.WriteLine("Login: ");
        var login = Console.ReadLine();
        Console.WriteLine("Password: ");
        var password = Console.ReadLine();

        if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(lastName) ||
            string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
        {
            Console.WriteLine("Input data cannot be empty.");
            return;
        }

        var userService = new UserService(context);
        var newUser = new UserModel(0, name, lastName, login, password, (int)UserRoles.RegistredCustomer);
        userService.Add(newUser);
        Console.WriteLine("Registration successful.");
    }

    public static void UpdateUser()
    {
        throw new NotImplementedException();
    }

    public static void DeleteUser()
    {
        throw new NotImplementedException();
    }

    public static void ShowUser()
    {
        throw new NotImplementedException();
    }

    public static void ShowAllUsers()
    {
        throw new NotImplementedException();
    }

    public static void AddUserRole()
    {
        throw new NotImplementedException();
    }

    public static void UpdateUserRole()
    {
        throw new NotImplementedException();
    }

    public static void DeleteUserRole()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Shows all user roles.
    /// </summary>
    public static void ShowAllUserRoles()
    {
        var service = new UserRoleService(context);
        var menu = new ContextMenu(new AdminContextMenuHandler(service, InputHelper.ReadUserRoleModel), service.GetAll);
        menu.Run();
    }

    public static void AddProductTitle()
    {
        throw new NotImplementedException();
    }

    public static void UpdateProductTitle()
    {
        throw new NotImplementedException();
    }

    public static void DeleteProductTitle()
    {
        throw new NotImplementedException();
    }

    public static void ShowAllProductTitles()
    {
        throw new NotImplementedException();
    }

    public static void AddManufacturer()
    {
        throw new NotImplementedException();
    }

    public static void UpdateManufacturer()
    {
        throw new NotImplementedException();
    }

    public static void DeleteManufacturer()
    {
        throw new NotImplementedException();
    }

    public static void ShowAllManufacturers()
    {
        throw new NotImplementedException();
    }
}
