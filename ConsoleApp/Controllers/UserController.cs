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
    /// <summary>
    /// Registers a new user.
    /// </summary>
    /// <param name="userService">The user service used to add the new user.</param>
    public static void AddUser(ICrud userService)
    {
        Console.WriteLine("Register new user:");
        try
        {
            var newUser = InputHelper.ReadUserModel();
            userService?.Add(newUser);
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
    /// <param name="userService">The user service used to update the user's information.</param>
    public static void UpdateUser(ICrud userService)
    {
        try
        {
            int userId = UserMenuController.UserRole == UserRoles.Administrator ? InputHelper.ReadUserId() : UserMenuController.UserId;
            userService = userService ?? throw new ArgumentNullException(nameof(userService));
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
    /// <param name="userService">The user service used to retrieve and display all users.</param>
    public static void ShowAllUsers(ICrud userService)
    {
        Console.WriteLine("Users:");
        userService = userService ?? throw new ArgumentNullException(nameof(userService));
        var menu = new ContextMenu(new AdminContextMenuHandler(userService, InputHelper.ReadUserModel), userService.GetAll);
        menu.Run();
    }

    /// <summary>
    /// Shows all user roles.
    /// </summary>
    /// <param name="userRoleService">The user role service used to retrieve and display all user roles.</param>
    public static void ShowAllUserRoles(ICrud userRoleService)
    {
        userRoleService = userRoleService ?? throw new ArgumentNullException(nameof(userRoleService));
        var menu = new ContextMenu(new AdminContextMenuHandler(userRoleService, InputHelper.ReadUserRoleModel), userRoleService.GetAll);
        menu.Run();
    }
}
