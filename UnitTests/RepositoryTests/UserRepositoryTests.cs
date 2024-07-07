using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using StoreDAL.Data;
using StoreDAL.Data.InitDataFactory;
using StoreDAL.Entities;
using StoreDAL.Repository;
using Xunit;

namespace UnitTests.RepositoryTests
{
    /// <summary>
    /// Unit tests for the <see cref="UserRepository"/> class.
    /// </summary>
    public class UserRepositoryTests
    {
        private readonly DbContextOptions<StoreDbContext> _dbContextOptions;
        private readonly AbstractDataFactory _testDataFactory;
        private readonly StoreDbContext _context;
        private readonly UserRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepositoryTests"/> class.
        /// </summary>
        public UserRepositoryTests()
        {
            _dbContextOptions = new DbContextOptionsBuilder<StoreDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            _testDataFactory = new TestDataFactory();
            _context = new StoreDbContext(_dbContextOptions, _testDataFactory);
            _repository = new UserRepository(_context);
        }

        /// <summary>
        /// Tests the Add method of <see cref="UserRepository"/>.
        /// </summary>
        [Fact]
        public void Add_ShouldAddUser()
        {
            var user = new User { Name = "John", LastName = "Doe", Login = "john.doe", Password = "password", RoleId = 2 };

            _repository.Add(user);
            var result = _context.Users.FirstOrDefault(u => u.Login == "john.doe");

            Assert.NotNull(result);
            Assert.Equal("John", result.Name);
        }

        /// <summary>
        /// Tests the Delete method of <see cref="UserRepository"/>.
        /// </summary>
        [Fact]
        public void Delete_ShouldDeleteUser()
        {
            var user = new User { Name = "John", LastName = "Doe", Login = "john.doe", Password = "password", RoleId = 2 };
            _context.Users.Add(user);
            _context.SaveChanges();

            _repository.Delete(user);
            var result = _context.Users.FirstOrDefault(u => u.Login == "john.doe");

            Assert.Null(result);
        }

        /// <summary>
        /// Tests the DeleteById method of <see cref="UserRepository"/>.
        /// </summary>
        [Fact]
        public void DeleteById_ShouldDeleteUserById()
        {
            var user = new User { Name = "John", LastName = "Doe", Login = "john.doe", Password = "password", RoleId = 2 };
            _context.Users.Add(user);
            _context.SaveChanges();

            _repository.DeleteById(user.Id);
            var result = _context.Users.FirstOrDefault(u => u.Id == user.Id);

            Assert.Null(result);
        }

        /// <summary>
        /// Tests the GetAll method of <see cref="UserRepository"/>.
        /// </summary>
        [Fact]
        public void GetAll_ShouldReturnAllUsers()
        {
            _context.Users.AddRange(new List<User>
            {
                new User { Name = "John", LastName = "Doe", Login = "john.doe", Password = "password", RoleId = 2 },
                new User { Name = "Jane", LastName = "Doe", Login = "jane.doe", Password = "password", RoleId = 2 }
            });
            _context.SaveChanges();

            var result = _repository.GetAll();

            Assert.Equal(2, result.Count());
            Assert.Contains(result, u => u.Login == "john.doe");
            Assert.Contains(result, u => u.Login == "jane.doe");
        }

        /// <summary>
        /// Tests the GetAll method with pagination of <see cref="UserRepository"/>.
        /// </summary>
        [Fact]
        public void GetAll_WithPagination_ShouldReturnPaginatedUsers()
        {
            _context.Users.AddRange(new List<User>
            {
                new User { Name = "John", LastName = "Doe", Login = "john.doe", Password = "password", RoleId = 2 },
                new User { Name = "Jane", LastName = "Doe", Login = "jane.doe", Password = "password", RoleId = 2 },
                new User { Name = "Alice", LastName = "Smith", Login = "alice.smith", Password = "password", RoleId = 2 },
                new User { Name = "Bob", LastName = "Johnson", Login = "bob.johnson", Password = "password", RoleId = 2 }
            });
            _context.SaveChanges();

            var result = _repository.GetAll(1, 2); 

            Assert.Equal(2, result.Count());
            Assert.Contains(result, u => u.Login == "john.doe");
            Assert.Contains(result, u => u.Login == "jane.doe");

            var result2 = _repository.GetAll(2, 2); 

            Assert.Equal(2, result2.Count());
            Assert.Contains(result2, u => u.Login == "alice.smith");
            Assert.Contains(result2, u => u.Login == "bob.johnson");
        }

        /// <summary>
        /// Tests the GetById method of <see cref="UserRepository"/>.
        /// </summary>
        [Fact]
        public void GetById_ShouldReturnUser()
        {
            var user = new User { Name = "John", LastName = "Doe", Login = "john.doe", Password = "password", RoleId = 2 };
            _context.Users.Add(user);
            _context.SaveChanges();

            var result = _repository.GetById(user.Id);

            Assert.NotNull(result);
            Assert.Equal("John", result.Name);
        }

        /// <summary>
        /// Tests the GetByLogin method of <see cref="UserRepository"/>.
        /// </summary>
        [Fact]
        public void GetByLogin_ShouldReturnUser()
        {
            var user = new User { Name = "John", LastName = "Doe", Login = "john.doe", Password = "password", RoleId = 2 };
            _context.Users.Add(user);
            _context.SaveChanges();

            var result = _repository.GetByLogin("john.doe");

            Assert.NotNull(result);
            Assert.Equal("John", result.Name);
        }

        /// <summary>
        /// Tests the Update method of <see cref="UserRepository"/>.
        /// </summary>
        [Fact]
        public void Update_ShouldUpdateUser()
        {
            var user = new User { Name = "John", LastName = "Doe", Login = "john.doe", Password = "password", RoleId = 2 };
            _context.Users.Add(user);
            _context.SaveChanges();

            user.Name = "Updated John";
            _repository.Update(user);
            var result = _context.Users.FirstOrDefault(u => u.Id == user.Id);

            Assert.NotNull(result);
            Assert.Equal("Updated John", result.Name);
        }
    }
}
