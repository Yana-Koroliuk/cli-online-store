using Moq;
using StoreBLL.Interfaces;
using StoreBLL.Models;
using StoreBLL.Services;
using StoreDAL.Entities;
using StoreDAL.Interfaces;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace UnitTests.ServiceTests
{
    /// <summary>
    /// Unit tests for the <see cref="OrderStateService "/> class.
    /// </summary>
    public class OrderStateServiceTests
    {
        private readonly Mock<IOrderStateRepository> mockRepository;
        private readonly OrderStateService orderStateService;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderStateServiceTests"/> class.
        /// Sets up the mock repository and the OrderStateService instance.
        /// </summary>
        public OrderStateServiceTests()
        {
            mockRepository = new Mock<IOrderStateRepository>();
            orderStateService = new OrderStateService(mockRepository.Object);
        }

        /// <summary>
        /// Tests the Add method to ensure an order state is added correctly.
        /// </summary>
        [Fact]
        public void Add_ShouldAddOrderState()
        {
            var orderStateModel = new OrderStateModel(0, "New Order");

            orderStateService.Add(orderStateModel);

            mockRepository.Verify(r => r.Add(It.IsAny<OrderState>()), Times.Once);
        }

        /// <summary>
        /// Tests the Delete method to ensure an order state is deleted by ID correctly.
        /// </summary>
        [Fact]
        public void Delete_ShouldDeleteOrderState()
        {
            var orderStateId = 1;

            orderStateService.Delete(orderStateId);

            mockRepository.Verify(r => r.DeleteById(1), Times.Once);
        }

        /// <summary>
        /// Tests the GetAll method to ensure all order states are retrieved correctly.
        /// </summary>
        [Fact]
        public void GetAll_ShouldReturnAllOrderStates()
        {
            var orderStates = new List<OrderState>
            {
                new OrderState(1, "Confirmed"),
                new OrderState(2, "In delivery")
            };
            mockRepository.Setup(r => r.GetAll()).Returns(orderStates);

            var result = orderStateService.GetAll();

            Assert.Equal(2, result.Count());
            Assert.Contains(result, os => ((OrderStateModel)os).StateName == "Confirmed");
            Assert.Contains(result, os => ((OrderStateModel)os).StateName == "In delivery");
        }

        /// <summary>
        /// Tests the GetById method to ensure an order state is retrieved by ID correctly.
        /// </summary>
        [Fact]
        public void GetById_ShouldReturnOrderState()
        {
            var orderState = new OrderState(1, "Moved to delivery company");
            mockRepository.Setup(r => r.GetById(1)).Returns(orderState);

            var result = (OrderStateModel)orderStateService.GetById(1);

            Assert.NotNull(result);
            Assert.Equal("Moved to delivery company", result.StateName);
        }

        /// <summary>
        /// Tests the GetChangeToStatusIds method to ensure the correct allowed status IDs are retrieved.
        /// </summary>
        [Fact]
        public void GetChangeToStatusIds_ShouldReturnAllowedStatusIds()
        {
            var orderStates = new List<OrderState>
            {
                new OrderState(1, "New Order"),
                new OrderState(2, "Cancelled by user"),
                new OrderState(3, "Cancelled by administrator"),
                new OrderState(4, "Confirmed"),
                new OrderState(5, "Moved to delivery company"),
                new OrderState(6, "In delivery"),
                new OrderState(7, "Delivered to client"),
                new OrderState(8, "Delivery confirmed by client"),
            };
            mockRepository.Setup(r => r.GetAll()).Returns(orderStates);

            var result = orderStateService.GetChangeToStatusIds(1);

            Assert.Equal(2, result.Count);
            Assert.Contains(4, result);
            Assert.Contains(3, result);
        }

        /// <summary>
        /// Tests the Update method to ensure an order state is updated correctly.
        /// </summary>
        [Fact]
        public void Update_ShouldUpdateOrderState()
        {
            var orderStateModel = new OrderStateModel(1, "Confirmed");
            var orderState = new OrderState(1, "New Order");
            mockRepository.Setup(r => r.GetById(1)).Returns(orderState);

            orderStateService.Update(orderStateModel);

            mockRepository.Verify(r => r.Update(It.IsAny<OrderState>()), Times.Once);
            Assert.Equal("Confirmed", orderState.StateName);
        }
    }
}
