using Moq;
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
    /// Unit tests for the <see cref="UserRoleService"/> class.
    /// </summary>
    public class UserRoleServiceTests
    {
        private readonly Mock<IUserRoleRepository> mockRepository;
        private readonly UserRoleService userRoleService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRoleServiceTests"/> class.
        /// Sets up the mock repository and the UserRoleService instance.
        /// </summary>
        public UserRoleServiceTests()
        {
            mockRepository = new Mock<IUserRoleRepository>();
            userRoleService = new UserRoleService(mockRepository.Object);
        }

        /// <summary>
        /// Tests the Add method to ensure a user role is added correctly.
        /// </summary>
        [Fact]
        public void Add_ShouldAddUserRole()
        {
            var userRoleModel = new UserRoleModel(0, "Administrator");

            userRoleService.Add(userRoleModel);

            mockRepository.Verify(r => r.Add(It.IsAny<UserRole>()), Times.Once);
        }

        /// <summary>
        /// Tests the Delete method to ensure a user role is deleted by ID correctly.
        /// </summary>
        [Fact]
        public void Delete_ShouldDeleteUserRole()
        {
            var userRoleId = 1;

            userRoleService.Delete(userRoleId);

            mockRepository.Verify(r => r.DeleteById(1), Times.Once);
        }

        /// <summary>
        /// Tests the GetAll method to ensure all user roles are retrieved correctly.
        /// </summary>
        [Fact]
        public void GetAll_ShouldReturnAllUserRoles()
        {
            var userRoles = new List<UserRole>
            {
                new UserRole(1, "Administrator"),
                new UserRole(2, "User")
            };
            mockRepository.Setup(r => r.GetAll()).Returns(userRoles);

            var result = userRoleService.GetAll();

            Assert.Equal(2, result.Count());
            Assert.Contains(result, ur => ((UserRoleModel)ur).RoleName == "Administrator");
            Assert.Contains(result, ur => ((UserRoleModel)ur).RoleName == "User");
        }

        /// <summary>
        /// Tests the GetById method to ensure a user role is retrieved by ID correctly.
        /// </summary>
        [Fact]
        public void GetById_ShouldReturnUserRole()
        {
            var userRole = new UserRole(1, "Administrator");
            mockRepository.Setup(r => r.GetById(1)).Returns(userRole);

            var result = (UserRoleModel)userRoleService.GetById(1);

            Assert.NotNull(result);
            Assert.Equal("Administrator", result.RoleName);
        }

        /// <summary>
        /// Tests the Update method to ensure a user role is updated correctly.
        /// </summary>
        [Fact]
        public void Update_ShouldUpdateUserRole()
        {
            var userRoleModel = new UserRoleModel(1, "User");
            var userRole = new UserRole(1, "Administrator");
            mockRepository.Setup(r => r.GetById(1)).Returns(userRole);

            userRoleService.Update(userRoleModel);

            mockRepository.Verify(r => r.Update(It.IsAny<UserRole>()), Times.Once);
            Assert.Equal("User", userRole.RoleName);
        }
    }
}
