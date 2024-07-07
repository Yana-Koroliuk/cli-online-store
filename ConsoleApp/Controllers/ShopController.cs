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
using Microsoft.CodeAnalysis;
using StoreBLL.Interfaces;
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
        /// <summary>
        /// Adds a new order for the current user.
        /// </summary>
        /// <param name="customerOrderService">Service for managing customer orders.</param>
        /// <param name="productService">Service for managing products.</param>
        /// <param name="orderDetailService">Service for managing order details.</param>>
        public static void AddOrder(ICrud customerOrderService, ICrud productService, ICrud orderDetailService)
        {
            var orderDetails = new List<OrderDetailModel>();
            Console.WriteLine("Create new order:");
            while (true)
            {
                try
                {
                    customerOrderService = customerOrderService ?? throw new ArgumentNullException(nameof(customerOrderService));
                    productService = productService ?? throw new ArgumentNullException(nameof(productService));
                    orderDetailService = orderDetailService ?? throw new ArgumentNullException(nameof(orderDetailService));
                    var orderDetail = InputHelper.ReadOrderDetailModel();
                    var product = (ProductModel)productService.GetById(orderDetail.ProductId);
                    var price = product.UnitPrice * orderDetail.ProductAmount;
                    orderDetail.Price = price;
                    orderDetails.Add(orderDetail);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    continue;
                }

                if (InputHelper.ReadProductsInOrder() == 0)
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

        /// <summary>
        /// Adds a new order by the administrator.
        /// </summary>
        /// <param name="customerOrderService">Service for managing customer orders.</param>
        public static void AddOrderByAdmin(ICrud customerOrderService)
        {
            Console.WriteLine("Create new order:");
            customerOrderService = customerOrderService ?? throw new ArgumentNullException(nameof(customerOrderService));
            var menu = new ContextMenu(new ShoppingContextMenuHandler(customerOrderService, InputHelper.ReadCustomerOrderModel).GenerateMenuItems, customerOrderService.GetAll);
            menu.Run();
        }

        /// <summary>
        /// Shows all orders for the current user or all users if the user is an administrator.
        /// </summary>
        /// <param name="customerOrderService">Service for managing customer orders.</param>
        /// <param name="orderStateService">Service for managing order states.</param>
        public static void ShowAllOrders(ICrud customerOrderService, ICrud orderStateService)
        {
            customerOrderService = customerOrderService ?? throw new ArgumentNullException(nameof(customerOrderService));
            orderStateService = orderStateService ?? throw new ArgumentNullException(nameof(orderStateService));
            if (UserMenuController.UserRole == UserRoles.Administrator)
            {
                Console.WriteLine("All Orders:");
                var menu = new ContextMenu(new OrderContextMenuHandler(customerOrderService, InputHelper.ReadCustomerOrderModel).GenerateMenuItems, customerOrderService.GetAll);
                menu.Run();
            }
            else
            {
                var orders = customerOrderService.GetAll().OfType<CustomerOrderModel>().Where(o => o.UserId == UserMenuController.UserId).ToList();
                Console.WriteLine("Your Order History:");
                foreach (var order in orders)
                {
                    var orderState = (OrderStateModel)orderStateService.GetById(order.OrderStateId);
                    var statusName = orderState != null ? orderState.StateName : "Unknown Status";
                    Console.WriteLine($"Order ID: {order.Id}, Date: {order.OperationTime}, Status: {statusName}");
                }
            }
        }

        /// <summary>
        /// Confirms the delivery of an order by the user.
        /// </summary>
        /// <param name="customerOrderService">Service for managing customer orders.</param>
        public static void ConfirmOrderDelivery(ICrud customerOrderService)
        {
            try
            {
                var orderId = InputHelper.ReadCustomerOrderId();
                customerOrderService = customerOrderService ?? throw new ArgumentNullException(nameof(customerOrderService));
                var order = (CustomerOrderModel)customerOrderService.GetById(orderId);
                if (order.UserId != UserMenuController.UserId)
                {
                    Console.WriteLine("You can only confirm delivery for your own orders.");
                    return;
                }

                if (order.OrderStateId == 7)
                {
                    order.OrderStateId = 8;
                    customerOrderService.Update(order);
                    Console.WriteLine("Order delivery confirmed successfully.");
                }
                else
                {
                    Console.WriteLine("Order cannot be confirmed at this stage.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Cancels an order by the user.
        /// </summary>
        /// <param name="customerOrderService">Service for managing customer orders.</param>
        public static void CancelOrder(ICrud customerOrderService)
        {
            try
            {
                var orderId = InputHelper.ReadCustomerOrderId();
                customerOrderService = customerOrderService ?? throw new ArgumentNullException(nameof(customerOrderService));
                var order = (CustomerOrderModel)customerOrderService.GetById(orderId);
                if (order.UserId != UserMenuController.UserId)
                {
                    Console.WriteLine("You can only cancel your own orders.");
                    return;
                }

                if (order.OrderStateId == 1 || order.OrderStateId == 4 || order.OrderStateId == 7)
                {
                    order.OrderStateId = 2;
                    customerOrderService.Update(order);
                    Console.WriteLine("Order cancelled successfully.");
                }
                else
                {
                    Console.WriteLine("Order cannot be cancelled at this stage.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Changes the status of an order by the administrator.
        /// </summary>
        /// <param name="customerOrderService">Service for managing customer orders.</param>
        /// <param name="orderStateService">Service for managing order states.</param>
        public static void ChangeOrderStatus(ICrud customerOrderService, OrderStateService orderStateService)
        {
            try
            {
                var orderId = InputHelper.ReadCustomerOrderId();
                customerOrderService = customerOrderService ?? throw new ArgumentNullException(nameof(customerOrderService));
                orderStateService = orderStateService ?? throw new ArgumentNullException(nameof(orderStateService));
                var order = (CustomerOrderModel)customerOrderService.GetById(orderId);
                var allowedStatusIds = orderStateService.GetChangeToStatusIds(order.OrderStateId);

                if (allowedStatusIds.Count == 0)
                {
                    Console.WriteLine("No allowed states to transition to.");
                    return;
                }

                var newStatusName = InputHelper.ReadStatusName();
                var newStatus = orderStateService.GetAll().OfType<OrderStateModel>().FirstOrDefault(os =>
                os.StateName.Equals(newStatusName, StringComparison.OrdinalIgnoreCase));

                if (newStatus != null && allowedStatusIds.Contains(newStatus.Id))
                {
                    order.OrderStateId = newStatus.Id;
                    customerOrderService.Update(order);
                    Console.WriteLine("Order status updated successfully.");
                }
                else
                {
                    Console.WriteLine("Invalid status selection.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Shows all possible order states.
        /// </summary>
        /// <param name="orderStateService">Service for managing order states.</param>
        public static void ShowAllOrderStates(ICrud orderStateService)
        {
            orderStateService = orderStateService ?? throw new ArgumentNullException(nameof(orderStateService));
            var menu = new ContextMenu(new AdminContextMenuHandler(orderStateService, InputHelper.ReadOrderStateModel), orderStateService.GetAll);
            menu.Run();
        }
    }
}
