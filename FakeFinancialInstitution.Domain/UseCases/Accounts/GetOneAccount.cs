using FakeFinancialInstitution.Domain.Contexts;
using FakeFinancialInstitution.Domain.Entities;
using FakeFinancialInstitution.Models;
using FakeFinancialInstitution.Models.Constants;
using FakeFinancialInstitution.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FakeFinancialInstitution.Domain.UseCases.Accounts
{
	//NB this methid can also be used to get balance as its part of the response model
	public sealed class GetOneAccount : DomainUseCase<ServiceResponse<AccountResponseModel>>
	{

		public string AccountNumber { get; set; }

		public GetOneAccount(DomainContext domainContext)
		   : base(domainContext)
		{
		}

		protected override Tuple<bool, ServiceResponse<AccountResponseModel>> VerifyStateInternal()
		{
			var repo = Context.Repository;

			var account = repo.GetSingle<Account>(i => i.AccountNumber == AccountNumber);
			if(account == null)
			{
				return Tuple.Create(false, new ServiceResponse<AccountResponseModel>
				{
					IsSuccess = false,
					Message = ErrorMessage.AccountDoesNotExit
				});
			}

			return Tuple.Create(true, new ServiceResponse<AccountResponseModel>());
		}

		protected override Task<ServiceResponse<AccountResponseModel>> ExecuteInternal()
		{
			var repo = Context.Repository;

			var account = repo.GetSingle<Account>(i => i.AccountNumber == AccountNumber);

			return Task.FromResult(new ServiceResponse<AccountResponseModel>
			{
				Payload = createResponseModel(account)
			});
		}

		private static AccountResponseModel createResponseModel(Account account)
		{
			return new AccountResponseModel
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
			};
		}
	}
}
