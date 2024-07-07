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
    /// Unit tests for the <see cref="UserRoleRepository"/> class.
    /// </summary>
    public class UserRoleRepositoryTests
    {
        private readonly DbContextOptions<StoreDbContext> _dbContextOptions;
        private readonly AbstractDataFactory _testDataFactory;
        private readonly StoreDbContext _context;
        private readonly UserRoleRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRoleRepositoryTests"/> class.
        /// </summary>
        public UserRoleRepositoryTests()
        {
            _dbContextOptions = new DbContextOptionsBuilder<StoreDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            _testDataFactory = new TestDataFactory();
            _context = new StoreDbContext(_dbContextOptions, _testDataFactory);
            _repository = new UserRoleRepository(_context);
        }

        /// <summary>
        /// Tests the Add method of <see cref="UserRoleRepository"/>.
        /// </summary>
        [Fact]
        public void Add_ShouldAddUserRole()
        {
            var userRole = new UserRole { RoleName = "Admin" };

            _repository.Add(userRole);
            var result = _context.UserRoles.FirstOrDefault(ur => ur.RoleName == "Admin");

            Assert.NotNull(result);
            Assert.Equal("Admin", result.RoleName);
        }

        /// <summary>
        /// Tests the Delete method of <see cref="UserRoleRepository"/>.
        /// </summary>
        [Fact]
        public void Delete_ShouldDeleteUserRole()
        {
            var userRole = new UserRole { RoleName = "Admin" };
            _context.UserRoles.Add(userRole);
            _context.SaveChanges();

            _repository.Delete(userRole);
            var result = _context.UserRoles.FirstOrDefault(ur => ur.RoleName == "Admin");

            Assert.Null(result);
        }

        /// <summary>
        /// Tests the DeleteById method of <see cref="UserRoleRepository"/>.
        /// </summary>
        [Fact]
        public void DeleteById_ShouldDeleteUserRoleById()
        {
            var userRole = new UserRole { RoleName = "Admin" };
            _context.UserRoles.Add(userRole);
            _context.SaveChanges();

            _repository.DeleteById(userRole.Id);
            var result = _context.UserRoles.FirstOrDefault(ur => ur.Id == userRole.Id);

            Assert.Null(result);
        }

        /// <summary>
        /// Tests the GetAll method of <see cref="UserRoleRepository"/>.
        /// </summary>
        [Fact]
        public void GetAll_ShouldReturnAllUserRoles()
        {
            _context.UserRoles.AddRange(new List<UserRole>
            {
                new UserRole { RoleName = "Admin" },
                new UserRole { RoleName = "User" }
            });
            _context.SaveChanges();

            var result = _repository.GetAll();

            Assert.Equal(2, result.Count());
            Assert.Contains(result, ur => ur.RoleName == "Admin");
            Assert.Contains(result, ur => ur.RoleName == "User");
        }

        /// <summary>
        /// Tests the GetAll method with pagination of <see cref="UserRoleRepository"/>.
        /// </summary>
        [Fact]
        public void GetAll_WithPagination_ShouldReturnPaginatedUserRoles()
        {
            _context.UserRoles.AddRange(new List<UserRole>
            {
                new UserRole { RoleName = "Admin" },
                new UserRole { RoleName = "User" },
                new UserRole { RoleName = "Manager" },
                new UserRole { RoleName = "Guest" }
            });
            _context.SaveChanges();

            var result = _repository.GetAll(1, 2);

            Assert.Equal(2, result.Count());
            Assert.Contains(result, ur => ur.RoleName == "Admin");
            Assert.Contains(result, ur => ur.RoleName == "User");

            var result2 = _repository.GetAll(2, 2);

            Assert.Equal(2, result2.Count());
            Assert.Contains(result2, ur => ur.RoleName == "Manager");
            Assert.Contains(result2, ur => ur.RoleName == "Guest");
        }

        /// <summary>
        /// Tests the GetById method of <see cref="UserRoleRepository"/>.
        /// </summary>
        [Fact]
        public void GetById_ShouldReturnUserRole()
        {
            var userRole = new UserRole { RoleName = "Admin" };
            _context.UserRoles.Add(userRole);
            _context.SaveChanges();

            var result = _repository.GetById(userRole.Id);

            Assert.NotNull(result);
            Assert.Equal("Admin", result.RoleName);
        }

        /// <summary>
        /// Tests the Update method of <see cref="UserRoleRepository"/>.
        /// </summary>
        [Fact]
        public void Update_ShouldUpdateUserRole()
        {
            var userRole = new UserRole { RoleName = "Admin" };
            _context.UserRoles.Add(userRole);
            _context.SaveChanges();

            userRole.RoleName = "Super Admin";
            _repository.Update(userRole);
            var result = _context.UserRoles.FirstOrDefault(ur => ur.Id == userRole.Id);

            Assert.NotNull(result);
            Assert.Equal("Super Admin", result.RoleName);
        }
    }
}
