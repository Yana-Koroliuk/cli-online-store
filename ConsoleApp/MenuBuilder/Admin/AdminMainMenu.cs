using ConsoleApp.Controllers;
using ConsoleApp.Services;
using ConsoleApp1;
using StoreBLL.Services;
using StoreDAL.Data;

namespace ConsoleMenu.Builder;

/// <summary>
/// Creates the main menu for the administrator.
/// </summary>
public class AdminMainMenu : AbstractMenuCreator
{
    private static UserService userService = UserMenuController.GetService<UserService>();
    private static UserRoleService userRoleService = UserMenuController.GetService<UserRoleService>();
    private static CustomerOrderService customerOrderService = UserMenuController.GetService<CustomerOrderService>();
    private static ProductService productService = UserMenuController.GetService<ProductService>();
    private static OrderStateService orderStateService = UserMenuController.GetService<OrderStateService>();
    private static ProductTitleService productTitleService = UserMenuController.GetService<ProductTitleService>();
    private static CategoryService categoryService = UserMenuController.GetService<CategoryService>();
    private static ManufacturerService manufacturerService = UserMenuController.GetService<ManufacturerService>();

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
                (ConsoleKey.F2, "Show product list", () => ProductController.ShowAllProducts(productService, productTitleService, manufacturerService)),
                (ConsoleKey.F3, "Add product", () => ProductController.AddProduct(productService, productTitleService, categoryService)),
                (ConsoleKey.F4, "Update product", () => ProductController.UpdateProduct(productService, productTitleService, categoryService, manufacturerService)),
                (ConsoleKey.F5, "Create new order", () => ShopController.AddOrderByAdmin(customerOrderService)),
                (ConsoleKey.F6, "Show order list", () => ShopController.ShowAllOrders(customerOrderService, orderStateService)),
                (ConsoleKey.F7, "Change order status", () => ShopController.ChangeOrderStatus(customerOrderService, orderStateService)),
                (ConsoleKey.F8, "Show user list", () => UserController.ShowAllUsers(userService)),
                (ConsoleKey.F9, "Update user info", () => UserController.UpdateUser(userService)),
                (ConsoleKey.F10, "User roles", () => UserController.ShowAllUserRoles(userRoleService)),
                (ConsoleKey.F11, "Order states", () => ShopController.ShowAllOrderStates(orderStateService)),
            };
        return array;
    }
}