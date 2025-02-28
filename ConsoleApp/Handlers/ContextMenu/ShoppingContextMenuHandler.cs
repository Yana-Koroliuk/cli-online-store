﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp.Services;
using ConsoleApp1;
using StoreBLL.Interfaces;
using StoreBLL.Models;
using StoreBLL.Services;

namespace ConsoleApp.Handlers.ContextMenu
{
    /// <summary>
    /// Context menu handler for shopping.
    /// </summary>
    public class ShoppingContextMenuHandler : ContextMenuHandler
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ShoppingContextMenuHandler"/> class.
        /// </summary>
        /// <param name="service">The CRUD service.</param>
        /// <param name="readModel">The function to read a model.</param>
        public ShoppingContextMenuHandler(ICrud service, Func<AbstractModel> readModel)
            : base(service, readModel)
        {
        }

        public static void CreateOrder()
        {
            CustomerOrderService customerOrderService = UserMenuController.GetService<CustomerOrderService>();
            OrderDetailService orderDetailService = UserMenuController.GetService<OrderDetailService>();
            ProductService productService = UserMenuController.GetService<ProductService>();
            ShopController.AddOrder(customerOrderService, productService, orderDetailService);
        }

        /// <summary>
        /// Generates the menu items.
        /// </summary>
        /// <returns>An array of menu items.</returns>
        public override (ConsoleKey id, string caption, Action action)[] GenerateMenuItems()
        {
            (ConsoleKey id, string caption, Action action)[] array =
                {
                     (ConsoleKey.A, "Create order", CreateOrder),
                     (ConsoleKey.V, "View Details", this.GetItemDetails),
                };
            return array;
        }
    }
}
