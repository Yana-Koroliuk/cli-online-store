/*
Yuriy Antonov copyright 2018-2020
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMenu
{
    /// <summary>
    /// Represents a menu item.
    /// </summary>
    public class MenuItem
    {
        private readonly string caption;
        private readonly Action action;

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuItem"/> class.
        /// </summary>
        /// <param name="caption">The caption of the menu item.</param>
        /// <param name="action">The action to perform when the menu item is selected.</param>
        public MenuItem(string caption, Action action)
        {
            this.caption = caption;
            this.action = action;
        }

        /// <summary>
        /// Gets the caption of the menu item.
        /// </summary>
        public string Caption
        {
            get { return this.caption; }
        }

        /// <summary>
        /// Gets the action of the menu item.
        /// </summary>
        public Action Action
        {
            get { return this.action; }
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return this.caption;
        }
    }
}