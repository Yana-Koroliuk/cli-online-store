using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Moq;
using StoreBLL.Interfaces;
using StoreBLL.Models;
using StoreBLL.Services;
using StoreDAL.Interfaces;
using StoreDAL.Repository;
using StoreDAL.Entities;
using StoreDAL.Data;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace UnitTests.ServiceTests
{
    /// <summary>
    /// Unit tests for the <see cref="CategoryService"/> class.
    /// </summary>
    public class CategoryServiceTests
    {
        private readonly Mock<ICategoryRepository> mockRepository;
        private readonly CategoryService categoryService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryServiceTests"/> class.
        /// </summary>
        public CategoryServiceTests()
        {
            mockRepository = new Mock<ICategoryRepository>();
            categoryService = new CategoryService(mockRepository.Object);
        }

        /// <summary>
        /// Tests the Add method of <see cref="CategoryService"/> class.
        /// </summary>
        [Fact]
        public void Add_ShouldAddCategory()
        {
            var categoryModel = new CategoryModel(0, "Electronics");

            categoryService.Add(categoryModel);

            mockRepository.Verify(r => r.Add(It.IsAny<Category>()), Times.Once);
        }

        /// <summary>
        /// Tests the Delete method of <see cref="CategoryService"/> class.
        /// </summary>
        [Fact]
        public void Delete_ShouldDeleteCategory()
        {
            var categoryId = 1;

            categoryService.Delete(categoryId);

            mockRepository.Verify(r => r.DeleteById(categoryId), Times.Once);
        }

        /// <summary>
        /// Tests the GetAll method of <see cref="CategoryService"/> class.
        /// </summary>
        [Fact]
        public void GetAll_ShouldReturnAllCategories()
        {
            var categories = new List<Category>
            {
                new Category(1, "Electronics"),
                new Category(2, "Clothing")
            };
            mockRepository.Setup(r => r.GetAll()).Returns(categories);

            var result = categoryService.GetAll();

            Assert.Equal(2, result.Count());
            Assert.IsType<CategoryModel>(result.First());
        }

        /// <summary>
        /// Tests the GetById method of <see cref="CategoryService"/> class.
        /// </summary>
        [Fact]
        public void GetById_ShouldReturnCategory()
        {
            var category = new Category(1, "Electronics");
            mockRepository.Setup(r => r.GetById(1)).Returns(category);

            var result = categoryService.GetById(1);

            Assert.NotNull(result);
            Assert.Equal("Electronics", ((CategoryModel)result).Name);
        }

        /// <summary>
        /// Tests the GetByName method of <see cref="CategoryService"/> class.
        /// </summary>
        [Fact]
        public void GetByName_ShouldReturnCategory()
        {
            var category = new Category(1, "Electronics");
            mockRepository.Setup(r => r.GetAll()).Returns(new List<Category> { category });

            var result = categoryService.GetByName("Electronics");

            Assert.NotNull(result);
            Assert.Equal("Electronics", result.Name);
        }

        /// <summary>
        /// Tests the Create method of <see cref="CategoryService"/> class.
        /// </summary>
        [Fact]
        public void Create_ShouldCreateCategory()
        {
            var categoryName = "Electronics";
            var newCategory = new Category(1, categoryName);
            var categoryList = new List<Category> { newCategory };

            mockRepository.Setup(r => r.GetAll()).Returns(categoryList);

            var result = categoryService.Create(categoryName);

            mockRepository.Verify(r => r.Add(It.IsAny<Category>()), Times.Once);
            Assert.Equal(categoryName, result.Name);
            Assert.Equal(1, result.Id);
        }

        // <summary>
        /// Tests the Update method of <see cref="CategoryService"/> class.
        /// </summary>
        [Fact]
        public void Update_ShouldUpdateCategory()
        {
            var categoryModel = new CategoryModel(1, "Electronics");
            var category = new Category(1, "OldName");
            mockRepository.Setup(r => r.GetById(1)).Returns(category);

            categoryService.Update(categoryModel);

            mockRepository.Verify(r => r.Update(It.Is<Category>(c => c.Name == "Electronics")), Times.Once);
        }
    }
}

