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
    /// Unit tests for the <see cref="CategoryRepository"/> class.
    /// </summary>
    public class CategoryRepositoryTests
    {
        private readonly DbContextOptions<StoreDbContext> _dbContextOptions;
        private readonly AbstractDataFactory _testDataFactory;
        private readonly StoreDbContext _context;
        private readonly CategoryRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryRepositoryTests"/> class.
        /// </summary>
        public CategoryRepositoryTests()
        {
            _dbContextOptions = new DbContextOptionsBuilder<StoreDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            _testDataFactory = new TestDataFactory();
            _context = new StoreDbContext(_dbContextOptions, _testDataFactory);
            _repository = new CategoryRepository(_context);
        }

        /// <summary>
        /// Tests the Add method of <see cref="CategoryRepository"/>.
        /// </summary>
        [Fact]
        public void Add_ShouldAddCategory()
        {
            var category = new Category { Name = "Electronics" };

            _repository.Add(category);
            var result = _context.Categories.FirstOrDefault(c => c.Name == "Electronics");

            Assert.NotNull(result);
            Assert.Equal("Electronics", result.Name);
        }

        /// <summary>
        /// Tests the Delete method of <see cref="CategoryRepository"/>.
        /// </summary>
        [Fact]
        public void Delete_ShouldDeleteCategory()
        {
            var category = new Category { Name = "Electronics" };
            _context.Categories.Add(category);
            _context.SaveChanges();

            _repository.Delete(category);
            var result = _context.Categories.FirstOrDefault(c => c.Name == "Electronics");

            Assert.Null(result);
        }

        /// <summary>
        /// Tests the DeleteById method of <see cref="CategoryRepository"/>.
        /// </summary>
        [Fact]
        public void DeleteById_ShouldDeleteCategoryById()
        {
            var category = new Category { Name = "Electronics" };
            _context.Categories.Add(category);
            _context.SaveChanges();

            _repository.DeleteById(category.Id);
            var result = _context.Categories.FirstOrDefault(c => c.Id == category.Id);

            Assert.Null(result);
        }

        /// <summary>
        /// Tests the GetAll method of <see cref="CategoryRepository"/>.
        /// </summary>
        [Fact]
        public void GetAll_ShouldReturnAllCategories()
        {
            _context.Categories.AddRange(new List<Category>
            {
                new Category { Name = "Electronics" },
                new Category { Name = "Clothing" }
            });
            _context.SaveChanges();

            var result = _repository.GetAll();

            Assert.Equal(2, result.Count());
            Assert.Contains(result, c => c.Name == "Electronics");
            Assert.Contains(result, c => c.Name == "Clothing");
        }

        /// <summary>
        /// Tests the GetAll method with pagination of <see cref="CategoryRepository"/>.
        /// </summary>
        [Fact]
        public void GetAll_WithPagination_ShouldReturnPaginatedCategories()
        {
            _context.Categories.AddRange(new List<Category>
            {
                new Category { Name = "Category 1" },
                new Category { Name = "Category 2" },
                new Category { Name = "Category 3" },
                new Category { Name = "Category 4" }
            });
            _context.SaveChanges();

            var result = _repository.GetAll(1, 2);

            Assert.Equal(2, result.Count());
            Assert.Contains(result, c => c.Name == "Category 1");
            Assert.Contains(result, c => c.Name == "Category 2");

            var result2 = _repository.GetAll(2, 2); 

            Assert.Equal(2, result2.Count());
            Assert.Contains(result2, c => c.Name == "Category 3");
            Assert.Contains(result2, c => c.Name == "Category 4");
        }

        /// <summary>
        /// Tests the GetById method of <see cref="CategoryRepository"/>.
        /// </summary>
        [Fact]
        public void GetById_ShouldReturnCategory()
        {
            var category = new Category { Name = "Electronics" };
            _context.Categories.Add(category);
            _context.SaveChanges();

            var result = _repository.GetById(category.Id);

            Assert.NotNull(result);
            Assert.Equal("Electronics", result.Name);
        }

        /// <summary>
        /// Tests the Update method of <see cref="CategoryRepository"/>.
        /// </summary>
        [Fact]
        public void Update_ShouldUpdateCategory()
        {
            var category = new Category { Name = "Electronics" };
            _context.Categories.Add(category);
            _context.SaveChanges();

            category.Name = "Updated Electronics";
            _repository.Update(category);
            var result = _context.Categories.FirstOrDefault(c => c.Id == category.Id);

            Assert.NotNull(result);
            Assert.Equal("Updated Electronics", result.Name);
        }
    }
}
