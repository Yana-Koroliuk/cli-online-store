using ConsoleApp.Controllers;
using ConsoleApp.Services;
using ConsoleApp1;
using StoreBLL.Services;
using StoreDAL.Data;

namespace ConsoleMenu.Builder;

/// <summary>
/// Provides the main menu for registered users.
/// </summary
public class UserMainMenu : AbstractMenuCreator
{
    private static UserService userService = UserMenuController.GetService<UserService>();
    private static CustomerOrderService customerOrderService = UserMenuController.GetService<CustomerOrderService>();
    private static OrderDetailService orderDetailService = UserMenuController.GetService<OrderDetailService>();
    private static ProductService productService = UserMenuController.GetService<ProductService>();
    private static OrderStateService orderStateService = UserMenuController.GetService<OrderStateService>();
    private static ProductTitleService productTitleService = UserMenuController.GetService<ProductTitleService>();
    private static ManufacturerService manufacturerService = UserMenuController.GetService<ManufacturerService>();

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
                (ConsoleKey.F2, "Show product list", () => ProductController.ShowAllProducts(productService, productTitleService, manufacturerService)),
                (ConsoleKey.F3, "Show order list", () => ShopController.ShowAllOrders(customerOrderService, orderStateService)),
                (ConsoleKey.F4, "Create new order", () => ShopController.AddOrder(customerOrderService, productService, orderDetailService)),
                (ConsoleKey.F5, "Cancel order", () => ShopController.CancelOrder(customerOrderService)),
                (ConsoleKey.F6, "Confirm order delivery", () => ShopController.ConfirmOrderDelivery(customerOrderService)),
                (ConsoleKey.F7, "Update personal information", () => UserController.UpdateUser(userService)),
            };
        return array;
    }
}