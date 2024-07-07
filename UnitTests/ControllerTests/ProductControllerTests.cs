using ConsoleApp.Controllers;
using ConsoleApp.Helpers;
using Moq;
using StoreBLL.Interfaces;
using StoreBLL.Models;
using StoreBLL.Services;
using System;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace UnitTests.ControllerTests
{
    /// <summary>
    /// Unit tests for the <see cref="ProductController"/> class.
    /// </summary>
    public class ProductControllerTests
    {
        private readonly Mock<ICrud> _mockProductService;
        private readonly Mock<ICrud> _mockProductTitleService;
        private readonly Mock<ICrud> _mockManufacturerService;
       
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductControllerTests"/> class.
        /// </summary>
        public ProductControllerTests()
        {
            _mockProductService = new Mock<ICrud>();
            _mockProductTitleService = new Mock<ICrud>();
            _mockManufacturerService = new Mock<ICrud>();
        }

        /// <summary>
        /// Tests the <see cref="ProductController.ShowAllProducts(ICrud, ICrud, ICrud)"/> method to ensure it displays all products.
        /// </summary>
        [Fact]
        public void ShowAllProducts_ShouldDisplayAllProducts()
        {
            var products = new List<ProductModel>
            {
                new ProductModel(1, 1, 1, "Description1", 100),
                new ProductModel(2, 2, 2, "Description2", 200)
            };
            var productTitles = new List<ProductTitleModel>
            {
                new ProductTitleModel(1, "Product1", 1),
                new ProductTitleModel(2, "Product2",2)
            };
            var manufacturers = new List<ManufacturerModel>
            {
                new ManufacturerModel(1, "Manufacturer1"),
                new ManufacturerModel(2, "Manufacturer2")
            };
            _mockProductService.Setup(s => s.GetAll()).Returns(products);
            _mockProductTitleService.Setup(s => s.GetById(It.IsAny<int>())).Returns((int id) => productTitles.Find(pt => pt.Id == id)!);
            _mockManufacturerService.Setup(s => s.GetById(It.IsAny<int>())).Returns((int id) => manufacturers.Find(m => m.Id == id)!);

            ProductController.ShowAllProducts(_mockProductService.Object, _mockProductTitleService.Object, _mockManufacturerService.Object);

            _mockProductService.Verify(s => s.GetAll(), Times.Once);
        }
    }
}
