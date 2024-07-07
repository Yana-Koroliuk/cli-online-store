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
    /// Unit tests for the <see cref="OrderStateRepository"/> class.
    /// </summary>
    public class OrderStateRepositoryTests
    {
        private readonly DbContextOptions<StoreDbContext> _dbContextOptions;
        private readonly AbstractDataFactory _testDataFactory;
        private readonly StoreDbContext _context;
        private readonly OrderStateRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderStateRepositoryTests"/> class.
        /// </summary>
        public OrderStateRepositoryTests()
        {
            _dbContextOptions = new DbContextOptionsBuilder<StoreDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            _testDataFactory = new TestDataFactory();
            _context = new StoreDbContext(_dbContextOptions, _testDataFactory);
            _repository = new OrderStateRepository(_context);
        }

        /// <summary>
        /// Tests the Add method of <see cref="OrderStateRepository"/>.
        /// </summary>
        [Fact]
        public void Add_ShouldAddOrderState()
        {
            var orderState = new OrderState { StateName = "New State" };

            _repository.Add(orderState);
            var result = _context.OrderStates.FirstOrDefault(os => os.StateName == "New State");

            Assert.NotNull(result);
            Assert.Equal("New State", result.StateName);
        }

        /// <summary>
        /// Tests the Delete method of <see cref="OrderStateRepository"/>.
        /// </summary>
        [Fact]
        public void Delete_ShouldDeleteOrderState()
        {
            var orderState = new OrderState { StateName = "New State" };
            _context.OrderStates.Add(orderState);
            _context.SaveChanges();

            _repository.Delete(orderState);
            var result = _context.OrderStates.FirstOrDefault(os => os.StateName == "New State");

            Assert.Null(result);
        }

        /// <summary>
        /// Tests the DeleteById method of <see cref="OrderStateRepository"/>.
        /// </summary>
        [Fact]
        public void DeleteById_ShouldDeleteOrderStateById()
        {
            var orderState = new OrderState { StateName = "New State" };
            _context.OrderStates.Add(orderState);
            _context.SaveChanges();

            _repository.DeleteById(orderState.Id);
            var result = _context.OrderStates.FirstOrDefault(os => os.Id == orderState.Id);

            Assert.Null(result);
        }

        /// <summary>
        /// Tests the GetAll method of <see cref="OrderStateRepository"/>.
        /// </summary>
        [Fact]
        public void GetAll_ShouldReturnAllOrderStates()
        {
            _context.OrderStates.AddRange(new List<OrderState>
            {
                new OrderState { StateName = "State 1" },
                new OrderState { StateName = "State 2" }
            });
            _context.SaveChanges();

            var result = _repository.GetAll();

            Assert.Equal(2, result.Count());
            Assert.Contains(result, os => os.StateName == "State 1");
            Assert.Contains(result, os => os.StateName == "State 2");
        }

        /// <summary>
        /// Tests the GetAll method with pagination of <see cref="OrderStateRepository"/>.
        /// </summary>
        [Fact]
        public void GetAll_WithPagination_ShouldReturnPaginatedOrderStates()
        {
            _context.OrderStates.AddRange(new List<OrderState>
            {
                new OrderState { StateName = "State 1" },
                new OrderState { StateName = "State 2" },
                new OrderState { StateName = "State 3" },
                new OrderState { StateName = "State 4" }
            });
            _context.SaveChanges();

            var result = _repository.GetAll(1, 2);

            Assert.Equal(2, result.Count());
            Assert.Contains(result, os => os.StateName == "State 1");
            Assert.Contains(result, os => os.StateName == "State 2");

            var result2 = _repository.GetAll(2, 2);

            Assert.Equal(2, result2.Count());
            Assert.Contains(result2, os => os.StateName == "State 3");
            Assert.Contains(result2, os => os.StateName == "State 4");
        }

        /// <summary>
        /// Tests the GetById method of <see cref="OrderStateRepository"/>.
        /// </summary>
        [Fact]
        public void GetById_ShouldReturnOrderState()
        {
            var orderState = new OrderState { StateName = "State 1" };
            _context.OrderStates.Add(orderState);
            _context.SaveChanges();

            var result = _repository.GetById(orderState.Id);

            Assert.NotNull(result);
            Assert.Equal("State 1", result.StateName);
        }

        /// <summary>
        /// Tests the Update method of <see cref="OrderStateRepository"/>.
        /// </summary>
        [Fact]
        public void Update_ShouldUpdateOrderState()
        {
            var orderState = new OrderState { StateName = "State 1" };
            _context.OrderStates.Add(orderState);
            _context.SaveChanges();

            orderState.StateName = "Updated State";
            _repository.Update(orderState);
            var result = _context.OrderStates.FirstOrDefault(os => os.Id == orderState.Id);

            Assert.NotNull(result);
            Assert.Equal("Updated State", result.StateName);
        }
    }
}
