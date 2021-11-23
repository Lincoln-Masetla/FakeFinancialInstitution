using FakeFinancialInstitution.Domain.EF.Contexts;
using FakeFinancialInstitution.Domain.EF.Repositories;
using FakeFinancialInstitution.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace FakeFinancialInstitution.Domain.EF
{
    public static class Setup
    {
        public static void AddDatabaseDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<FakeFinancialInstitutionDbContext>(options =>
            {
                var connectionString = configuration.GetConnectionString("FakeFinancialInstitutionDbContext");


                options.UseSqlServer(connectionString, (sql) =>
                {
                    sql.EnableRetryOnFailure();
                });
            });

            services.AddScoped<IEntityRepositoryFactory, DbContextEntityRepositoryFactory>();
            services.AddScoped<FakeFinancialInstitutionDbContext, FakeFinancialInstitutionDbContext>();
            services.AddScoped<IEntityRepository, DbContextRepository>();
        }
    }
}
