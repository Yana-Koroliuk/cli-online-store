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
using StoreBLL.Interfaces;

/// <summary>
/// Provides methods for managing users.
/// </summary>
public static class UserController
{
    private static UserService userService = UserMenuController.GetService<UserService>();
    private static UserRoleService userRoleService = UserMenuController.GetService<UserRoleService>();

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
        var menu = new ContextMenu(new AdminContextMenuHandler(userService, InputHelper.ReadUserModel), userService.GetAll);
        menu.Run();
    }

    /// <summary>
    /// Shows all user roles.
    /// </summary>
    public static void ShowAllUserRoles()
    {
        var menu = new ContextMenu(new AdminContextMenuHandler(userRoleService, InputHelper.ReadUserRoleModel), userRoleService.GetAll);
        menu.Run();
    }
}
