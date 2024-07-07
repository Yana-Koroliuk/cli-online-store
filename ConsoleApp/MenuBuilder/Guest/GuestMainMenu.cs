using ConsoleApp.Controllers;
using ConsoleApp.Services;
using ConsoleApp1;
using StoreBLL.Services;
using StoreDAL.Data;

namespace ConsoleMenu.Builder;

/// <summary>
/// Represents the main menu for guest users.
/// </summary>
public class GuestMainMenu : AbstractMenuCreator
{
    private static UserService userService = UserMenuController.GetService<UserService>();
    private static ProductService productService = UserMenuController.GetService<ProductService>();
    private static ProductTitleService productTitleService = UserMenuController.GetService<ProductTitleService>();
    private static ManufacturerService manufacturerService = UserMenuController.GetService<ManufacturerService>();

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
            (ConsoleKey.F2, "Show product list", () => ProductController.ShowAllProducts(productService, productTitleService, manufacturerService)),
            (ConsoleKey.F3, "Register", () => UserController.AddUser(userService)),
        };
        return array;
    }
}