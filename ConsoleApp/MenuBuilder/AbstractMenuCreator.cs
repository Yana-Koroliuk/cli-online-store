using StoreDAL.Data;

namespace ConsoleMenu.Builder;

/// <summary>
/// Abstract class for creating menus.
/// </summary>
public abstract class AbstractMenuCreator : IMenuCreator
{
    /// <summary>
    /// Gets the menu items.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <returns>An array of menu items.</returns>
    public abstract (ConsoleKey id, string caption, Action action)[] GetMenuItems(StoreDbContext context);

    /// <summary>
    /// Creates a menu.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <returns>The created menu.</returns>
    public Menu Create(StoreDbContext context)
    {
        return new Menu(this.GetMenuItems(context));
    }
}