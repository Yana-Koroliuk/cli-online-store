using ConsoleApp.Controllers;
using ConsoleApp.Services;
using ConsoleApp1;
using StoreDAL.Data;

namespace ConsoleMenu.Builder;

/// <summary>
/// Provides the main menu for registered users.
/// </summary
public class UserMainMenu : AbstractMenuCreator
{
    /// <summary>
    /// Gets the menu items for the registered user.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <returns>An array of menu items.</returns>
    public override (ConsoleKey id, string caption, Action action)[] GetMenuItems(StoreDbContext context)
    {
        (ConsoleKey id, string caption, Action action)[] array =
            {
                (ConsoleKey.F1, "Logout", UserMenuController.Logout),
                (ConsoleKey.F2, "Show product list", ProductController.ShowAllProducts),
                (ConsoleKey.F3, "Show order list", ShopController.ShowAllOrders),
                (ConsoleKey.F4, "Create new order", ShopController.AddOrder),
                (ConsoleKey.F5, "Cancel order", ShopController.CancelOrder),
                (ConsoleKey.F6, "Confirm order delivery", ShopController.ConfirmOrderDelivery),
                (ConsoleKey.F7, "Update personal information", UserController.UpdateUser),
            };
        return array;
    }
}