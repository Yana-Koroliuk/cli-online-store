namespace StoreBLL.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using StoreBLL.Models;

    /// <summary>
    /// Defines a set of additional operations for managing product titles.
    /// </summary>
    public interface IProductTitleService : ICrud
    {
        /// <summary>
        /// Gets a product title by name and category ID.
        /// </summary>
        /// <param name="name">The name of the product title.</param>
        /// <param name="categoryId">The ID of the category.</param>
        /// <returns>The product title model if found, otherwise null.</returns>
        ProductTitleModel? GetByNameAndCategoryId(string name, int categoryId);

        /// <summary>
        /// Creates a new entity.
        /// </summary>
        /// <param name="name">The name of the product title.</param>
        /// <param name="categoryId">The ID of the category.</param>
        /// <returns>The created entity model.</returns>
        AbstractModel Create(string name, int categoryId);
    }
}