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
    /// Unit tests for the <see cref="ProductRepository"/> class.
    /// </summary>
    public class ProductRepositoryTests
    {
        private readonly DbContextOptions<StoreDbContext> _dbContextOptions;
        private readonly AbstractDataFactory _testDataFactory;
        private readonly StoreDbContext _context;
        private readonly ProductRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductRepositoryTests"/> class.
        /// </summary>
        public ProductRepositoryTests()
        {
            _dbContextOptions = new DbContextOptionsBuilder<StoreDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            _testDataFactory = new TestDataFactory();
            _context = new StoreDbContext(_dbContextOptions, _testDataFactory);
            _repository = new ProductRepository(_context);
        }

        /// <summary>
        /// Tests the Add method of <see cref="ProductRepository"/>.
        /// </summary>
        [Fact]
        public void Add_ShouldAddProduct()
        {
            var product = new Product { TitleId = 1, ManufacturerId = 1, Description = "A new product", UnitPrice = 10.99m };

            _repository.Add(product);
            var result = _context.Products.FirstOrDefault(p => p.Description == "A new product");

            Assert.NotNull(result);
            Assert.Equal("A new product", result.Description);
        }

        /// <summary>
        /// Tests the Delete method of <see cref="ProductRepository"/>.
        /// </summary>
        [Fact]
        public void Delete_ShouldDeleteProduct()
        {
            var product = new Product { TitleId = 1, ManufacturerId = 1, Description = "A new product", UnitPrice = 10.99m };
            _context.Products.Add(product);
            _context.SaveChanges();

            _repository.Delete(product);
            var result = _context.Products.FirstOrDefault(p => p.Description == "A new product");

            Assert.Null(result);
        }

        /// <summary>
        /// Tests the DeleteById method of <see cref="ProductRepository"/>.
        /// </summary>
        [Fact]
        public void DeleteById_ShouldDeleteProductById()
        {
            var product = new Product { TitleId = 1, ManufacturerId = 1, Description = "A new product", UnitPrice = 10.99m };
            _context.Products.Add(product);
            _context.SaveChanges();

            _repository.DeleteById(product.Id);
            var result = _context.Products.FirstOrDefault(p => p.Id == product.Id);

            Assert.Null(result);
        }

        /// <summary>
        /// Tests the GetAll method of <see cref="ProductRepository"/>.
        /// </summary>
        [Fact]
        public void GetAll_ShouldReturnAllProducts()
        {
            _context.Products.AddRange(new List<Product>
            {
                new Product { TitleId = 1, ManufacturerId = 1, Description = "Product 1", UnitPrice = 10.99m },
                new Product { TitleId = 2, ManufacturerId = 2, Description = "Product 2", UnitPrice = 15.99m }
            });
            _context.SaveChanges();

            var result = _repository.GetAll();

            Assert.Equal(2, result.Count());
            Assert.Contains(result, p => p.Description == "Product 1");
            Assert.Contains(result, p => p.Description == "Product 2");
        }

        /// <summary>
        /// Tests the GetAll method with pagination of <see cref="ProductRepository"/>.
        /// </summary>
        [Fact]
        public void GetAll_WithPagination_ShouldReturnPaginatedProducts()
        {
            _context.Products.AddRange(new List<Product>
            {
                new Product { TitleId = 1, ManufacturerId = 1, Description = "Product 1", UnitPrice = 10.99m },
                new Product { TitleId = 2, ManufacturerId = 2, Description = "Product 2", UnitPrice = 15.99m },
                new Product { TitleId = 3, ManufacturerId = 3, Description = "Product 3", UnitPrice = 20.99m },
                new Product { TitleId = 4, ManufacturerId = 4, Description = "Product 4", UnitPrice = 25.99m }
            });
            _context.SaveChanges();

            var result = _repository.GetAll(1, 2);

            Assert.Equal(2, result.Count());
            Assert.Contains(result, p => p.Description == "Product 1");
            Assert.Contains(result, p => p.Description == "Product 2");

            var result2 = _repository.GetAll(2, 2); 

            Assert.Equal(2, result2.Count());
            Assert.Contains(result2, p => p.Description == "Product 3");
            Assert.Contains(result2, p => p.Description == "Product 4");
        }

        /// <summary>
        /// Tests the GetById method of <see cref="ProductRepository"/>.
        /// </summary>
        [Fact]
        public void GetById_ShouldReturnProduct()
        {
            var product = new Product { TitleId = 1, ManufacturerId = 1, Description = "Product 1", UnitPrice = 10.99m };
            _context.Products.Add(product);
            _context.SaveChanges();

            var result = _repository.GetById(product.Id);

            Assert.NotNull(result);
            Assert.Equal("Product 1", result.Description);
        }

        /// <summary>
        /// Tests the Update method of <see cref="ProductRepository"/>.
        /// </summary>
        [Fact]
        public void Update_ShouldUpdateProduct()
        {
            var product = new Product { TitleId = 1, ManufacturerId = 1, Description = "Product 1", UnitPrice = 10.99m };
            _context.Products.Add(product);
            _context.SaveChanges();

            product.Description = "Updated Product";
            _repository.Update(product);
            var result = _context.Products.FirstOrDefault(p => p.Id == product.Id);

            Assert.NotNull(result);
            Assert.Equal("Updated Product", result.Description);
        }
    }
}
