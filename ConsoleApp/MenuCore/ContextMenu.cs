using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp.Controllers;
using ConsoleApp.Handlers.ContextMenu;
using ConsoleApp1;
using StoreBLL.Interfaces;
using StoreBLL.Models;

namespace ConsoleMenu
{
    /// <summary>
    /// Represents a context menu with dynamic content.
    /// </summary>
    public class ContextMenu : Menu
    {
        private readonly Func<IEnumerable<AbstractModel>> getAll;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContextMenu"/> class.
        /// </summary>
        /// <param name="controller">The context menu handler.</param>
        /// <param name="getAll">The function to get all records.</param>
        public ContextMenu(AdminContextMenuHandler controller, Func<IEnumerable<AbstractModel>> getAll)
            : base(controller?.GenerateMenuItems() ?? throw new ArgumentNullException(nameof(controller)))
        {
            this.getAll = getAll ?? throw new ArgumentNullException(nameof(getAll));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ContextMenu"/> class.
        /// </summary>
        /// <param name="generateMenuItems">The function to generate menu items.</param>
        /// <param name="getAll">The function to get all records.</param>
        public ContextMenu(Func<(ConsoleKey id, string caption, Action action)[]> generateMenuItems, Func<IEnumerable<AbstractModel>> getAll)
            : base((generateMenuItems ?? throw new ArgumentNullException(nameof(generateMenuItems)))())
        {
            this.getAll = getAll ?? throw new ArgumentNullException(nameof(getAll));
        }

        /// <summary>
        /// Runs the context menu, displaying records and handling input.
        /// </summary>
        public override void Run()
        {
            ConsoleKey resKey;
            bool updateItems = true;
            do
            {
                if (updateItems)
                {
                    Console.WriteLine("======= Current DataSet ==========");
                    foreach (var record in this.getAll())
                    {
                        Console.WriteLine(record);
                    }

                    Console.WriteLine("===================================");
                }

                resKey = this.RunOnce(ref updateItems);
            }
            while (resKey != ConsoleKey.Escape);
        }
    }
}
