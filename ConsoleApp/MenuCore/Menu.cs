using System.Linq;
/*
Yuriy Antonov copyright 2018-2020
*/

namespace ConsoleMenu
{
    /// <summary>
    /// Represents a menu with selectable items.
    /// </summary>
    public class Menu
    {
        private readonly Dictionary<ConsoleKey, MenuItem> items;

        /// <summary>
        /// Initializes a new instance of the <see cref="Menu"/> class.
        /// </summary>
        public Menu()
        {
            this.items = new Dictionary<ConsoleKey, MenuItem>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Menu"/> class with a single menu item.
        /// </summary>
        /// <param name="id">The key to select the menu item.</param>
        /// <param name="caption">The caption of the menu item.</param>
        /// <param name="action">The action to perform when the menu item is selected.</param>
        public Menu(ConsoleKey id, string caption, Action action)
        {
            this.items = new Dictionary<ConsoleKey, MenuItem>();
            this.items.Add(id, new MenuItem(caption, action));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Menu"/> class with an array of menu items.
        /// </summary>
        /// <param name="array">An array of menu items.</param>
        public Menu((ConsoleKey id, string caption, Action action)[] array)
        {
            this.items = new Dictionary<ConsoleKey, MenuItem>();
            foreach (var elem in from elem in array
                                 where !this.items.ContainsKey(elem.id)
                                 select elem)
            {
                this.items.Add(elem.id, new MenuItem(elem.caption, elem.action));
            }
        }

        /// <summary>
        /// Runs the menu, displaying it and handling input.
        /// </summary>
        public virtual void Run()
        {
            ConsoleKey resKey;
            bool updateItems = true;
            do
            {
                resKey = this.RunOnce(ref updateItems);
            }
            while (resKey != ConsoleKey.Escape);
        }

        /// <summary>
        /// Runs the menu once, displaying it and handling input.
        /// </summary>
        /// <param name="updateItems">Indicates whether to update items display.</param>
        /// <returns>The key pressed.</returns>
        public ConsoleKey RunOnce(ref bool updateItems)
        {
            ConsoleKeyInfo res;
            if (updateItems)
            {
                    foreach (var item in this.items)
                    {
                        Console.WriteLine($"<{item.Key}>:  {item.Value}");
                    }

                    Console.WriteLine("Or press <Esc> to return");
            }

            res = Console.ReadKey(true);
            if (this.items.TryGetValue(res.Key, out MenuItem? value))
            {
                value.Action();
                updateItems = true;
            }
            else
            {
                updateItems = false;
            }

            return res.Key;
        }

        /// <summary>
        /// Runs the old version of the menu.
        /// </summary>
        public void RunOld()
        {
            ConsoleKeyInfo res;
            bool updateItems = true;
            do
            {
                if (updateItems)
                {
                    foreach (var item in this.items)
                    {
                        Console.WriteLine($"<{item.Key}>:  {item.Value}");
                    }

                    Console.WriteLine("Or press <Esc> to return");
                }

                res = Console.ReadKey(true);
                if (this.items.TryGetValue(res.Key, out MenuItem? value))
                {
                    value.Action();
                    updateItems = true;
                }
                else
                {
                    updateItems = false;
                }
            }
            while (res.Key != ConsoleKey.Escape);
        }
    }
}