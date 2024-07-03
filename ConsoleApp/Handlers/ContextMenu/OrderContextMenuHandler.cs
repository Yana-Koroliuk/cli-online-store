namespace ConsoleApp.Handlers.ContextMenu;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreBLL.Interfaces;
using StoreBLL.Models;

/// <summary>
/// Context menu handler for orders.
/// </summary>
public class OrderContextMenuHandler : ContextMenuHandler
{
    /// <summary>
    /// Initializes a new instance of the <see cref="OrderContextMenuHandler"/> class.
    /// </summary>
    /// <param name="service">The CRUD service.</param>
    /// <param name="readModel">The function to read a model.</param>
    public OrderContextMenuHandler(ICrud service, Func<AbstractModel> readModel)
        : base(service, readModel)
    {
    }

    /// <summary>
    /// Removes an item.
    /// </summary>
    public void RemoveItem()
    {
        Console.WriteLine("Input record ID that will be removed");
        var idInput = Console.ReadLine();
        if (int.TryParse(idInput, out var id))
        {
            this.service.Delete(id);
        }
    }

    /// <summary>
    /// Edits an item.
    /// </summary>
    public void EditItem()
    {
        Console.WriteLine("Input record ID that will be edited");
        var idInput = Console.ReadLine();
        if (int.TryParse(idInput, out var id))
        {
            var record = this.readModel();
            this.service.Update(record);
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
                 (ConsoleKey.V, "View Details", this.GetItemDetails),
                 (ConsoleKey.V, "Change order status", this.EditItem),
            };
        return array;
    }
}
