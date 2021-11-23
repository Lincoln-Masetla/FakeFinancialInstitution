using FakeFinancialInstitution.Domain.Contexts;
using FakeFinancialInstitution.Domain.Entities;
using FakeFinancialInstitution.Domain.Repositories;
using FakeFinancialInstitution.Models;
using FakeFinancialInstitution.Models.Constants;
using FakeFinancialInstitution.Models.RequestModels;
using FakeFinancialInstitution.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FakeFinancialInstitution.Domain.UseCases.Accounts
{
	public sealed class CreateAccount : DomainUseCase<ServiceResponse<BankAccountForCustomerResponseModel>>
	{

		public BankAccountForCustomerRequestModel request;

		public CreateAccount(DomainContext domainContext)
		   : base(domainContext)
		{

		}

		protected override Tuple<bool,ServiceResponse<BankAccountForCustomerResponseModel>> VerifyStateInternal()
		{
			var repo = Context.Repository;
			// Check AccountTypeId
			if (CheckAccountTypes(repo))
			{
				return Tuple.Create(false, new ServiceResponse<BankAccountForCustomerResponseModel>
				{
					IsSuccess = false,
					Message = ErrorMessage.InvalidAccountType
				});
			}

			//check CustomerId
			if (CheckCustomerType(repo))
			{
				return Tuple.Create(false, new ServiceResponse<BankAccountForCustomerResponseModel>
				{
					IsSuccess = false,
					Message = ErrorMessage.InvalidCustomerType
				});
			}

			//check CustomerPhone
			if (CheckCustomerPhoneNumber(repo))
			{
				return Tuple.Create(false, new ServiceResponse<BankAccountForCustomerResponseModel>
				{
					IsSuccess = false,
					Message = ErrorMessage.CustomerPhoneNumberExists
				});
			}

			//check CustomerEmail
			if (CheckCustomerEmail(repo))
			{
				return Tuple.Create(false, new ServiceResponse<BankAccountForCustomerResponseModel>
				{
					IsSuccess = false,
					Message = ErrorMessage.CustomerEmailExists
				});
			}

			//check CustomerIdNumber
			if (CheckCustomerIdNumber(repo))
			{
				return Tuple.Create(false, new ServiceResponse<BankAccountForCustomerResponseModel>
				{
					IsSuccess = false,
					Message = ErrorMessage.CustomerIDNumberExists
				});
			}

			return Tuple.Create(true, new ServiceResponse<BankAccountForCustomerResponseModel>());
		}

		private bool CheckAccountTypes(IEntityRepository repo)
		{
			var accountTypes = repo.All<AccountType>();
			var accountTypeDoesNotExists = false;
			foreach(var account in request.Accounts)
			{
				var accountType = accountTypes.FirstOrDefault(x => x.Id == account.AccountTypeId);
				if (accountType == null)
				{
					accountTypeDoesNotExists = true;
					break;
				}
			}
			return accountTypeDoesNotExists;
		}

		private bool CheckCustomerType(IEntityRepository repo)
		{
			var customerTypes = repo.GetSingle<CustomerType>(customerType => customerType.Id == request.Customer.CustomerTypeId);
			return customerTypes == null;
		}

		private bool CheckCustomerPhoneNumber(IEntityRepository repo)
		{
			var exists = repo.GetSingle<Customer>(x => x.CustomerPhone == request.Customer.CustomerPhone);
			return exists != null;
		}
		
		private bool CheckCustomerIdNumber(IEntityRepository repo)
		{
			var exists = repo.GetSingle<Customer>(x => x.CustomerIdNumber == request.Customer.CustomerIdNumber);
			return exists != null;
		}

		private bool CheckCustomerEmail(IEntityRepository repo)
		{
			var exists = repo.GetSingle<Customer>(x => x.CustomerEmail == request.Customer.CustomerEmail);
			return exists != null;
		}

		protected override Task<ServiceResponse<BankAccountForCustomerResponseModel>> ExecuteInternal()
		{
			var repo = Context.Repository;
			Customer customer = CreateCustomer();
			IEnumerable<Account> accounts = CreateAccounts(customer);
			repo.Add(customer);
			repo.Commit();
			repo.AddRange(accounts);
			repo.Commit();
			return Task.FromResult(new ServiceResponse<BankAccountForCustomerResponseModel>
			{
				Payload = CreateResponse(customer,accounts)
			});
		}

		private IEnumerable<Account> CreateAccounts(Customer customer)
		{
			var accounts = new List<Account>();
			var accountsReq = request.Accounts;
			foreach (var account in accountsReq) {
				var item = new Account
				{
					AccountName = account.AccountName,
					AccountTypeId = account.AccountTypeId,
					AccountNumber = createAccountNumber(),
					Balance = account.Balance,
					CustomerId = customer.Id.Value
				};
				accounts.Add(item);
			}
			return accounts;
		}

		private Customer CreateCustomer()
		{
			var customerReq = request.Customer;
			var customer = new Customer
			{
				CustomerEmail = customerReq.CustomerEmail,
				CustomerIdNumber = customerReq.CustomerIdNumber,
				CustomerName = customerReq.CustomerName,
				CustomerPhone = customerReq.CustomerPhone,
				CustomerTypeId = customerReq.CustomerTypeId,
			};
			return customer;
		}

		private static BankAccountForCustomerResponseModel CreateResponse(Customer customer, IEnumerable<Account> accounts)
		{
			return new BankAccountForCustomerResponseModel
			{
				Accounts = (accounts.Select(account => new AccountResponseModel
				{
					Id = account.Id,
					CreatedDate = account.CreatedDate,
					UpdatedDate = account.UpdatedDate,
					IsActive = account.IsActive,
					AccountName = account.AccountName,
					AccountTypeId = account.AccountTypeId,
					AccountNumber = account.AccountNumber,
					Balance = account.Balance,
					CustomerId = account.CustomerId
				})).ToList(),
				Customer = new CustomerResponseModel
				{
					Id = customer.Id,
					CreatedDate = customer.CreatedDate,
					UpdatedDate = customer.UpdatedDate,
					IsActive = customer.IsActive,
					CustomerEmail = customer.CustomerEmail,
					CustomerIdNumber = customer.CustomerIdNumber,
					CustomerName = customer.CustomerName,
					CustomerPhone = customer.CustomerPhone,
					CustomerTypeId = customer.CustomerTypeId
				}
			};
		}

		private string createAccountNumber()
		{
			var repo = Context.Repository;
			string startWith = "32";
			Random generator = new Random();
			string partialAccountNumber = generator.Next(0, 999999).ToString("D6");
			string accounNumber = startWith + partialAccountNumber;
			var account = repo.GetSingle<Account>(i => i.AccountNumber == accounNumber);
			return accounNumber;
		}
	}
}
