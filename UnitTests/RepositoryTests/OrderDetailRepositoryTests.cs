using Microsoft.EntityFrameworkCore;
using StoreDAL.Data;
using StoreDAL.Data.InitDataFactory;
using StoreDAL.Entities;
using StoreDAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace UnitTests.RepositoryTests
{
    /// <summary>
    /// Unit tests for the <see cref="OrderDetailRepository"/> class.
    /// </summary>
    public class OrderDetailRepositoryTests
    {
        private readonly DbContextOptions<StoreDbContext> _dbContextOptions;
        private readonly AbstractDataFactory _testDataFactory;
        private readonly StoreDbContext _context;
        private readonly OrderDetailRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderDetailRepositoryTests"/> class.
        /// </summary>
        public OrderDetailRepositoryTests()
        {
            _dbContextOptions = new DbContextOptionsBuilder<StoreDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            _testDataFactory = new TestDataFactory();
            _context = new StoreDbContext(_dbContextOptions, _testDataFactory);
            _repository = new OrderDetailRepository(_context);
        }

        /// <summary>
        /// Tests the Add method of <see cref="OrderDetailRepository"/>.
        /// </summary>
        [Fact]
        public void Add_ShouldAddOrderDetail()
        {
            var orderDetail = new OrderDetail { OrderId = 1, ProductId = 1, Price = 10.99m, ProductAmount = 2 };

            _repository.Add(orderDetail);
            var result = _context.OrderDetails.FirstOrDefault(od => od.Price == 10.99m);

            Assert.NotNull(result);
            Assert.Equal(10.99m, result.Price);
        }

        /// <summary>
        /// Tests the Delete method of <see cref="OrderDetailRepository"/>.
        /// </summary>
        [Fact]
        public void Delete_ShouldDeleteOrderDetail()
        {
            var orderDetail = new OrderDetail { OrderId = 1, ProductId = 1, Price = 10.99m, ProductAmount = 2 };
            _context.OrderDetails.Add(orderDetail);
            _context.SaveChanges();

            _repository.Delete(orderDetail);
            var result = _context.OrderDetails.FirstOrDefault(od => od.Price == 10.99m);

            Assert.Null(result);
        }

        /// <summary>
        /// Tests the DeleteById method of <see cref="OrderDetailRepository"/>.
        /// </summary>
        [Fact]
        public void DeleteById_ShouldDeleteOrderDetailById()
        {
            var orderDetail = new OrderDetail { OrderId = 1, ProductId = 1, Price = 10.99m, ProductAmount = 2 };
            _context.OrderDetails.Add(orderDetail);
            _context.SaveChanges();

            _repository.DeleteById(orderDetail.Id);
            var result = _context.OrderDetails.FirstOrDefault(od => od.Id == orderDetail.Id);

            Assert.Null(result);
        }

        /// <summary>
        /// Tests the GetAll method of <see cref="OrderDetailRepository"/>.
        /// </summary>
        [Fact]
        public void GetAll_ShouldReturnAllOrderDetails()
        {
            _context.OrderDetails.AddRange(new List<OrderDetail>
            {
                new OrderDetail { OrderId = 1, ProductId = 1, Price = 10.99m, ProductAmount = 2 },
                new OrderDetail { OrderId = 2, ProductId = 2, Price = 5.99m, ProductAmount = 1 }
            });
            _context.SaveChanges();

            var result = _repository.GetAll();

            Assert.Equal(2, result.Count());
            Assert.Contains(result, od => od.Price == 10.99m);
            Assert.Contains(result, od => od.Price == 5.99m);
        }

        /// <summary>
        /// Tests the GetAll method with pagination of <see cref="OrderDetailRepository"/>.
        /// </summary>
        [Fact]
        public void GetAll_WithPagination_ShouldReturnPaginatedOrderDetails()
        {
            _context.OrderDetails.AddRange(new List<OrderDetail>
            {
                new OrderDetail { OrderId = 1, ProductId = 1, Price = 10.99m, ProductAmount = 2 },
                new OrderDetail { OrderId = 2, ProductId = 2, Price = 5.99m, ProductAmount = 1 },
                new OrderDetail { OrderId = 3, ProductId = 3, Price = 15.99m, ProductAmount = 3 },
                new OrderDetail { OrderId = 4, ProductId = 4, Price = 20.99m, ProductAmount = 4 }
            });
            _context.SaveChanges();

            var result = _repository.GetAll(1, 2);

            Assert.Equal(2, result.Count());
            Assert.Contains(result, od => od.Price == 10.99m);
            Assert.Contains(result, od => od.Price == 5.99m);

            var result2 = _repository.GetAll(2, 2);

            Assert.Equal(2, result2.Count());
            Assert.Contains(result2, od => od.Price == 15.99m);
            Assert.Contains(result2, od => od.Price == 20.99m);
        }

        /// <summary>
        /// Tests the GetById method of <see cref="OrderDetailRepository"/>.
        /// </summary>
        [Fact]
        public void GetById_ShouldReturnOrderDetail()
        {
            var orderDetail = new OrderDetail { OrderId = 1, ProductId = 1, Price = 10.99m, ProductAmount = 2 };
            _context.OrderDetails.Add(orderDetail);
            _context.SaveChanges();

            var result = _repository.GetById(orderDetail.Id);

            Assert.NotNull(result);
            Assert.Equal(10.99m, result.Price);
        }

        /// <summary>
        /// Tests the Update method of <see cref="OrderDetailRepository"/>.
        /// </summary>
        [Fact]
        public void Update_ShouldUpdateOrderDetail()
        {
            var orderDetail = new OrderDetail { OrderId = 1, ProductId = 1, Price = 10.99m, ProductAmount = 2 };
            _context.OrderDetails.Add(orderDetail);
            _context.SaveChanges();

            orderDetail.Price = 12.99m;
            _repository.Update(orderDetail);
            var result = _context.OrderDetails.FirstOrDefault(od => od.Id == orderDetail.Id);

            Assert.NotNull(result);
            Assert.Equal(12.99m, result.Price);
        }
    }
}
