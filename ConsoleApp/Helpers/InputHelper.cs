namespace ConsoleApp.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreBLL.Models;

/// <summary>
/// Helper class for reading input.
/// </summary>
internal static class InputHelper
{
    /// <summary>
    /// Reads an OrderStateModel from input.
    /// </summary>
    /// <returns>The read OrderStateModel.</returns>
    public static OrderStateModel ReadOrderStateModel()
    {
        Console.WriteLine("Input State Id");
        var idInput = Console.ReadLine();
        var id = int.Parse(idInput ?? "0", CultureInfo.InvariantCulture);

        Console.WriteLine("Input State Name");
        var name = Console.ReadLine() ?? string.Empty;

        return new OrderStateModel(id, name);
    }

    /// <summary>
    /// Reads a UserRoleModel from input.
    /// </summary>
    /// <returns>The read UserRoleModel.</returns>
    public static UserRoleModel ReadUserRoleModel()
    {
        Console.WriteLine("Input User Role Id");
        var idInput = Console.ReadLine();
        var id = int.Parse(idInput ?? "0", CultureInfo.InvariantCulture);

        Console.WriteLine("Input User Role Name");
        var name = Console.ReadLine() ?? string.Empty;

        return new UserRoleModel(id, name);
    }
}
