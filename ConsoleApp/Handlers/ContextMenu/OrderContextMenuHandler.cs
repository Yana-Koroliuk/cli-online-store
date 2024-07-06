namespace ConsoleApp.Handlers.ContextMenu;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp.Helpers;
using ConsoleApp.Services;
using StoreBLL.Interfaces;
using StoreBLL.Models;

/// <summary>
/// Context menu handler for orders.
/// </summary>
public class OrderContextMenuHandler : ContextMenuHandler
{
    private readonly ICrud service;

    /// <summary>
    /// Initializes a new instance of the <see cref="OrderContextMenuHandler"/> class.
    /// </summary>
    /// <param name="service">The CRUD service.</param>
    /// <param name="readModel">The function to read a model.</param>
    public OrderContextMenuHandler(ICrud service, Func<AbstractModel> readModel)
        : base(service, readModel)
    {
        this.service = service;
    }

    /// <summary>
    /// Edits an item.
    /// </summary>
    public static void EditItem()
    {
        ShopController.ChangeOrderStatus();
    }

    /// <summary>
    /// Removes an item.
    /// </summary>
    public void RemoveItem()
    {
        try
        {
            var id = InputHelper.ReadIntInput("Input record ID that will be removed", "ID");
            this.service.GetById(id);
            this.service.Delete(id);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    /// <summary>
    /// Generates the menu items.
    /// </summary>
    /// <returns>An array of menu items.</returns>
    public override (ConsoleKey id, string caption, Action action)[] GenerateMenuItems()
    {
        (ConsoleKey id, string caption, Action action)[] array =
            {
                 (ConsoleKey.A, "Change order status", EditItem),
                 (ConsoleKey.R, "Remove order", this.RemoveItem),
                 (ConsoleKey.V, "View Details", this.GetItemDetails),
            };
        return array;
    }
}
