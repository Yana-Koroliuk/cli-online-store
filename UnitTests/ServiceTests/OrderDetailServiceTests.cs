using Moq;
using StoreBLL.Models;
using StoreBLL.Services;
using StoreDAL.Entities;
using StoreDAL.Interfaces;
using Xunit;
using System.Collections.Generic;
using System.Linq;

namespace UnitTests.ServiceTests
{
    /// <summary>
    /// Unit tests for the <see cref="OrderDetailService"/> class.
    /// </summary>
    public class OrderDetailServiceTests
    {
        private readonly Mock<IOrderDetailRepository> mockRepository;
        private readonly OrderDetailService orderDetailService;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderDetailServiceTests"/> class.
        /// </summary>
        public OrderDetailServiceTests()
        {
            mockRepository = new Mock<IOrderDetailRepository>();
            orderDetailService = new OrderDetailService(mockRepository.Object);
        }

        /// <summary>
        /// Tests the Add method of <see cref="OrderDetailService"/> class.
        /// </summary>
        [Fact]
        public void Add_ShouldAddOrderDetail()
        {
            var orderDetailModel = new OrderDetailModel(0, 1, 1, 100m, 2);

            orderDetailService.Add(orderDetailModel);

            mockRepository.Verify(r => r.Add(It.Is<OrderDetail>(o =>
                o.OrderId == orderDetailModel.OrderId &&
                o.ProductId == orderDetailModel.ProductId &&
                o.Price == orderDetailModel.Price &&
                o.ProductAmount == orderDetailModel.ProductAmount)), Times.Once);
        }

        /// <summary>
        /// Tests the Delete method of <see cref="OrderDetailService"/> class.
        /// </summary>
        [Fact]
        public void Delete_ShouldDeleteOrderDetail()
        {
            var orderDetailId = 1;

            orderDetailService.Delete(orderDetailId);

            mockRepository.Verify(r => r.DeleteById(orderDetailId), Times.Once);
        }

        /// <summary>
        /// Tests the GetAll method of <see cref="OrderDetailService"/> class.
        /// </summary>
        [Fact]
        public void GetAll_ShouldReturnAllOrderDetails()
        {
            var orderDetails = new List<OrderDetail>
            {
                new OrderDetail(1, 1, 1, 100m, 2),
                new OrderDetail(2, 2, 2, 200m, 3)
            };
            mockRepository.Setup(r => r.GetAll()).Returns(orderDetails);

            var result = orderDetailService.GetAll();

            Assert.Equal(2, result.Count());
            Assert.Equal(1, ((OrderDetailModel)result.ElementAt(0)).OrderId);
            Assert.Equal(2, ((OrderDetailModel)result.ElementAt(1)).OrderId);
        }

        /// <summary>
        /// Tests the GetById method of <see cref="OrderDetailService"/> class.
        /// </summary>
        [Fact]
        public void GetById_ShouldReturnOrderDetail()
        {
            var orderDetail = new OrderDetail(1, 1, 1, 100m, 2);
            mockRepository.Setup(r => r.GetById(1)).Returns(orderDetail);

            var result = orderDetailService.GetById(1);

            Assert.Equal(1, ((OrderDetailModel)result).OrderId);
            Assert.Equal(1, ((OrderDetailModel)result).ProductId);
            Assert.Equal(100m, ((OrderDetailModel)result).Price);
            Assert.Equal(2, ((OrderDetailModel)result).ProductAmount);
        }

        /// <summary>
        /// Tests the Update method of <see cref="OrderDetailService"/> class.
        /// </summary>
        [Fact]
        public void Update_ShouldUpdateOrderDetail()
        {
            var orderDetailModel = new OrderDetailModel(1, 1, 1, 150m, 3);
            var existingOrderDetail = new OrderDetail(1, 1, 1, 100m, 2);

            mockRepository.Setup(r => r.GetById(orderDetailModel.Id)).Returns(existingOrderDetail);

            orderDetailService.Update(orderDetailModel);

            mockRepository.Verify(r => r.Update(It.Is<OrderDetail>(o =>
                o.Id == orderDetailModel.Id &&
                o.OrderId == orderDetailModel.OrderId &&
                o.ProductId == orderDetailModel.ProductId &&
                o.Price == orderDetailModel.Price &&
                o.ProductAmount == orderDetailModel.ProductAmount)), Times.Once);
        }
    }
}
