using ConsoleMenu;
using ConsoleMenu.Builder;
using StoreBLL.Interfaces;
using StoreBLL.Services;
using StoreDAL.Data;
using StoreDAL.Data.InitDataFactory;
using StoreDAL.Interfaces;
using StoreDAL.Repository;

namespace ConsoleApp1
{
    /// <summary>
    /// Represents user roles in the system.
    /// </summary>
    public enum UserRoles
    {
        Guest,
        Administrator,
        RegistredCustomer,
    }

    /// <summary>
    /// Manages user menu navigation and actions.
    /// </summary>
    public static class UserMenuController
    {
        private static int userId;
        private static UserRoles userRole = UserRoles.Guest;
        private static StoreDbContext context = new StoreDbFactory(new TestDataFactory()).CreateContext();
        private static Dictionary<UserRoles, Menu> rolesToMenu = new Dictionary<UserRoles, Menu>
        {
            { UserRoles.Guest, new GuestMainMenu().Create(context) },
            { UserRoles.RegistredCustomer, new UserMainMenu().Create(context) },
            { UserRoles.Administrator, new AdminMainMenu().Create(context) },
        };

        private static ICategoryRepository categoryRepository = new CategoryRepository(context);
        private static ICustomerOrderRepository customerOrderRepository = new CustomerOrderRepository(context);
        private static IManufacturerRepository manufacturerRepository = new ManufacturerRepository(context);
        private static IOrderDetailRepository orderDetailRepository = new OrderDetailRepository(context);
        private static IOrderStateRepository orderStateRepository = new OrderStateRepository(context);
        private static IProductRepository productRepository = new ProductRepository(context);
        private static IProductTitleRepository productTitleRepository = new ProductTitleRepository(context);
        private static IUserRoleRepository userRoleRepository = new UserRoleRepository(context);
        private static IUserRepository userRepository = new UserRepository(context);

        private static Dictionary<Type, object> services = new Dictionary<Type, object>
        {
             { typeof(CategoryService), new CategoryService(categoryRepository) },
             { typeof(CustomerOrderService), new CustomerOrderService(customerOrderRepository) },
             { typeof(ManufacturerService), new ManufacturerService(manufacturerRepository) },
             { typeof(OrderDetailService), new OrderDetailService(orderDetailRepository) },
             { typeof(OrderStateService), new OrderStateService(orderStateRepository) },
             { typeof(ProductService), new ProductService(productRepository) },
             { typeof(ProductTitleService), new ProductTitleService(productTitleRepository) },
             { typeof(UserRoleService), new UserRoleService(userRoleRepository) },
             { typeof(UserService), new UserService(userRepository) },
        };

        /// <summary>
        /// Gets the user ID.
        /// </summary>
        public static int UserId
        {
            get => userId;
        }

        /// <summary>
        /// Gets the User Role.
        /// </summary>
        public static UserRoles UserRole
        {
            get => userRole;
        }

        /// <summary>
        /// Retrieves a service of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of the service.</typeparam>
        /// <returns>The service instance.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the service is not found.</exception>
        public static T GetService<T>()
            where T : class
        {
            return services[typeof(T)] as T ?? throw new InvalidOperationException("Service not found.");
        }

        /// <summary>
        /// Handles user login.
        /// </summary>
        public static void Login()
        {
            Console.WriteLine("Login: ");
            var login = Console.ReadLine();
            Console.WriteLine("Password: ");
            var password = Console.ReadLine();
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                Console.WriteLine("Login and password cannot be empty.");
                return;
            }

            try
            {
                var userService = GetService<UserService>();
                var user = userService.Authenticate(login, password);
                userId = user.Id;
                userRole = (UserRoles)user.RoleId;
                Console.WriteLine("Login successful.");
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid login or password.");
            }
        }

        /// <summary>
        /// Handles user logout.
        /// </summary>
        public static void Logout()
        {
            userId = 0;
            userRole = UserRoles.Guest;
        }

        /// <summary>
        /// Starts the user menu.
        /// </summary>
        public static void Start()
        {
            ConsoleKey resKey;
            bool updateItems = true;
            do
            {
                resKey = rolesToMenu[userRole].RunOnce(ref updateItems);
            }
            while (resKey != ConsoleKey.Escape);
        }
    }
}