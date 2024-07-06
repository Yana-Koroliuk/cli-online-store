using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ConsoleApp.Helpers;
using ConsoleApp1;
using StoreBLL.Models;
using StoreBLL.Services;
using StoreDAL.Data;
using StoreDAL.Entities;

namespace ConsoleApp.Controllers
{
    /// <summary>
    /// Provides methods for managing products.
    /// </summary>
    public static class ProductController
    {
        private static ProductService productService = UserMenuController.GetService<ProductService>();
        private static ProductTitleService productTitleService = UserMenuController.GetService<ProductTitleService>();
        private static CategoryService categoryService = UserMenuController.GetService<CategoryService>();
        private static ManufacturerService manufacturerService = UserMenuController.GetService<ManufacturerService>();

        /// <summary>
        /// Adds a new product.
        /// </summary>
        public static void AddProduct()
        {
            try
            {
                var (productName, categoryName, description, unitPrice) = InputHelper.ReadDataAddProduct();
                var existingCategory = categoryService.GetByName(categoryName) ?? categoryService.Create(categoryName);
                var productTitle = productTitleService.GetByNameAndCategoryId(productName, existingCategory.Id) ?? productTitleService.Create(productName, existingCategory.Id);
                var product = new ProductModel(0, productTitle.Id, null, description, unitPrice);
                productService.Add(product);
                Console.WriteLine("Product added successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Updates an existing product.
        /// </summary>
        public static void UpdateProduct()
        {
            try
            {
                var productId = InputHelper.ReadProductId();
                var product = (ProductModel)productService.GetById(productId);
                var productTitle = (ProductTitleModel)productTitleService.GetById(product.TitleId);
                var category = (CategoryModel)categoryService.GetById(productTitle.CategoryId);
                var manufacturerId = product.ManufacturerId ?? 0;
                var manufacturer = manufacturerId != 0 ? (ManufacturerModel)manufacturerService.GetById(manufacturerId) : null;

                Console.WriteLine($"Current Name: {productTitle.Title}, Current Category: {category.Name}, Current Manufacturer: {manufacturer?.Name ?? "N/A"}," +
                    $" Current Description: {product.Description}, Current Unit Price: {product.UnitPrice}");
                var (newName, newCategory, newManufacturer, newDescription, newUnitPrice) = InputHelper.ReadDataUpdateProduct();
                if (!string.IsNullOrEmpty(newCategory))
                {
                    category = categoryService.GetByName(newCategory) ?? categoryService.Create(newCategory);
                }

                if (!string.IsNullOrEmpty(newName))
                {
                    productTitle = productTitleService.GetByNameAndCategoryId(newName, category.Id)
                       ?? productTitleService.Create(newName, category.Id);
                }
                else
                {
                    productTitle = productTitleService.GetByNameAndCategoryId(productTitle.Title, category.Id)
                        ?? productTitleService.Create(productTitle.Title, category.Id);
                }

                if (!string.IsNullOrEmpty(newManufacturer))
                {
                    manufacturer = manufacturerService.GetByName(newManufacturer)
                        ?? manufacturerService.Create(newManufacturer);
                    product.ManufacturerId = manufacturer.Id;
                }

                if (!string.IsNullOrEmpty(newDescription))
                {
                    product.Description = newDescription;
                }

                if (newUnitPrice != 0)
                {
                    product.UnitPrice = newUnitPrice;
                }

                product.TitleId = productTitle.Id;
                productService.Update(product);
                Console.WriteLine("Product updated successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Shows all products.
        /// </summary>
        public static void ShowAllProducts()
        {
            var products = productService.GetAll().OfType<ProductModel>();

            Console.WriteLine("Products:");
            foreach (var product in products)
            {
                var productTitle = (ProductTitleModel)productTitleService.GetById(product.TitleId);
                var title = productTitle != null ? productTitle.Title : "Unknown";
                var manufacturerId = product.ManufacturerId ?? 0;
                var manufacturer = manufacturerId != 0 ? (ManufacturerModel)manufacturerService.GetById(manufacturerId) : null;
                var manufacturerName = manufacturer != null ? manufacturer.Name : "Unknown";
                Console.WriteLine($"Id: {product.Id}, Title: {title}, Manufacture: {manufacturerName}, Description: {product.Description}, UnitPrice: ${product.UnitPrice}");
            }
        }
    }
}