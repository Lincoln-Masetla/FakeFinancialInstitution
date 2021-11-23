using FakeFinancialInstitution.Domain.EF.Contexts;
using FakeFinancialInstitution.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FakeFinancialInstitution.Domain.EF.Repositories
{
    public sealed class DbContextEntityRepositoryFactory : IEntityRepositoryFactory
    {
        private readonly IServiceProvider _provider;

        public DbContextEntityRepositoryFactory(IServiceProvider provider)
        {
            _provider = provider ?? throw new ArgumentNullException(nameof(provider));
        }


        public IEntityRepository CreateRepository()
        {
            var options = _provider.GetService(typeof(DbContextOptions)) as DbContextOptions;

            return new DbContextRepository(new FakeFinancialInstitutionDbContext(options));
        }
    }
}
