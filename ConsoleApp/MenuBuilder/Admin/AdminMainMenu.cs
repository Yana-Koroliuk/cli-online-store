using ConsoleApp.Controllers;
using ConsoleApp.Services;
using ConsoleApp1;
using StoreDAL.Data;

namespace ConsoleMenu.Builder;

/// <summary>
/// Creates the main menu for the administrator.
/// </summary>
public class AdminMainMenu : AbstractMenuCreator
{
    /// <summary>
    /// Gets the menu items for the administrator.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <returns>An array of menu items.</returns>
    public override (ConsoleKey id, string caption, Action action)[] GetMenuItems(StoreDbContext context)
    {
        (ConsoleKey id, string caption, Action action)[] array =
            {
                (ConsoleKey.F1, "Logout", UserMenuController.Logout),
                (ConsoleKey.F2, "Show product list", ProductController.ShowAllProducts),
                (ConsoleKey.F3, "Add product", ProductController.AddProduct),
                (ConsoleKey.F4, "Update product", ProductController.UpdateProduct),
                (ConsoleKey.F5, "Create new order", ShopController.AddOrderByAdmin),
                (ConsoleKey.F6, "Show order list", ShopController.ShowAllOrders),
                (ConsoleKey.F7, "Change order status", ShopController.ChangeOrderStatus),
                (ConsoleKey.F8, "Show user list", UserController.ShowAllUsers),
                (ConsoleKey.F9, "Update user info", UserController.UpdateUser),
                (ConsoleKey.F10, "User roles", UserController.ShowAllUserRoles),
                (ConsoleKey.F11, "Order states", ShopController.ShowAllOrderStates),
            };
        return array;
    }
}