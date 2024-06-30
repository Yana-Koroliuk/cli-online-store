using ConsoleMenu;
using ConsoleMenu.Builder;
using StoreBLL.Services;
using StoreDAL.Data;
using StoreDAL.Data.InitDataFactory;

namespace ConsoleApp1;

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
    private static readonly Dictionary<UserRoles, Menu> RolesToMenu;
    private static int userId;
    private static UserRoles userRole;
    private static StoreDbContext context;

    static UserMenuController()
    {
        userId = 0;
        userRole = UserRoles.Guest;
        RolesToMenu = new Dictionary<UserRoles, Menu>();
        var factory = new StoreDbFactory(new TestDataFactory());
        context = factory.CreateContext();
        RolesToMenu.Add(UserRoles.Guest, new GuestMainMenu().Create(context));
        RolesToMenu.Add(UserRoles.RegistredCustomer, new UserMainMenu().Create(context));
        RolesToMenu.Add(UserRoles.Administrator, new AdminMainMenu().Create(context));
    }

    /// <summary>
    /// Gets the database context.
    /// </summary>
    public static StoreDbContext Context
    {
        get { return context; }
    }

    /// <summary>
    /// Gets the User ID.
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
            var userService = new UserService(context);
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
                resKey = RolesToMenu[userRole].RunOnce(ref updateItems);
        }
        while (resKey != ConsoleKey.Escape);
    }
}