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
    /// Unit tests for the UserService class.
    /// </summary>
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> mockRepository;
        private readonly UserService userService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserServiceTests"/> class.
        /// Sets up the mock repository and the UserService instance.
        /// </summary>
        public UserServiceTests()
        {
            mockRepository = new Mock<IUserRepository>();
            userService = new UserService(mockRepository.Object);
        }

        /// <summary>
        /// Tests the Add method to ensure a user is added correctly.
        /// </summary>
        [Fact]
        public void Add_ShouldAddUser()
        {
            var userModel = new UserModel(0, "John", "Doe", "john.doe", "password", 1);

            userService.Add(userModel);

            mockRepository.Verify(r => r.Add(It.IsAny<User>()), Times.Once);
        }

        /// <summary>
        /// Tests the Delete method to ensure a user is deleted by ID correctly.
        /// </summary>
        [Fact]
        public void Delete_ShouldDeleteUser()
        {
            var userId = 1;

            userService.Delete(userId);

            mockRepository.Verify(r => r.DeleteById(1), Times.Once);
        }

        /// <summary>
        /// Tests the GetAll method to ensure all users are retrieved correctly.
        /// </summary>
        [Fact]
        public void GetAll_ShouldReturnAllUsers()
        {
            var users = new List<User>
            {
                new User(1, "John", "Doe", "john.doe", "password", 1),
                new User(2, "Jane", "Smith", "jane.smith", "password", 2)
            };
            mockRepository.Setup(r => r.GetAll()).Returns(users);

            var result = userService.GetAll();

            Assert.Equal(2, result.Count());
            Assert.Contains(result, u => ((UserModel)u).Login == "john.doe");
            Assert.Contains(result, u => ((UserModel)u).Login == "jane.smith");
        }

        /// <summary>
        /// Tests the GetById method to ensure a user is retrieved by ID correctly.
        /// </summary>
        [Fact]
        public void GetById_ShouldReturnUser()
        {
            var user = new User(1, "John", "Doe", "john.doe", "password", 1);
            mockRepository.Setup(r => r.GetById(1)).Returns(user);

            var result = (UserModel)userService.GetById(1);

            Assert.NotNull(result);
            Assert.Equal("john.doe", result.Login);
        }

        /// <summary>
        /// Tests the Authenticate method to ensure a user is authenticated correctly.
        /// </summary>
        [Fact]
        public void Authenticate_ShouldReturnAuthenticatedUser()
        {
            var user = new User(1, "John", "Doe", "john.doe", BCrypt.Net.BCrypt.HashPassword("password"), 1);
            mockRepository.Setup(r => r.GetByLogin("john.doe")).Returns(user);

            var result = userService.Authenticate("john.doe", "password");

            Assert.NotNull(result);
            Assert.Equal("john.doe", result.Login);
        }

        /// <summary>
        /// Tests the Authenticate method to ensure an exception is thrown for invalid login or password.
        /// </summary>
        [Fact]
        public void Authenticate_ShouldThrowInvalidOperationExceptionForInvalidLoginOrPassword()
        {
            var user = new User(1, "John", "Doe", "john.doe", BCrypt.Net.BCrypt.HashPassword("password"), 1);
            mockRepository.Setup(r => r.GetByLogin("john.doe")).Returns(user);

            Assert.Throws<InvalidOperationException>(() => userService.Authenticate("john.doe", "wrongpassword"));
        }

        /// <summary>
        /// Tests the Update method to ensure a user is updated correctly.
        /// </summary>
        [Fact]
        public void Update_ShouldUpdateUser()
        {
            var userModel = new UserModel(1, "John", "Doe", "john.doe", "newpassword", 1);
            var user = new User(1, "John", "Doe", "john.doe", BCrypt.Net.BCrypt.HashPassword("oldpassword"), 1);
            mockRepository.Setup(r => r.GetById(1)).Returns(user);

            userService.Update(userModel);

            mockRepository.Verify(r => r.Update(It.IsAny<User>()), Times.Once);
            Assert.Equal("John", user.Name);
            Assert.Equal("newpassword", userModel.Password); 
        }
    }
}
