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
/// Abstract base class for context menu handlers.
/// </summary>
public abstract class ContextMenuHandler
{
    protected readonly ICrud service;
    protected readonly Func<AbstractModel> readModel;

    /// <summary>
    /// Initializes a new instance of the <see cref="ContextMenuHandler"/> class.
    /// </summary>
    /// <param name="service">The CRUD service.</param>
    /// <param name="readModel">The function to read a model.</param>
    protected ContextMenuHandler(ICrud service, Func<AbstractModel> readModel)
    {
        this.service = service;
        this.readModel = readModel;
    }

    /// <summary>
    /// Gets the details of an item.
    /// </summary>
    public void GetItemDetails()
    {
        Console.WriteLine("Input record ID for more details");
        var idInput = Console.ReadLine();
        if (int.TryParse(idInput, out var id))
        {
            Console.WriteLine(this.service.GetById(id));
        }
    }

    /// <summary>
    /// Generates the menu items.
    /// </summary>
    /// <returns>An array of menu items.</returns>
    public abstract (ConsoleKey id, string caption, Action action)[] GenerateMenuItems();
}
