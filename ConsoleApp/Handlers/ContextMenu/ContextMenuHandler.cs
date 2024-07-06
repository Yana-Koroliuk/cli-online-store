namespace ConsoleApp.Handlers.ContextMenu;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp.Helpers;
using ConsoleApp1;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using StoreBLL.Interfaces;
using StoreBLL.Models;
using StoreDAL.Entities;

/// <summary>
/// Abstract base class for context menu handlers.
/// </summary>
public abstract class ContextMenuHandler
{
    private readonly ICrud service;

    /// <summary>
    /// Initializes a new instance of the <see cref="ContextMenuHandler"/> class.
    /// </summary>
    /// <param name="service">The CRUD service.</param>
    /// <param name="readModel">The function to read a model.</param>
    protected ContextMenuHandler(ICrud service, Func<AbstractModel> readModel)
    {
        this.service = service;
    }

    /// <summary>
    /// Gets the details of an item.
    /// </summary>
    public void GetItemDetails()
    {
        try
        {
            var id = InputHelper.ReadIntInput("Input record ID for more details", "ID");
            Console.WriteLine(this.service.GetById(id));
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
    public abstract (ConsoleKey id, string caption, Action action)[] GenerateMenuItems();
}
