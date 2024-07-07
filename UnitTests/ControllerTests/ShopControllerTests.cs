using ConsoleApp.Services;
using ConsoleApp1;
using Moq;
using StoreBLL.Interfaces;
using StoreBLL.Models;
using StoreBLL.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Xunit;

namespace UnitTests.ControllerTests
{
    /// <summary>
    /// Unit tests for the <see cref="ShopController"/> class.
    /// </summary>
    public class ShopControllerTests
    {
        private readonly Mock<ICrud> _mockCustomerOrderService;
        private readonly Mock<ICrud> _mockProductService;
        private readonly Mock<ICrud> _mockOrderDetailService;
        private readonly Mock<IOrderStateService> _mockOrderStateService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ShopControllerTests"/> class.
        /// </summary>
        public ShopControllerTests()
        {
            _mockCustomerOrderService = new Mock<ICrud>();
            _mockProductService = new Mock<ICrud>();
            _mockOrderDetailService = new Mock<ICrud>();
            _mockOrderStateService = new Mock<IOrderStateService>();
        }

        /// <summary>
        /// Tests the <see cref="ShopController.AddOrder(CustomerOrderService, ProductService, OrderDetailService)"/> method to ensure it adds a new order.
        /// </summary>
        [Fact]
        public void AddOrder_ShouldAddNewOrder()
        {
            _mockProductService.Setup(s => s.GetById(It.IsAny<int>())).Returns(new ProductModel(1, 1, 1, "Product1", 100));
            _mockCustomerOrderService.Setup(s => s.GetAll()).Returns(new List<AbstractModel> { new CustomerOrderModel(1, DateTime.Now.ToString(CultureInfo.InvariantCulture), 1, 1) });

            Console.SetIn(new StringReader("1\n1\n")); 

            ShopController.AddOrder(_mockCustomerOrderService.Object, _mockProductService.Object, _mockOrderDetailService.Object);

            _mockCustomerOrderService.Verify(s => s.Add(It.IsAny<AbstractModel>()), Times.Once);
            _mockOrderDetailService.Verify(s => s.Add(It.IsAny<AbstractModel>()), Times.AtLeastOnce);
        }

        /// <summary>
        /// Tests the <see cref="ShopController.ShowAllOrders(CustomerOrderService, OrderStateService)"/> method to ensure it displays all orders.
        /// </summary>
        [Fact]
        public void ShowAllOrders_ShouldDisplayAllOrders()
        {
            var orders = new List<CustomerOrderModel>
            {
                new CustomerOrderModel(1, "2023-07-07T12:34:56", 1, 1),
                new CustomerOrderModel(2, "2023-07-07T12:34:56", 2, 1)
            };
            _mockCustomerOrderService.Setup(s => s.GetAll()).Returns(orders);
            _mockOrderStateService.Setup(s => s.GetById(It.IsAny<int>())).Returns(new OrderStateModel(1, "Pending"));

            ShopController.ShowAllOrders(_mockCustomerOrderService.Object, _mockOrderStateService.Object);

            _mockCustomerOrderService.Verify(s => s.GetAll(), Times.Once);       
        }

        /// <summary>
        /// Tests the <see cref="ShopController.ConfirmOrderDelivery(ICrud)"/> method to ensure it handles incorrect user trying to confirm delivery.
        /// </summary>
        [Fact]
        public void ConfirmOrderDelivery_ShouldHandleIncorrectUser()
        {
            var order = new CustomerOrderModel(1, DateTime.Now.ToString(CultureInfo.InvariantCulture), 2, 7);
            _mockCustomerOrderService.Setup(s => s.GetById(It.IsAny<int>())).Returns(order);

            Console.SetIn(new StringReader("1\n"));

            ShopController.ConfirmOrderDelivery(_mockCustomerOrderService.Object);

            _mockCustomerOrderService.Verify(s => s.Update(It.IsAny<AbstractModel>()), Times.Never);
        }

        /// <summary>
        /// Tests the <see cref="ShopController.CancelOrder(ICrud)"/> method to ensure it handles incorrect user trying to cancel the order.
        /// </summary>
        [Fact]
        public void CancelOrder_ShouldHandleIncorrectUser()
        {
            var order = new CustomerOrderModel(1, DateTime.Now.ToString(CultureInfo.InvariantCulture), 2, 1);
            _mockCustomerOrderService.Setup(s => s.GetById(It.IsAny<int>())).Returns(order);

            Console.SetIn(new StringReader("1\n"));

            ShopController.CancelOrder(_mockCustomerOrderService.Object);

            _mockCustomerOrderService.Verify(s => s.Update(It.IsAny<AbstractModel>()), Times.Never);
        }

        /// <summary>
        /// Tests the <see cref="ShopController.ChangeOrderStatus(ICrud, IOrderStateService)"/> method to ensure it changes the order status successfully.
        /// </summary>
        [Fact]
        public void ChangeOrderStatus_ShouldChangeOrderStatusSuccessfully()
        {
            var order = new CustomerOrderModel(1, "2023-01-01T12:00:00", 1, 1);
            var allowedStatusIds = new List<int> { 2, 4 };
            _mockCustomerOrderService.Setup(cos => cos.GetById(It.IsAny<int>())).Returns(order);
            _mockOrderStateService.Setup(oss => oss.GetChangeToStatusIds(It.IsAny<int>())).Returns(allowedStatusIds);
            _mockOrderStateService.Setup(oss => oss.GetAll()).Returns(new List<OrderStateModel>
            {
                new OrderStateModel(1, "New Order"),
                new OrderStateModel(2, "Confirmed"),
            });
            _mockCustomerOrderService.Setup(cos => cos.Update(It.IsAny<CustomerOrderModel>())).Verifiable();

            var input = new StringReader("1\nConfirmed\n");
            Console.SetIn(input);

            ShopController.ChangeOrderStatus(_mockCustomerOrderService.Object, _mockOrderStateService.Object);

            _mockCustomerOrderService.Verify(cos => cos.Update(It.Is<CustomerOrderModel>(o => o.OrderStateId == 2)), Times.Once);
        }
    }
}
