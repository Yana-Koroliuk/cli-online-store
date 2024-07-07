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
    /// Unit tests for the <see cref="ProductTitleRepository"/> class.
    /// </summary>
    public class ProductTitleRepositoryTests
    {
        private readonly DbContextOptions<StoreDbContext> _dbContextOptions;
        private readonly AbstractDataFactory _testDataFactory;
        private readonly StoreDbContext _context;
        private readonly ProductTitleRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductTitleRepositoryTests"/> class.
        /// </summary>
        public ProductTitleRepositoryTests()
        {
            _dbContextOptions = new DbContextOptionsBuilder<StoreDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            _testDataFactory = new TestDataFactory();
            _context = new StoreDbContext(_dbContextOptions, _testDataFactory);
            _repository = new ProductTitleRepository(_context);
        }

        /// <summary>
        /// Tests the Add method of <see cref="ProductTitleRepository"/>.
        /// </summary>
        [Fact]
        public void Add_ShouldAddProductTitle()
        {
            var productTitle = new ProductTitle { Title = "Laptop", CategoryId = 1 };

            _repository.Add(productTitle);
            var result = _context.ProductTitles.FirstOrDefault(pt => pt.Title == "Laptop");

            Assert.NotNull(result);
            Assert.Equal("Laptop", result.Title);
        }

        /// <summary>
        /// Tests the Delete method of <see cref="ProductTitleRepository"/>.
        /// </summary>
        [Fact]
        public void Delete_ShouldDeleteProductTitle()
        {
            var productTitle = new ProductTitle { Title = "Laptop", CategoryId = 1 };
            _context.ProductTitles.Add(productTitle);
            _context.SaveChanges();

            _repository.Delete(productTitle);
            var result = _context.ProductTitles.FirstOrDefault(pt => pt.Title == "Laptop");

            Assert.Null(result);
        }

        /// <summary>
        /// Tests the DeleteById method of <see cref="ProductTitleRepository"/>.
        /// </summary>
        [Fact]
        public void DeleteById_ShouldDeleteProductTitleById()
        {
            var productTitle = new ProductTitle { Title = "Laptop", CategoryId = 1 };
            _context.ProductTitles.Add(productTitle);
            _context.SaveChanges();

            _repository.DeleteById(productTitle.Id);
            var result = _context.ProductTitles.FirstOrDefault(pt => pt.Id == productTitle.Id);

            Assert.Null(result);
        }

        /// <summary>
        /// Tests the GetAll method of <see cref="ProductTitleRepository"/>.
        /// </summary>
        [Fact]
        public void GetAll_ShouldReturnAllProductTitles()
        {
            _context.ProductTitles.AddRange(new List<ProductTitle>
            {
                new ProductTitle { Title = "Laptop", CategoryId = 1 },
                new ProductTitle { Title = "Smartphone", CategoryId = 2 }
            });
            _context.SaveChanges();

            var result = _repository.GetAll();

            Assert.Equal(2, result.Count());
            Assert.Contains(result, pt => pt.Title == "Laptop");
            Assert.Contains(result, pt => pt.Title == "Smartphone");
        }

        /// <summary>
        /// Tests the GetAll method with pagination of <see cref="ProductTitleRepository"/>.
        /// </summary>
        [Fact]
        public void GetAll_WithPagination_ShouldReturnPaginatedProductTitles()
        {
            _context.ProductTitles.AddRange(new List<ProductTitle>
            {
                new ProductTitle { Title = "Laptop", CategoryId = 1 },
                new ProductTitle { Title = "Smartphone", CategoryId = 2 },
                new ProductTitle { Title = "Tablet", CategoryId = 3 },
                new ProductTitle { Title = "Smartwatch", CategoryId = 4 }
            });
            _context.SaveChanges();

            var result = _repository.GetAll(1, 2);

            Assert.Equal(2, result.Count());
            Assert.Contains(result, pt => pt.Title == "Laptop");
            Assert.Contains(result, pt => pt.Title == "Smartphone");

            var result2 = _repository.GetAll(2, 2);

            Assert.Equal(2, result2.Count());
            Assert.Contains(result2, pt => pt.Title == "Tablet");
            Assert.Contains(result2, pt => pt.Title == "Smartwatch");
        }

        /// <summary>
        /// Tests the GetById method of <see cref="ProductTitleRepository"/>.
        /// </summary>
        [Fact]
        public void GetById_ShouldReturnProductTitle()
        {
            var productTitle = new ProductTitle { Title = "Laptop", CategoryId = 1 };
            _context.ProductTitles.Add(productTitle);
            _context.SaveChanges();

            var result = _repository.GetById(productTitle.Id);

            Assert.NotNull(result);
            Assert.Equal("Laptop", result.Title);
        }

        /// <summary>
        /// Tests the Update method of <see cref="ProductTitleRepository"/>.
        /// </summary>
        [Fact]
        public void Update_ShouldUpdateProductTitle()
        {
            var productTitle = new ProductTitle { Title = "Laptop", CategoryId = 1 };
            _context.ProductTitles.Add(productTitle);
            _context.SaveChanges();

            productTitle.Title = "Updated Laptop";
            _repository.Update(productTitle);
            var result = _context.ProductTitles.FirstOrDefault(pt => pt.Id == productTitle.Id);

            Assert.NotNull(result);
            Assert.Equal("Updated Laptop", result.Title);
        }
    }
}
