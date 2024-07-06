using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreDAL.Data;
using StoreDAL.Entities;

namespace StoreDAL.Repository
{
    /// <summary>
    /// Provides a base class for repository implementations with disposable pattern.
    /// </summary>
    public abstract class AbstractRepository : IDisposable
    {
        /// <summary>
        /// The database context used by the repository.
        /// </summary>
        private readonly StoreDbContext context;
        private bool disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractRepository"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        protected AbstractRepository(StoreDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="AbstractRepository"/> class.
        /// </summary>
        ~AbstractRepository()
        {
            this.Dispose(false);
        }

        protected StoreDbContext Context
        {
            get => this.context;
        }

        /// <summary>
        /// Disposes the resources used by the repository.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes the resources used by the repository.
        /// </summary>
        /// <param name="disposing">If set to <c>true</c>, managed resources are disposed; otherwise, only unmanaged resources are disposed.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this.context.Dispose();
                }

                this.disposed = true;
            }
        }
    }
}
