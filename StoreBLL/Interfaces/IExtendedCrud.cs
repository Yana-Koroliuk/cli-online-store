namespace StoreBLL.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using StoreBLL.Models;

    /// <summary>
    /// Defines a set of CRUD operations for managing entities with additional methods for GetByName and Create.
    /// </summary>
    public interface IExtendedCrud : ICrud
    {
        /// <summary>
        /// Gets an entity by its name.
        /// </summary>
        /// <param name="name">The name of the entity to retrieve.</param>
        /// <returns>The entity model if found, otherwise null.</returns>
        AbstractModel? GetByName(string name);

        /// <summary>
        /// Creates a new entity.
        /// </summary>
        /// <param name="name">The name of the entity to create.</param>
        /// <returns>The created entity model.</returns>
        AbstractModel Create(string name);
    }
}