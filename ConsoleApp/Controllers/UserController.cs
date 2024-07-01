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

    /// <summary>
    /// Updates the personal information of a user.
    /// </summary>
    public static void UpdateUser()
    {
        var userService = new UserService(context);
        int userId;

        if (UserMenuController.UserRole == UserRoles.Administrator)
        {
            Console.WriteLine("Enter the ID of the user to update:");
            var userIdInput = Console.ReadLine();

            if (!int.TryParse(userIdInput, out userId))
            {
                Console.WriteLine("Invalid user ID.");
                return;
            }
        }
        else
        {
            userId = UserMenuController.UserId;
        }

        var user = userService.GetById(userId);
        if (user == null)
        {
            Console.WriteLine("User not found.");
            return;
        }

        var userModel = (UserModel)user;

        Console.WriteLine($"Current Name: {userModel.Name} ");
        Console.WriteLine("New Name (leave empty to keep current): ");
        var newName = Console.ReadLine();
        userModel.Name = string.IsNullOrEmpty(newName) ? userModel.Name : newName;

        Console.WriteLine($"Current Last Name: {userModel.LastName}");
        Console.WriteLine("New Last Name (leave empty to keep current): ");
        var newLastName = Console.ReadLine();
        userModel.LastName = string.IsNullOrEmpty(newLastName) ? userModel.LastName : newLastName;

        Console.WriteLine($"Current Login: {userModel.Login}");
        Console.WriteLine("New Login (leave empty to keep current): ");
        var newLogin = Console.ReadLine();
        userModel.Login = string.IsNullOrEmpty(newLogin) ? userModel.Login : newLogin;

        Console.WriteLine("New Password (leave empty to keep current): ");
        var newPassword = Console.ReadLine();
        userModel.Password = string.IsNullOrEmpty(newPassword) ? userModel.Password : newPassword;

        userService.Update(userModel);
        Console.WriteLine("User information updated successfully.");
    }

    /// <summary>
    /// Displays all users.
    /// </summary>
    public static void ShowAllUsers()
    {
        var userService = new UserService(context);
        var userRoleService = new UserRoleService(context);
        var users = userService.GetAll().OfType<UserModel>();

        Console.WriteLine("Users:");
        foreach (var user in users)
        {
            var userRole = (UserRoleModel)userRoleService.GetById(user.RoleId);
            var roleName = userRole != null ? userRole.RoleName : "Unknown";
            Console.WriteLine($"Id: {user.Id} Name: {user.Name} LastName: {user.LastName} Login: {user.Login} Role: {roleName}");
        }
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
}
