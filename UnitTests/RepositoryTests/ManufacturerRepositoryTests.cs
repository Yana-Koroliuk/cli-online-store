using Microsoft.EntityFrameworkCore;
using StoreDAL.Data;
using StoreDAL.Data.InitDataFactory;
using StoreDAL.Entities;
using StoreDAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace UnitTests.RepositoryTests
{
    /// <summary>
    /// Unit tests for the <see cref="ManufacturerRepository"/> class.
    /// </summary>
    public class ManufacturerRepositoryTests
    {
        private readonly DbContextOptions<StoreDbContext> _dbContextOptions;
        private readonly AbstractDataFactory _testDataFactory;
        private readonly StoreDbContext _context;
        private readonly ManufacturerRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ManufacturerRepositoryTests"/> class.
        /// </summary>
        public ManufacturerRepositoryTests()
        {
            _dbContextOptions = new DbContextOptionsBuilder<StoreDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            _testDataFactory = new TestDataFactory();
            _context = new StoreDbContext(_dbContextOptions, _testDataFactory);
            _repository = new ManufacturerRepository(_context);
        }

        /// <summary>
        /// Tests the Add method of <see cref="ManufacturerRepository"/>.
        /// </summary>
        [Fact]
        public void Add_ShouldAddManufacturer()
        {
            var manufacturer = new Manufacturer { Name = "Manufacturer 1" };

            _repository.Add(manufacturer);
            var result = _context.Manufacturers.FirstOrDefault(m => m.Name == "Manufacturer 1");

            Assert.NotNull(result);
            Assert.Equal("Manufacturer 1", result.Name);
        }

        /// <summary>
        /// Tests the Delete method of <see cref="ManufacturerRepository"/>.
        /// </summary>
        [Fact]
        public void Delete_ShouldDeleteManufacturer()
        {
            var manufacturer = new Manufacturer { Name = "Manufacturer 1" };
            _context.Manufacturers.Add(manufacturer);
            _context.SaveChanges();

            _repository.Delete(manufacturer);
            var result = _context.Manufacturers.FirstOrDefault(m => m.Name == "Manufacturer 1");

            Assert.Null(result);
        }

        /// <summary>
        /// Tests the DeleteById method of <see cref="ManufacturerRepository"/>.
        /// </summary>
        [Fact]
        public void DeleteById_ShouldDeleteManufacturerById()
        {
            var manufacturer = new Manufacturer { Name = "Manufacturer 1" };
            _context.Manufacturers.Add(manufacturer);
            _context.SaveChanges();

            _repository.DeleteById(manufacturer.Id);
            var result = _context.Manufacturers.FirstOrDefault(m => m.Id == manufacturer.Id);

            Assert.Null(result);
        }

        /// <summary>
        /// Tests the GetAll method of <see cref="ManufacturerRepository"/>.
        /// </summary>
        [Fact]
        public void GetAll_ShouldReturnAllManufacturers()
        {
            _context.Manufacturers.AddRange(new List<Manufacturer>
            {
                new Manufacturer { Name = "Manufacturer 1" },
                new Manufacturer { Name = "Manufacturer 2" }
            });
            _context.SaveChanges();

            var result = _repository.GetAll();

            Assert.Equal(2, result.Count());
            Assert.Contains(result, m => m.Name == "Manufacturer 1");
            Assert.Contains(result, m => m.Name == "Manufacturer 2");
        }

        /// <summary>
        /// Tests the GetAll method with pagination of <see cref="ManufacturerRepository"/>.
        /// </summary>
        [Fact]
        public void GetAll_WithPagination_ShouldReturnPaginatedManufacturers()
        {
            _context.Manufacturers.AddRange(new List<Manufacturer>
            {
                new Manufacturer { Name = "Manufacturer 1" },
                new Manufacturer { Name = "Manufacturer 2" },
                new Manufacturer { Name = "Manufacturer 3" },
                new Manufacturer { Name = "Manufacturer 4" }
            });
            _context.SaveChanges();

            var result = _repository.GetAll(1, 2); 

            Assert.Equal(2, result.Count());
            Assert.Contains(result, m => m.Name == "Manufacturer 1");
            Assert.Contains(result, m => m.Name == "Manufacturer 2");

            var result2 = _repository.GetAll(2, 2);

            Assert.Equal(2, result2.Count());
            Assert.Contains(result2, m => m.Name == "Manufacturer 3");
            Assert.Contains(result2, m => m.Name == "Manufacturer 4");
        }

        /// <summary>
        /// Tests the GetById method of <see cref="ManufacturerRepository"/>.
        /// </summary>
        [Fact]
        public void GetById_ShouldReturnManufacturer()
        {
            var manufacturer = new Manufacturer { Name = "Manufacturer 1" };
            _context.Manufacturers.Add(manufacturer);
            _context.SaveChanges();

            var result = _repository.GetById(manufacturer.Id);

            Assert.NotNull(result);
            Assert.Equal("Manufacturer 1", result.Name);
        }

        /// <summary>
        /// Tests the Update method of <see cref="ManufacturerRepository"/>.
        /// </summary>
        [Fact]
        public void Update_ShouldUpdateManufacturer()
        {
            var manufacturer = new Manufacturer { Name = "Manufacturer 1" };
            _context.Manufacturers.Add(manufacturer);
            _context.SaveChanges();

            manufacturer.Name = "Updated Manufacturer";
            _repository.Update(manufacturer);
            var result = _context.Manufacturers.FirstOrDefault(m => m.Id == manufacturer.Id);

            Assert.NotNull(result);
            Assert.Equal("Updated Manufacturer", result.Name);
        }
    }
}
