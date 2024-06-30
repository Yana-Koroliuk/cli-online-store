using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp.Controllers;
using ConsoleApp.Handlers.ContextMenu;
using ConsoleApp.Helpers;
using ConsoleApp1;
using ConsoleMenu;
using StoreBLL.Models;
using StoreBLL.Services;
using StoreDAL.Data;
using StoreDAL.Entities;

namespace ConsoleApp.Services
{
    /// <summary>
    /// Provides methods for managing shop-related operations.
    /// </summary>
    public static class ShopController
    {
        private static StoreDbContext context = UserMenuController.Context;

        /// <summary>
        /// Adds a new order for the current user.
        /// </summary>
        public static void AddOrder()
        {
            var customerOrderService = new CustomerOrderService(context);
            var orderDetailService = new OrderDetailService(context);
            var productService = new ProductService(context);

            Console.WriteLine("Create new order:");

            var orderDetails = new List<OrderDetailModel>();

            while (true)
            {
                Console.WriteLine("Add product details to order:");
                Console.WriteLine("Product ID: ");
                var productIdInput = Console.ReadLine();
                Console.WriteLine("Amount: ");
                var amountInput = Console.ReadLine();

                if (int.TryParse(productIdInput, out var productId) &&
                    int.TryParse(amountInput, out var amount))
                {
                    try
                    {
                        var product = (ProductModel)productService.GetById(productId);
                        var price = product.UnitPrice * amount;
                        orderDetails.Add(new OrderDetailModel(0, 0, productId, price, amount));
                    }
                    catch
                    {
                        Console.WriteLine("Product not found. Please try again.");
                        continue;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please try again.");
                    continue;
                }

                Console.WriteLine("Add another product? (yes/no)");
                var another = Console.ReadLine();
                if (another != null && another != "yes")
                {
                    break;
                }
            }

            var newOrder = new CustomerOrderModel(0, DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture), UserMenuController.UserId, 1);
            customerOrderService.Add(newOrder);
            newOrder = (CustomerOrderModel)customerOrderService.GetAll().Last();

            foreach (var detail in orderDetails)
            {
                detail.OrderId = newOrder.Id;
                orderDetailService.Add(detail);
            }

            Console.WriteLine("Order created successfully.");
        }

        public static void UpdateOrder()
        {
            throw new NotImplementedException();
        }

        public static void DeleteOrder()
        {
            throw new NotImplementedException();
        }

        public static void ShowOrder()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Shows all orders for the current user or all users if the user is an administrator.
        /// </summary>
        public static void ShowAllOrders()
        {
            var customerOrderService = new CustomerOrderService(context);
            var orderStateService = new OrderStateService(context);
            List<CustomerOrderModel> orders;

            if (UserMenuController.UserRole == UserRoles.Administrator)
            {
                orders = customerOrderService.GetAll().OfType<CustomerOrderModel>().ToList();
            }
            else
            {
                orders = customerOrderService.GetAll().OfType<CustomerOrderModel>().Where(o => o.UserId == UserMenuController.UserId).ToList();
            }

            Console.WriteLine(UserMenuController.UserRole == UserRoles.Administrator ? "All Orders:" : "Your Order History:");
            foreach (var order in orders)
            {
                var orderState = (OrderStateModel)orderStateService.GetById(order.OrderStateId);
                var statusName = orderState != null ? orderState.StateName : "Unknown Status";
                Console.WriteLine($"Order ID: {order.Id}, Date: {order.OperationTime}, Status: {statusName}");
            }
        }

        /// <summary>
        /// Confirms the delivery of an order by the user.
        /// </summary>
        public static void ConfirmOrderDelivery()
        {
            var orderService = new CustomerOrderService(context);
            Console.WriteLine("Enter order ID to confirm delivery:");
            var orderIdInput = Console.ReadLine();
            if (int.TryParse(orderIdInput, out var orderId))
            {
                try
                {
                    var order = (CustomerOrderModel)orderService.GetById(orderId);
                    if (order.UserId != UserMenuController.UserId)
                    {
                        Console.WriteLine("You can only confirm delivery for your own orders.");
                        return;
                    }

                    if (order.OrderStateId == 7)
                    {
                        order.OrderStateId = 8;
                        orderService.Update(order);
                        Console.WriteLine("Order delivery confirmed successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Order cannot be confirmed at this stage.");
                    }
                }
                catch
                {
                    Console.WriteLine("Order not found. Please try again.");
                }
            }
        }

        /// <summary>
        /// Cancels an order by the user.
        /// </summary>
        public static void CancelOrder()
        {
            var orderService = new CustomerOrderService(context);
            Console.WriteLine("Enter order ID to cancel:");
            var orderIdInput = Console.ReadLine();
            if (int.TryParse(orderIdInput, out var orderId))
            {
                try
                {
                    var order = (CustomerOrderModel)orderService.GetById(orderId);
                    if (order.UserId != UserMenuController.UserId)
                    {
                        Console.WriteLine("You can only cancel your own orders.");
                        return;
                    }

                    if (order.OrderStateId == 1 || order.OrderStateId == 4 || order.OrderStateId == 7)
                    {
                        order.OrderStateId = 2;
                        orderService.Update(order);
                        Console.WriteLine("Order cancelled successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Order cannot be cancelled at this stage.");
                    }
                }
                catch
                {
                    Console.WriteLine("Order not found. Please try again.");
                }
            }
        }

        public static void AddOrderDetails()
        {
            throw new NotImplementedException();
        }

        public static void UpdateOrderDetails()
        {
            throw new NotImplementedException();
        }

        public static void DeleteOrderDetails()
        {
            throw new NotImplementedException();
        }

        public static void ShowAllOrderDetails()
        {
            throw new NotImplementedException();
        }

        public static void ProcessOrder()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Shows all possible order states.
        /// </summary>
        public static void ShowAllOrderStates()
        {
            var service = new OrderStateService(context);
            var menu = new ContextMenu(new AdminContextMenuHandler(service, InputHelper.ReadOrderStateModel), service.GetAll);
            menu.Run();
        }
    }
}
