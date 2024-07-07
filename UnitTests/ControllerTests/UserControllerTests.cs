using ConsoleApp.Services;
using Moq;
using StoreBLL.Models;
using StoreBLL.Interfaces;
using System;
using Xunit;
using ConsoleApp1;

namespace UnitTests.ControllerTests
{
    /// <summary>
    /// Unit tests for the <see cref="UserController"/> class.
    /// </summary>
    public class UserControllerTests
    {
        private readonly Mock<ICrud> _mockUserService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserControllerTests"/> class.
        /// </summary>
        public UserControllerTests()
        {
            _mockUserService = new Mock<ICrud>();
        }

        /// <summary>
        /// Tests the <see cref="UserController.AddUser(ICrud)"/> method to ensure it registers a new user successfully.
        /// </summary>
        [Fact]
        public void AddUser_ShouldRegisterNewUser()
        {
            _mockUserService.Setup(s => s.Add(It.IsAny<AbstractModel>())).Verifiable();
            Console.SetIn(new StringReader("John\nDoe\njohn.doe\npassword\n"));

            UserController.AddUser(_mockUserService.Object);

            _mockUserService.Verify(s => s.Add(It.IsAny<AbstractModel>()), Times.Once);
        }

        /// <summary>
        /// Tests the <see cref="UserController.UpdateUser(ICrud)"/> method to ensure it updates user information successfully.
        /// </summary>
        [Fact]
        public void UpdateUser_ShouldUpdateUserInformation()
        {
            var user = new UserModel(1, "John", "Doe", "john.doe", "password", (int)UserRoles.RegistredCustomer);
            _mockUserService.Setup(s => s.GetById(It.IsAny<int>())).Returns(user);
            _mockUserService.Setup(s => s.Update(It.IsAny<AbstractModel>())).Verifiable();
            Console.SetIn(new StringReader("John\nDoe\njohn.doe\nnewpassword\n"));

            UserController.UpdateUser(_mockUserService.Object);

            _mockUserService.Verify(s => s.Update(It.IsAny<AbstractModel>()), Times.Once);
        }
    }
}
