namespace StoreBLL.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines a set of operations for managing order states.
    /// </summary>
    public interface IOrderStateService : ICrud
    {
        /// <summary>
        /// Retrieves the allowed to change status IDs for a given current status ID.
        /// </summary>
        /// <param name="currentStatusId">The current status ID.</param>
        /// <returns>A list of allowed status IDs.</returns>
        IEnumerable<int> GetChangeToStatusIds(int currentStatusId);
    }
}
