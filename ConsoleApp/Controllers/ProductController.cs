using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1;
using StoreBLL.Models;
using StoreBLL.Services;
using StoreDAL.Data;

namespace ConsoleApp.Controllers
{
    /// <summary>
    /// Provides methods for managing products.
    /// </summary>
    public static class ProductController
    {
        private static StoreDbContext context = UserMenuController.Context;

        public static void AddProduct()
        {
            throw new NotImplementedException();
        }

        public static void UpdateProduct()
        {
            throw new NotImplementedException();
        }

        public static void DeleteProduct()
        {
            throw new NotImplementedException();
        }

        public static void ShowProduct()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Shows all products.
        /// </summary>
        public static void ShowAllProducts()
        {
            var productService = new ProductService(context);
            var products = productService.GetAll().OfType<ProductModel>();

            Console.WriteLine("Products:");
            foreach (var product in products)
            {
                Console.WriteLine(product);
            }
        }

        public static void AddCategory()
        {
            throw new NotImplementedException();
        }

        public static void UpdateCategory()
        {
            throw new NotImplementedException();
        }

        public static void DeleteCategory()
        {
            throw new NotImplementedException();
        }

        public static void ShowAllCategories()
        {
            throw new NotImplementedException();
        }

        public static void AddProductTitle()
        {
            throw new NotImplementedException();
        }

        public static void UpdateProductTitle()
        {
            throw new NotImplementedException();
        }

        public static void DeleteProductTitle()
        {
            throw new NotImplementedException();
        }

        public static void ShowAllProductTitles()
        {
            throw new NotImplementedException();
        }

        public static void AddManufacturer()
        {
            throw new NotImplementedException();
        }

        public static void UpdateManufacturer()
        {
            throw new NotImplementedException();
        }

        public static void DeleteManufacturer()
        {
            throw new NotImplementedException();
        }

        public static void ShowAllManufacturers()
        {
            throw new NotImplementedException();
        }
    }
}