using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using StoreDAL.Data.InitDataFactory;

namespace StoreDAL.Data
{
    /// <summary>
    /// Factory for creating the StoreDbContext.
    /// </summary>
    public class StoreDbFactory
    {
        private readonly AbstractDataFactory factory;

        /// <summary>
        /// Initializes a new instance of the <see cref="StoreDbFactory"/> class.
        /// </summary>
        /// <param name="factory">The data factory to use for initializing data.</param>
        public StoreDbFactory(AbstractDataFactory factory)
        {
              this.factory = factory;
        }

        /// <summary>
        /// Creates a new StoreDbContext instance.
        /// </summary>
        /// <returns>The StoreDbContext instance.</returns>
        public StoreDbContext CreateContext()
        {
            var context = new StoreDbContext(this.CreateOptions(), this.factory);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            return context;
        }

        /// <summary>
        /// Creates the DbContextOptions for the StoreDbContext.
        /// </summary>
        /// <returns>The DbContextOptions instance.</returns>
        public DbContextOptions<StoreDbContext> CreateOptions()
        {
            return new DbContextOptionsBuilder<StoreDbContext>()
                .UseSqlite(CreateConnectionString())
                .Options;
        }

        /// <summary>
        /// Creates the connection string for the SQLite database.
        /// </summary>
        /// <returns>The connection string.</returns>
        private static string CreateConnectionString()
        {
            var dbPath = "store.db";
            var conString = new SqliteConnectionStringBuilder { DataSource = dbPath, Mode = SqliteOpenMode.ReadWriteCreate }.ConnectionString;
            return conString;
        }
    }
}
