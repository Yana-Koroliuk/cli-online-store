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
        private readonly Mock<IProductTitleService> _mockProductTitleService;
        private readonly Mock<IExtendedCrud> _mockCategoryService;
        private readonly Mock<IExtendedCrud> _mockManufacturerService;
       
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductControllerTests"/> class.
        /// </summary>
        public ProductControllerTests()
        {
            _mockProductService = new Mock<ICrud>();
            _mockProductTitleService = new Mock<IProductTitleService>();
            _mockCategoryService = new Mock<IExtendedCrud>();
            _mockManufacturerService = new Mock<IExtendedCrud>();
        }

        /// <summary>
        /// Tests the <see cref="ProductController.AddProduct(ICrud, IProductTitleService, IExtendedCrud)"/> method to ensure it adds a new product.
        /// </summary>
        [Fact]
        public void AddProduct_ShouldAddNewProduct()
        {
            _mockCategoryService.Setup(s => s.GetByName(It.IsAny<string>())).Returns((CategoryModel)null!);
            _mockCategoryService.Setup(s => s.Create(It.IsAny<string>())).Returns(new CategoryModel(1, "Category"));
            _mockProductTitleService.Setup(s => s.GetByNameAndCategoryId(It.IsAny<string>(), It.IsAny<int>())).Returns((ProductTitleModel)null!);
            _mockProductTitleService.Setup(s => s.Create(It.IsAny<string>(), It.IsAny<int>())).Returns(new ProductTitleModel(1, "Product", 1));
            _mockProductService.Setup(s => s.Add(It.IsAny<AbstractModel>())).Verifiable();

            Console.SetIn(new StringReader("Product\nCategory\nDescription\n100\n"));

            ProductController.AddProduct(_mockProductService.Object, _mockProductTitleService.Object, _mockCategoryService.Object);

            _mockProductService.Verify(s => s.Add(It.IsAny<AbstractModel>()), Times.Once);
        }

        /// <summary>
        /// Tests the <see cref="ProductController.UpdateProduct(ICrud, IProductTitleService, IExtendedCrud, IExtendedCrud)"/> method to ensure it updates an existing product.
        /// </summary>
        [Fact]
        public void UpdateProduct_ShouldUpdateExistingProduct()
        {
            var product = new ProductModel(1, 1, null, "Description", 100);
            var productTitle = new ProductTitleModel(1, "Product", 1);
            var category = new CategoryModel(1, "Category");
            var manufacturer = new ManufacturerModel(1, "Manufacturer");

            _mockProductService.Setup(s => s.GetById(It.IsAny<int>())).Returns(product);
            _mockProductTitleService.Setup(s => s.GetById(It.IsAny<int>())).Returns(productTitle);
            _mockCategoryService.Setup(s => s.GetById(It.IsAny<int>())).Returns(category);
            _mockManufacturerService.Setup(s => s.GetById(It.IsAny<int>())).Returns(manufacturer);
            _mockCategoryService.Setup(s => s.GetByName(It.IsAny<string>())).Returns(new CategoryModel(2, "Electronics"));
            _mockProductTitleService.Setup(s => s.GetByNameAndCategoryId(It.IsAny<string>(), It.IsAny<int>())).Returns(new ProductTitleModel(2, "New Product", 1));
            _mockManufacturerService.Setup(s => s.GetByName(It.IsAny<string>())).Returns(new ManufacturerModel(2, "New Manufacturer"));


            _mockProductService.Setup(s => s.Update(It.IsAny<AbstractModel>())).Verifiable();

            Console.SetIn(new StringReader("1\nNewProduct\nNewCategory\nNewManufacturer\nNewDescription\n200\n"));

            ProductController.UpdateProduct(_mockProductService.Object, _mockProductTitleService.Object, _mockCategoryService.Object, _mockManufacturerService.Object);

            _mockProductService.Verify(s => s.Update(It.IsAny<AbstractModel>()), Times.Once);
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
