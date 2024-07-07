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
    /// Unit tests for the <see cref="CustomerOrderRepository"/>.
    /// </summary>
    public class CustomerOrderRepositoryTests
    {
        private readonly DbContextOptions<StoreDbContext> _dbContextOptions;
        private readonly AbstractDataFactory _testDataFactory;
        private readonly StoreDbContext _context;
        private readonly CustomerOrderRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerOrderRepositoryTests"/> class.
        /// </summary>
        public CustomerOrderRepositoryTests()
        {
            _dbContextOptions = new DbContextOptionsBuilder<StoreDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            _testDataFactory = new TestDataFactory();
            _context = new StoreDbContext(_dbContextOptions, _testDataFactory);
            _repository = new CustomerOrderRepository(_context);
        }

        /// <summary>
        /// Tests the Add method of the <see cref="CustomerOrderRepository"/> class.
        /// </summary>
        [Fact]
        public void Add_ShouldAddCustomerOrder()
        {
            var customerOrder = new CustomerOrder { OperationTime = "2023-01-01T12:00:00", UserId = 1, OrderStateId = 1 };

            _repository.Add(customerOrder);
            var result = _context.CustomerOrders.FirstOrDefault(co => co.OperationTime == "2023-01-01T12:00:00");

            Assert.NotNull(result);
            Assert.Equal("2023-01-01T12:00:00", result.OperationTime);
        }

        /// <summary>
        /// Tests the Delete method of the <see cref="CustomerOrderRepository"/> class.
        /// </summary>
        [Fact]
        public void Delete_ShouldDeleteCustomerOrder()
        {
            var customerOrder = new CustomerOrder { OperationTime = "2023-01-01T12:00:00", UserId = 1, OrderStateId = 1 };
            _context.CustomerOrders.Add(customerOrder);
            _context.SaveChanges();

            _repository.Delete(customerOrder);
            var result = _context.CustomerOrders.FirstOrDefault(co => co.OperationTime == "2023-01-01T12:00:00");

            Assert.Null(result);
        }

        /// <summary>
        /// Tests the DeleteById method of the <see cref="CustomerOrderRepository"/> class.
        /// </summary>
        [Fact]
        public void DeleteById_ShouldDeleteCustomerOrderById()
        {
            var customerOrder = new CustomerOrder { OperationTime = "2023-01-01T12:00:00", UserId = 1, OrderStateId = 1 };
            _context.CustomerOrders.Add(customerOrder);
            _context.SaveChanges();

            _repository.DeleteById(customerOrder.Id);
            var result = _context.CustomerOrders.FirstOrDefault(co => co.Id == customerOrder.Id);

            Assert.Null(result);
        }

        /// <summary>
        /// Tests the GetAll method of the <see cref="CustomerOrderRepository"/> class.
        /// </summary>
        [Fact]
        public void GetAll_ShouldReturnAllCustomerOrders()
        {
            _context.CustomerOrders.AddRange(new List<CustomerOrder>
            {
                new CustomerOrder { OperationTime = "2023-01-01T12:00:00", UserId = 1, OrderStateId = 1 },
                new CustomerOrder { OperationTime = "2023-01-02T13:00:00", UserId = 2, OrderStateId = 2 }
            });
            _context.SaveChanges();

            var result = _repository.GetAll();

            Assert.Equal(2, result.Count());
            Assert.Contains(result, co => co.OperationTime == "2023-01-01T12:00:00");
            Assert.Contains(result, co => co.OperationTime == "2023-01-02T13:00:00");
        }

        /// <summary>
        /// Tests the GetAll method with pagination of the <see cref="CustomerOrderRepository"/> class.
        /// </summary>
        [Fact]
        public void GetAll_WithPagination_ShouldReturnPaginatedCustomerOrders()
        {
            _context.CustomerOrders.AddRange(new List<CustomerOrder>
            {
                new CustomerOrder { OperationTime = "2023-01-01T12:00:00", UserId = 1, OrderStateId = 1 },
                new CustomerOrder { OperationTime = "2023-01-02T13:00:00", UserId = 2, OrderStateId = 2 },
                new CustomerOrder { OperationTime = "2023-01-03T14:00:00", UserId = 3, OrderStateId = 3 },
                new CustomerOrder { OperationTime = "2023-01-04T15:00:00", UserId = 4, OrderStateId = 4 }
            });
            _context.SaveChanges();

            var result = _repository.GetAll(1, 2);

            Assert.Equal(2, result.Count());
            Assert.Contains(result, co => co.OperationTime == "2023-01-01T12:00:00");
            Assert.Contains(result, co => co.OperationTime == "2023-01-02T13:00:00");

            var result2 = _repository.GetAll(2, 2);

            Assert.Equal(2, result2.Count());
            Assert.Contains(result2, co => co.OperationTime == "2023-01-03T14:00:00");
            Assert.Contains(result2, co => co.OperationTime == "2023-01-04T15:00:00");
        }

        /// <summary>
        /// Tests the GetById method of the <see cref="CustomerOrderRepository"/> class.
        /// </summary>
        [Fact]
        public void GetById_ShouldReturnCustomerOrder()
        {
            var customerOrder = new CustomerOrder { OperationTime = "2023-01-01T12:00:00", UserId = 1, OrderStateId = 1 };
            _context.CustomerOrders.Add(customerOrder);
            _context.SaveChanges();

            var result = _repository.GetById(customerOrder.Id);

            Assert.NotNull(result);
            Assert.Equal("2023-01-01T12:00:00", result.OperationTime);
        }

        /// <summary>
        /// Tests the Update method of the <see cref="CustomerOrderRepository"/> class.
        /// </summary>
        [Fact]
        public void Update_ShouldUpdateCustomerOrder()
        {
            var customerOrder = new CustomerOrder { OperationTime = "2023-01-01T12:00:00", UserId = 1, OrderStateId = 1 };
            _context.CustomerOrders.Add(customerOrder);
            _context.SaveChanges();

            customerOrder.OperationTime = "2023-01-02T13:00:00";
            _repository.Update(customerOrder);
            var result = _context.CustomerOrders.FirstOrDefault(co => co.Id == customerOrder.Id);

            Assert.NotNull(result);
            Assert.Equal("2023-01-02T13:00:00", result.OperationTime);
        }
    }
}
