using Moq;
using StoreBLL.Models;
using StoreBLL.Services;
using StoreDAL.Entities;
using StoreDAL.Interfaces;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace UnitTests.ServiceTests
{
    /// <summary>
    /// Unit tests for the <see cref="ProductTitleService "/> class.
    /// </summary>
    public class ProductTitleServiceTests
    {
        private readonly Mock<IProductTitleRepository> mockRepository;
        private readonly ProductTitleService productTitleService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductTitleServiceTests"/> class.
        /// Sets up the mock repository and the ProductTitleService instance.
        /// </summary>
        public ProductTitleServiceTests()
        {
            mockRepository = new Mock<IProductTitleRepository>();
            productTitleService = new ProductTitleService(mockRepository.Object);
        }

        /// <summary>
        /// Tests the Add method to ensure a product title is added correctly.
        /// </summary>
        [Fact]
        public void Add_ShouldAddProductTitle()
        {
            var productTitleModel = new ProductTitleModel(0, "New Product", 1);

            productTitleService.Add(productTitleModel);

            mockRepository.Verify(r => r.Add(It.IsAny<ProductTitle>()), Times.Once);
        }

        /// <summary>
        /// Tests the Delete method to ensure a product title is deleted by ID correctly.
        /// </summary>
        [Fact]
        public void Delete_ShouldDeleteProductTitle()
        {
            var productTitleId = 1;

            productTitleService.Delete(productTitleId);

            mockRepository.Verify(r => r.DeleteById(1), Times.Once);
        }

        /// <summary>
        /// Tests the GetAll method to ensure all product titles are retrieved correctly.
        /// </summary>
        [Fact]
        public void GetAll_ShouldReturnAllProductTitles()
        {
            var productTitles = new List<ProductTitle>
            {
                new ProductTitle(1, "Product 1", 1),
                new ProductTitle(2, "Product 2", 2)
            };
            mockRepository.Setup(r => r.GetAll()).Returns(productTitles);

            var result = productTitleService.GetAll();

            Assert.Equal(2, result.Count());
            Assert.Contains(result, pt => ((ProductTitleModel)pt).Title == "Product 1");
            Assert.Contains(result, pt => ((ProductTitleModel)pt).Title == "Product 2");
        }

        /// <summary>
        /// Tests the GetById method to ensure a product title is retrieved by ID correctly.
        /// </summary>
        [Fact]
        public void GetById_ShouldReturnProductTitle()
        {
            var productTitle = new ProductTitle(1, "Product 1", 1);
            mockRepository.Setup(r => r.GetById(1)).Returns(productTitle);

            var result = (ProductTitleModel)productTitleService.GetById(1);

            Assert.NotNull(result);
            Assert.Equal("Product 1", result.Title);
        }

        /// <summary>
        /// Tests the GetByNameAndCategoryId method to ensure a product title is retrieved by name and category ID correctly.
        /// </summary>
        [Fact]
        public void GetByNameAndCategoryId_ShouldReturnProductTitle()
        {
            var productTitle = new ProductTitle(1, "Product 1", 1);
            mockRepository.Setup(r => r.GetAll()).Returns(new List<ProductTitle> { productTitle });

            var result = productTitleService.GetByNameAndCategoryId("Product 1", 1);

            Assert.NotNull(result);
            Assert.Equal("Product 1", result.Title);
        }

        /// <summary>
        /// Tests the Create method to ensure a new product title is created correctly.
        /// </summary>
        [Fact]
        public void Create_ShouldCreateProductTitle()
        {
            var productName = "New Product";
            var categoryId = 1;
            mockRepository.Setup(r => r.GetAll()).Returns(new List<ProductTitle>
            {
                new ProductTitle(1, productName, categoryId)
            });

            var result = productTitleService.Create(productName, categoryId);

            mockRepository.Verify(r => r.Add(It.IsAny<ProductTitle>()), Times.Once);
            Assert.Equal(productName, result.Title);
        }

        /// <summary>
        /// Tests the Update method to ensure a product title is updated correctly.
        /// </summary>
        [Fact]
        public void Update_ShouldUpdateProductTitle()
        {
            var productTitleModel = new ProductTitleModel(1, "Updated Product", 1);
            var productTitle = new ProductTitle(1, "Product 1", 1);
            mockRepository.Setup(r => r.GetById(1)).Returns(productTitle);

            productTitleService.Update(productTitleModel);

            mockRepository.Verify(r => r.Update(It.IsAny<ProductTitle>()), Times.Once);
            Assert.Equal("Updated Product", productTitle.Title);
        }
    }
}
