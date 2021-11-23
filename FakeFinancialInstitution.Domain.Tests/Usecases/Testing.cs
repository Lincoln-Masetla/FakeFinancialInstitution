using FakeFinancialInstitution.API;
using FakeFinancialInstitution.Domain.Contexts;
using FakeFinancialInstitution.Domain.Entities;
using FakeFinancialInstitution.Domain.UseCases.Accounts;
using FakeFinancialInstitution.Domain.UseCases.Transactions;
using FakeFinancialInstitution.Models;
using FakeFinancialInstitution.Models.RequestModels;
using FakeFinancialInstitution.Models.ResponseModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using Respawn;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace FakeFinancialInstitution.Domain.Tests.Usecases
{
	[SetUpFixture]
	public class Testing
	{
		private static IConfiguration Configuration;
		private static IServiceScopeFactory ScopeFactory;
		private static Checkpoint RespawnCheckpoint;
        [OneTimeSetUp]
        public void RunBeforeAnyTests()
        {
            var builder = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("appsettings.json", true, true)
             .AddEnvironmentVariables();

            Configuration = builder.Build();

            var services = new ServiceCollection();

            var startup = new Startup(Configuration);

            services.AddSingleton(Mock.Of<IWebHostEnvironment>(w => w.ApplicationName == "FakeFinancialInstitution.API"));

            startup.ConfigureServices(services);

            ScopeFactory = services.BuildServiceProvider().GetService<IServiceScopeFactory>();

        }

		#region Mock data helpers
		public static AccountType CreateAccountTypeWithRepo(AccountType accountType)
        {
            using var scope = ScopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetService<DomainContext>();

            var repo = context.Repository;

            repo.Add(accountType);
            repo.Commit();
            return accountType;
        }

        public static CustomerType CreateCustomerTypeWithRepo(CustomerType customerType)
        {
            using var scope = ScopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetService<DomainContext>();

            var repo = context.Repository;

            repo.Add(customerType);
            repo.Commit();
            return customerType;
        }
        
        public static AccountTransactionType CreateAccountTransactionTypeWithRepo(AccountTransactionType accountTransactionType)
        {
            using var scope = ScopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetService<DomainContext>();

            var repo = context.Repository;

            repo.Add(accountTransactionType);
            repo.Commit();
            return accountTransactionType;
        }
        
        public static Account CreateAccountWithRepo(Account account)
        {
            using var scope = ScopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetService<DomainContext>();

            var repo = context.Repository;

            repo.Add(account);
            repo.Commit();
            return account;
        }
        
        public static AccountTransaction CreateAccountTransactionWithRepo(AccountTransaction accountTransaction)
        {
            using var scope = ScopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetService<DomainContext>();

            var repo = context.Repository;

            repo.Add(accountTransaction);
            repo.Commit();
            return accountTransaction;
        }

        public static Customer CreateCustomerWithRepo(Customer customer)
        {
            using var scope = ScopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetService<DomainContext>();

            var repo = context.Repository;

            repo.Add(customer);
            repo.Commit();
            return customer;
        }
        #endregion

        public static async Task<ServiceResponse<BankAccountForCustomerResponseModel>> CreateAccountWithUsecaseAsync(BankAccountForCustomerRequestModel request)
        {
            using var scope = ScopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetService<DomainContext>();

            var bankAccount = new CreateAccount(context)
            {
                request = request
            };

            var result = await bankAccount.ExecuteAsync();

            return result;
        }

        public static async Task<ServiceResponse<AccountResponseModel>> GetOneAccountWithUsecaseAsync(string request)
        {
            using var scope = ScopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetService<DomainContext>();

            var bankAccount = new GetOneAccount(context)
            {
                AccountNumber = request
            };

            var result = await bankAccount.ExecuteAsync();

            return result;
        }

        public static async Task<ServiceResponse<List<AccountTransactionResponseModel>>> GetTransactionsWithUsecaseAsync(string request)
        {
            using var scope = ScopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetService<DomainContext>();

            var bankAccount = new GetTransactions(context)
            {
                AccountNumber = request
            };

            var result = await bankAccount.ExecuteAsync();

            return result;
        }

        public static async Task<ServiceResponse<TransferResponseModel>> TranferWithUsecaseAsync(TransferRequestModel request)
        {
            using var scope = ScopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetService<DomainContext>();

            var bankAccount = new TranferFunds(context)
            {
                 request = request,
            };

            var result = await bankAccount.ExecuteAsync();

            return result;
        }
    }
}
