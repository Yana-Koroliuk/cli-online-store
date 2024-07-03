namespace StoreDAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using StoreDAL.Data.InitDataFactory;
using StoreDAL.Entities;

/// <summary>
/// The DbContext for the store database.
/// </summary>
public class StoreDbContext : DbContext
{
    private readonly AbstractDataFactory factory;

    /// <summary>
    /// Initializes a new instance of the <see cref="StoreDbContext"/> class.
    /// </summary>
    /// <param name="options">The options to be used by a DbContext.</param>
    /// <param name="factory">The data factory to use for seeding data.</param>
    public StoreDbContext(DbContextOptions options, AbstractDataFactory factory)
        : base(options)
    {
        this.factory = factory;
    }

    /// <summary>
    /// Gets or sets the Categories DbSet.
    /// </summary>
    public DbSet<Category> Categories { get; set; }

    /// <summary>
    /// Gets or sets the CustomerOrders DbSet.
    /// </summary>
    public DbSet<CustomerOrder> CustomerOrders { get; set; }

    /// <summary>
    /// Gets or sets the Manufacturers DbSet.
    /// </summary>
    public DbSet<Manufacturer> Manufacturers { get; set; }

    /// <summary>
    /// Gets or sets the OrderDetails DbSet.
    /// </summary>
    public DbSet<OrderDetail> OrderDetails { get; set; }

    /// <summary>
    /// Gets or sets the OrderStates DbSet.
    /// </summary>
    public DbSet<OrderState> OrderStates { get; set; }

    /// <summary>
    /// Gets or sets the Products DbSet.
    /// </summary>
    public DbSet<Product> Products { get; set; }

    /// <summary>
    /// Gets or sets the ProductTitles DbSet.
    /// </summary>
    public DbSet<ProductTitle> ProductTitles { get; set; }

    /// <summary>
    /// Gets or sets the Users DbSet.
    /// </summary>
    public DbSet<User> Users { get; set; }

    /// <summary>
    /// Gets or sets the UserRoles DbSet.
    /// </summary>
    public DbSet<UserRole> UserRoles { get; set; }

    /// <summary>
    /// Configures the model relationships and seeds the data.
    /// </summary>
    /// <param name="modelBuilder">The model builder to be used for configuration.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>().HasData(this.factory.GetCategoryData());
        modelBuilder.Entity<Manufacturer>().HasData(this.factory.GetManufacturerData());
        modelBuilder.Entity<OrderState>().HasData(this.factory.GetOrderStateData());
        modelBuilder.Entity<UserRole>().HasData(this.factory.GetUserRoleData());
        modelBuilder.Entity<User>().HasData(this.factory.GetUserData());
        modelBuilder.Entity<ProductTitle>().HasData(this.factory.GetProductTitleData());
        modelBuilder.Entity<Product>().HasData(this.factory.GetProductData());
        modelBuilder.Entity<CustomerOrder>().HasData(this.factory.GetCustomerOrderData());
        modelBuilder.Entity<OrderDetail>().HasData(this.factory.GetOrderDetailData());
    }
}
