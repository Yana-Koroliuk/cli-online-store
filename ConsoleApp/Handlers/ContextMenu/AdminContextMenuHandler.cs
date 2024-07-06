namespace ConsoleApp.Handlers.ContextMenu;
using ConsoleApp.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreBLL.Interfaces;
using StoreBLL.Models;

/// <summary>
/// Context menu handler for administrators.
/// </summary>
public class AdminContextMenuHandler : ContextMenuHandler
{
    private readonly ICrud service;
    private readonly Func<AbstractModel> readModel;

    /// <summary>
    /// Initializes a new instance of the <see cref="AdminContextMenuHandler"/> class.
    /// </summary>
    /// <param name="service">The CRUD service.</param>
    /// <param name="readModel">The function to read a model.</param>
    public AdminContextMenuHandler(ICrud service, Func<AbstractModel> readModel)
        : base(service, readModel)
    {
        this.service = service;
        this.readModel = readModel;
    }

    /// <summary>
    /// Adds an item.
    /// </summary>
    public void AddItem()
    {
        try
        {
            var record = this.readModel();
            this.service.Add(record);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
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
    /// Edits an item.
    /// </summary>
    public void EditItem()
    {
        try
        {
            var id = InputHelper.ReadIntInput("Input record ID that will be edited", "ID");
            Console.WriteLine("Edit the details:");
            var updatedRecord = this.readModel();
            updatedRecord.Id = id;
            this.service.Update(updatedRecord);
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
                (ConsoleKey.A, "Add Item", this.AddItem),
                (ConsoleKey.R, "Remove Item", this.RemoveItem),
                (ConsoleKey.E, "Edit Item", this.EditItem),
                (ConsoleKey.V, "View Details", this.GetItemDetails),
            };
        return array;
    }
}
