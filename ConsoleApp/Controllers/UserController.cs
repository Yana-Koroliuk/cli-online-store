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
using StoreDAL.Entities;

/// <summary>
/// Provides methods for managing users.
/// </summary>
public static class UserController
{
    private static UserService userService = UserMenuController.GetService<UserService>();

    /// <summary>
    /// Registers a new user.
    /// </summary>
    public static void AddUser()
    {
        Console.WriteLine("Register new user:");
        try
        {
            var newUser = InputHelper.ReadUserModel();
            userService.Add(newUser);
            Console.WriteLine("Registration successful.");
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Registration failed: {ex.Message}");
        }
    }

    /// <summary>
    /// Updates the personal information of a user.
    /// </summary>
    public static void UpdateUser()
    {
        try
        {
            int userId = UserMenuController.UserRole == UserRoles.Administrator ? InputHelper.ReadUserId() : UserMenuController.UserId;
            var user = userService.GetById(userId);
            var userModel = InputHelper.ReadUserModel((UserModel)user);
            userService.Update(userModel);
            Console.WriteLine("User information updated successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Updating failed: {ex.Message}");
        }
    }

    /// <summary>
    /// Displays all users.
    /// </summary>
    public static void ShowAllUsers()
    {
        Console.WriteLine("Users:");
        var service = UserMenuController.GetService<UserService>();
        var menu = new ContextMenu(new AdminContextMenuHandler(service, InputHelper.ReadUserModel), service.GetAll);
        menu.Run();
    }

    /// <summary>
    /// Shows all user roles.
    /// </summary>
    public static void ShowAllUserRoles()
    {
        var service = UserMenuController.GetService<UserRoleService>();
        var menu = new ContextMenu(new AdminContextMenuHandler(service, InputHelper.ReadUserRoleModel), service.GetAll);
        menu.Run();
    }
}
