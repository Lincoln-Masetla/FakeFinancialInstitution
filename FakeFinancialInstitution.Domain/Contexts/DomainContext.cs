using FakeFinancialInstitution.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace FakeFinancialInstitution.Domain.Contexts
{
    public sealed class DomainContext
    {
        public DomainContext(
            IEntityRepository entityRepository,
            IEntityRepositoryFactory repositoryFactory)
        {
            RepositoryFactory = repositoryFactory;
            Repository = entityRepository;
        }

        internal DomainContext() { }

        /// <summary>
        /// Gets the underlying default entity repository factory instance.
        /// </summary>
        public IEntityRepositoryFactory RepositoryFactory { get; private set; }

        /// <summary>
        /// Gets the IEntityRepository instance.
        /// </summary>
        public IEntityRepository Repository { get; private set; }

        public DomainContext CreateAsyncCopy()
        {
            var newRepository = this.RepositoryFactory.CreateRepository();


            return new DomainContext
            {
                Repository = newRepository,
                RepositoryFactory = this.RepositoryFactory
            };


        }
    }
}
