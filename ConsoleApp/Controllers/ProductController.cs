using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
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
        private static StoreDbContext context = UserMenuController.Context;

        /// <summary>
        /// Adds a new product.
        /// </summary>
        public static void AddProduct()
        {
            var productService = new ProductService(context);
            var productTitleService = new ProductTitleService(context);
            var categoryService = new CategoryService(context);

            Console.WriteLine("Add new product:");
            Console.WriteLine("Product Name: ");
            var productName = Console.ReadLine();

            Console.WriteLine("Category Name: ");
            var categoryName = Console.ReadLine();

            Console.WriteLine("Description: ");
            var description = Console.ReadLine();

            Console.WriteLine("Unit Price: ");
            var unitPriceInput = Console.ReadLine();

            if (string.IsNullOrEmpty(productName) || string.IsNullOrEmpty(categoryName) ||
            string.IsNullOrEmpty(description))
            {
                Console.WriteLine("Input data cannot be empty.");
                return;
            }

            if (decimal.TryParse(unitPriceInput, out var unitPrice))
            {
                var existingCategory = categoryService.GetAll().FirstOrDefault(c => ((CategoryModel)c).Name.Equals(categoryName, StringComparison.OrdinalIgnoreCase));
                if (existingCategory == null)
                {
                    var newCategory = new CategoryModel(0, categoryName);
                    categoryService.Add(newCategory);
                    existingCategory = (CategoryModel)categoryService.GetAll().Last();
                }

                var productTitle = new ProductTitleModel(0, productName, existingCategory.Id);
                productTitleService.Add(productTitle);
                var addedProductTitle = (ProductTitleModel)productTitleService.GetAll().Last();

                var product = new ProductModel(0, addedProductTitle.Id, null, description, unitPrice);
                productService.Add(product);

                Console.WriteLine("Product added successfully.");
            }
            else
            {
                Console.WriteLine("Invalid unit price.");
            }
        }

        /// <summary>
        /// Updates an existing product.
        /// </summary>
        public static void UpdateProduct()
        {
            var productService = new ProductService(context);
            var categoryService = new CategoryService(context);
            var productTitleService = new ProductTitleService(context);
            var manufacturerService = new ManufacturerService(context);

            Console.WriteLine("Enter product ID to update:");
            var productIdInput = Console.ReadLine();

            if (int.TryParse(productIdInput, out var productId))
            {
                var product = (ProductModel)productService.GetById(productId);
                if (product == null)
                {
                    Console.WriteLine("Product not found.");
                    return;
                }

                var productTitle = (ProductTitleModel)productTitleService.GetById(product.TitleId);
                var category = (CategoryModel)categoryService.GetById(productTitle.CategoryId);
                var manufacturerId = product.ManufacturerId ?? 0;
                var manufacturer = manufacturerId != 0 ? (ManufacturerModel)manufacturerService.GetById(manufacturerId) : null;

                Console.WriteLine($"Current Name: {productTitle.Title}, Current Category: {category.Name}, Current Manufacturer: {manufacturer?.Name ?? "N/A"}, Current Description: {product.Description}, Current Unit Price: {product.UnitPrice}");
                Console.WriteLine("Enter new product name (leave empty to keep current): ");
                var newName = Console.ReadLine();
                Console.WriteLine("Enter new category name (leave empty to keep current): ");
                var newCategory = Console.ReadLine();
                Console.WriteLine("Enter new manufacturer name (leave empty to keep current): ");
                var newManufacturer = Console.ReadLine();
                Console.WriteLine("Enter new description (leave empty to keep current): ");
                var newDescription = Console.ReadLine();
                Console.WriteLine("Enter new unit price (leave empty to keep current): ");
                var newUnitPriceInput = Console.ReadLine();

                if (!string.IsNullOrEmpty(newCategory))
                {
                    var existingCategory = categoryService.GetAll().
                        FirstOrDefault(c => ((CategoryModel)c).Name.Equals(newCategory, StringComparison.OrdinalIgnoreCase));

                    if (existingCategory == null)
                    {
                        var newCat = new CategoryModel(0, newCategory);
                        categoryService.Add(newCat);
                        existingCategory = (CategoryModel)categoryService.GetAll().Last();
                    }

                    category = existingCategory != null ? (CategoryModel)existingCategory : null;
                }

                if (!string.IsNullOrEmpty(newManufacturer))
                {
                    var existingManufacturer = manufacturerService.GetAll()
                        .FirstOrDefault(m => ((ManufacturerModel)m).Name.Equals(newManufacturer, StringComparison.OrdinalIgnoreCase));

                    if (existingManufacturer == null)
                    {
                        var newMan = new ManufacturerModel(0, newManufacturer);
                        manufacturerService.Add(newMan);
                        existingManufacturer = (ManufacturerModel)manufacturerService.GetAll().Last();
                    }

                    manufacturer = existingManufacturer != null ? (ManufacturerModel)existingManufacturer : null;
                }

                if (!string.IsNullOrEmpty(newName))
                {
                    var existingProductTitle = (ProductTitleModel)productTitleService.GetById(product.TitleId);

                    if (existingProductTitle != null && category != null)
                    {
                        existingProductTitle.Title = newName;
                        existingProductTitle.CategoryId = category.Id;
                        productTitleService.Update(existingProductTitle);
                    }
                }

                if (manufacturer != null)
                {
                    product.ManufacturerId = manufacturer.Id;
                }

                if (!string.IsNullOrEmpty(newDescription))
                {
                    product.Description = newDescription;
                }

                if (!string.IsNullOrEmpty(newUnitPriceInput) && decimal.TryParse(newUnitPriceInput, out var newUnitPrice))
                {
                    product.UnitPrice = newUnitPrice;
                }

                productService.Update(product);
                Console.WriteLine("Product updated successfully.");
            }
            else
            {
                Console.WriteLine("Invalid product ID.");
            }
        }

        /// <summary>
        /// Shows all products.
        /// </summary>
        public static void ShowAllProducts()
        {
            var productService = new ProductService(context);
            var productTitleService = new ProductTitleService(context);
            var products = productService.GetAll().OfType<ProductModel>();

            Console.WriteLine("Products:");
            foreach (var product in products)
            {
                var productTitle = (ProductTitleModel)productTitleService.GetById(product.TitleId);
                var title = productTitle != null ? productTitle.Title : "Unknown";
                Console.WriteLine($"Id: {product.Id} Title: {title} Description: {product.Description} UnitPrice: ${product.UnitPrice}");
            }
        }
    }
}