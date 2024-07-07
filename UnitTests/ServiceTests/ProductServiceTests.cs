using Moq;
using StoreBLL.Interfaces;
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
    /// Unit tests for the ProductService class.
    /// </summary>
    public class ProductServiceTests
    {
        private readonly Mock<IProductRepository> mockRepository;
        private readonly ProductService productService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductServiceTests"/> class.
        /// Sets up the mock repository and the ProductService instance.
        /// </summary>
        public ProductServiceTests()
        {
            mockRepository = new Mock<IProductRepository>();
            productService = new ProductService(mockRepository.Object);
        }

        /// <summary>
        /// Tests the Add method to ensure a product is added correctly.
        /// </summary>
        [Fact]
        public void Add_ShouldAddProduct()
        {
            var productModel = new ProductModel(0, 1, 1, "Product Description", 10.5m);

            productService.Add(productModel);

            mockRepository.Verify(r => r.Add(It.IsAny<Product>()), Times.Once);
        }

        /// <summary>
        /// Tests the Delete method to ensure a product is deleted by ID correctly.
        /// </summary>
        [Fact]
        public void Delete_ShouldDeleteProduct()
        {
            var productId = 1;

            productService.Delete(productId);

            mockRepository.Verify(r => r.DeleteById(1), Times.Once);
        }

        /// <summary>
        /// Tests the GetAll method to ensure all products are retrieved correctly.
        /// </summary>
        [Fact]
        public void GetAll_ShouldReturnAllProducts()
        {
            var products = new List<Product>
            {
                new Product(1, 1, 1, "Description 1", 10.0m),
                new Product(2, 2, 2, "Description 2", 20.0m)
            };
            mockRepository.Setup(r => r.GetAll()).Returns(products);

            var result = productService.GetAll();

            Assert.Equal(2, result.Count());
            Assert.Contains(result, p => ((ProductModel)p).Description == "Description 1");
            Assert.Contains(result, p => ((ProductModel)p).Description == "Description 2");
        }

        /// <summary>
        /// Tests the GetById method to ensure a product is retrieved by ID correctly.
        /// </summary>
        [Fact]
        public void GetById_ShouldReturnProduct()
        {
            var product = new Product(1, 1, 1, "Product Description", 10.5m);
            mockRepository.Setup(r => r.GetById(1)).Returns(product);

            var result = (ProductModel)productService.GetById(1);

            Assert.NotNull(result);
            Assert.Equal("Product Description", result.Description);
        }

        /// <summary>
        /// Tests the Update method to ensure a product is updated correctly.
        /// </summary>
        [Fact]
        public void Update_ShouldUpdateProduct()
        {
            var productModel = new ProductModel(1, 1, 1, "Updated Description", 15.0m);
            var product = new Product(1, 1, 1, "Product Description", 10.5m);
            mockRepository.Setup(r => r.GetById(1)).Returns(product);

            productService.Update(productModel);

            mockRepository.Verify(r => r.Update(It.IsAny<Product>()), Times.Once);
            Assert.Equal("Updated Description", product.Description);
            Assert.Equal(15.0m, product.UnitPrice);
        }
    }
}
