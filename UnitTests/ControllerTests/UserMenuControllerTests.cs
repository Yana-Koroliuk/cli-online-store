using ConsoleApp1;
using ConsoleApp.Helpers;
using Moq;
using StoreBLL.Models;
using StoreBLL.Services;
using System;
using Xunit;

namespace UnitTests.ControllerTests
{
    /// <summary>
    /// Unit tests for the <see cref="UserMenuController"/> class.
    /// </summary>
    public class UserMenuControllerTests
    {
        private readonly Mock<UserService> mockUserService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserMenuControllerTests"/> class.
        /// Sets up the mock services.
        /// </summary>
        public UserMenuControllerTests()
        {
            mockUserService = new Mock<UserService>();
        }

        /// <summary>
        /// Tests the GetService method to ensure the correct service is retrieved.
        /// </summary>
        [Fact]
        public void GetService_ShouldReturnCorrectService()
        {
            var userService = UserMenuController.GetService<UserService>();

            Assert.NotNull(userService);
        }

        /// <summary>
        /// Tests the Login method to ensure successful login.
        /// </summary>
        [Fact]
        public void Login_ShouldLoginSuccessfully()
        {
            var userModel = new UserModel(1, "John", "Doe", "john.doe", "$2a$11$hkggypO4qZEopgDYwWIAo.POIb9E80igMtGIT6LQSotOMaZn1PtHe", (int)UserRoles.RegistredCustomer);
            mockUserService.Setup(us => us.Authenticate(It.IsAny<string>(), It.IsAny<string>())).Returns(userModel);

            Console.SetIn(new StringReader("john.doe\npassword123\n"));

            UserMenuController.Login();

            Assert.Equal(1, UserMenuController.UserId);
            Assert.Equal(UserRoles.RegistredCustomer, UserMenuController.UserRole);
        }

        /// <summary>
        /// Tests the Login method to ensure invalid login handling.
        /// </summary>
        [Fact]
        public void Login_ShouldHandleInvalidLogin()
        {
            mockUserService.Setup(us => us.Authenticate(It.IsAny<string>(), It.IsAny<string>())).Throws(new InvalidOperationException("Invalid login or password."));

            Console.SetIn(new StringReader("john.doe\nwrongpassword\n"));

            UserMenuController.Login();

            Assert.Equal(0, UserMenuController.UserId);
            Assert.Equal(UserRoles.Guest, UserMenuController.UserRole);
        }

        /// <summary>
        /// Tests the Logout method to ensure successful logout.
        /// </summary>
        [Fact]
        public void Logout_ShouldLogoutSuccessfully()
        {
            var userModel = new UserModel(1, "John", "Doe", "john.doe", "$2a$11$hkggypO4qZEopgDYwWIAo.POIb9E80igMtGIT6LQSotOMaZn1PtHe", (int)UserRoles.RegistredCustomer);
            mockUserService.Setup(us => us.Authenticate(It.IsAny<string>(), It.IsAny<string>())).Returns(userModel);
            Console.SetIn(new StringReader("john.doe\npassword123\n"));
            UserMenuController.Login();

            UserMenuController.Logout();

            Assert.Equal(0, UserMenuController.UserId);
            Assert.Equal(UserRoles.Guest, UserMenuController.UserRole);
        }
    }
}
