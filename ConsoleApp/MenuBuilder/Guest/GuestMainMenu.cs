using ConsoleApp.Controllers;
using ConsoleApp.Services;
using ConsoleApp1;
using StoreDAL.Data;

namespace ConsoleMenu.Builder;

/// <summary>
/// Represents the main menu for guest users.
/// </summary>
public class GuestMainMenu : AbstractMenuCreator
{
    /// <summary>
    /// Gets the menu items for guest users.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <returns>An array of menu items.</returns>
    public override (ConsoleKey id, string caption, Action action)[] GetMenuItems(StoreDbContext context)
    {
        (ConsoleKey id, string caption, Action action)[] array =
        {
            (ConsoleKey.F1, "Login", UserMenuController.Login),
            (ConsoleKey.F2, "Show product list", ProductController.ShowAllProducts),
            (ConsoleKey.F3, "Register", UserController.AddUser),
        };
        return array;
    }
}