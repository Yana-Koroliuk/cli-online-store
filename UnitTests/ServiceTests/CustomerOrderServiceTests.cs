using Moq;
using StoreBLL.Models;
using StoreBLL.Services;
using StoreDAL.Entities;
using StoreDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace UnitTests.ServiceTests
{
    /// <summary>
    /// Unit tests for the <see cref="CustomerOrderService"/> class.
    /// </summary>
    public class CustomerOrderServiceTests
    {
        private readonly Mock<ICustomerOrderRepository> mockRepository;
        private readonly CustomerOrderService customerOrderService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerOrderServiceTests"/> class.
        /// </summary>
        public CustomerOrderServiceTests()
        {
            mockRepository = new Mock<ICustomerOrderRepository>();
            customerOrderService = new CustomerOrderService(mockRepository.Object);
        }

        /// <summary>
        /// Tests the Add method of <see cref="CustomerOrderService"/> class.
        /// </summary>
        [Fact]
        public void Add_ShouldAddCustomerOrder()
        {
            var customerOrderModel = new CustomerOrderModel(0, DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss"), 1, 1);

            customerOrderService.Add(customerOrderModel);

            mockRepository.Verify(r => r.Add(It.IsAny<CustomerOrder>()), Times.Once);
        }

        /// <summary>
        /// Tests the Delete method of <see cref="CustomerOrderService"/> class.
        /// </summary>
        [Fact]
        public void Delete_ShouldDeleteCustomerOrder()
        {
            var orderId = 1;

            customerOrderService.Delete(orderId);

            mockRepository.Verify(r => r.DeleteById(orderId), Times.Once);
        }

        /// <summary>
        /// Tests the GetAll method of <see cref="CustomerOrderService"/> class.
        /// </summary>
        [Fact]
        public void GetAll_ShouldReturnAllCustomerOrders()
        {
            var orders = new List<CustomerOrder>
            {
                new CustomerOrder(1, "2023-07-01T12:00:00", 1, 1),
                new CustomerOrder(2, "2023-07-02T12:00:00", 2, 2)
            };
            mockRepository.Setup(r => r.GetAll()).Returns(orders);

            var result = customerOrderService.GetAll();

            Assert.Equal(2, result.Count());
            Assert.IsType<CustomerOrderModel>(result.First());
        }

        /// <summary>
        /// Tests the GetById method of <see cref="CustomerOrderService"/> class.
        /// </summary>
        [Fact]
        public void GetById_ShouldReturnCustomerOrder()
        {
            var order = new CustomerOrder(1, "2023-07-01T12:00:00", 1, 1);
            mockRepository.Setup(r => r.GetById(1)).Returns(order);

            var result = customerOrderService.GetById(1);

            Assert.NotNull(result);
            Assert.Equal(1, ((CustomerOrderModel)result).UserId);
        }

        /// <summary>
        /// Tests the Update method of <see cref="CustomerOrderService"/> class.
        /// </summary>
        [Fact]
        public void Update_ShouldUpdateCustomerOrder()
        {
            var customerOrderModel = new CustomerOrderModel(1, "2023-07-01T12:00:00", 1, 1);
            var customerOrder = new CustomerOrder(1, "2022-07-01T12:00:00", 2, 1);
            mockRepository.Setup(r => r.GetById(1)).Returns(customerOrder);

            customerOrderService.Update(customerOrderModel);

            mockRepository.Verify(r => r.Update(It.Is<CustomerOrder>(co =>
                co.OperationTime == customerOrderModel.OperationTime &&
                co.UserId == customerOrderModel.UserId &&
                co.OrderStateId == customerOrderModel.OrderStateId
            )), Times.Once);
        }
    }
}
