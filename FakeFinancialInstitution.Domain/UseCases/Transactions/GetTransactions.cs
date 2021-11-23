using FakeFinancialInstitution.Domain.Contexts;
using FakeFinancialInstitution.Domain.Entities;
using FakeFinancialInstitution.Models;
using FakeFinancialInstitution.Models.Constants;
using FakeFinancialInstitution.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeFinancialInstitution.Domain.UseCases.Transactions
{

	public sealed class GetTransactions : DomainUseCase<ServiceResponse<List<AccountTransactionResponseModel>>>
	{

		public string AccountNumber { get; set; }

		public GetTransactions(DomainContext domainContext)
		   : base(domainContext)
		{
		}

		protected override Tuple<bool, ServiceResponse<List<AccountTransactionResponseModel>>> VerifyStateInternal()
		{
			var repo = Context.Repository;

			var account = repo.GetSingle<Account>(i => i.AccountNumber == AccountNumber);
			if (account == null)
			{
				return Tuple.Create(false, new ServiceResponse<List<AccountTransactionResponseModel>>
				{
					IsSuccess = false,
					Message = ErrorMessage.AccountDoesNotExit
				});
			}
			return Tuple.Create(true, new ServiceResponse<List<AccountTransactionResponseModel>>());
		}

		protected override Task<ServiceResponse<List<AccountTransactionResponseModel>>> ExecuteInternal()
		{
			var repo = Context.Repository;
			var account = repo.GetSingle<Account>(i => i.AccountNumber == AccountNumber);
			var accountTransactions = repo.All<AccountTransaction>().Where(i => i.FromAccountId == account.Id || i.ToAccountId == account.Id).ToList();

			return Task.FromResult(new ServiceResponse<List<AccountTransactionResponseModel>>
			{
				Payload = createResponseModel(accountTransactions)
			});
		}

		private static List<AccountTransactionResponseModel> createResponseModel(List<AccountTransaction> transactions)
		{
			return transactions.Select(transaction => new AccountTransactionResponseModel
			{
				Id = transaction.Id.Value,
				CreatedDate = transaction.CreatedDate,
				ToAccountId = transaction.ToAccountId,
				FromAccountId = transaction.FromAccountId,
				FromAccountBalance = transaction.FromAccountBalance,
				FromAccountBeforeBalance = transaction.FromAccountBeforeBalance,
				FromReference = transaction.FromReference,
				ToAccountBalance = transaction.ToAccountBalance,
				ToAccountBeforeBalance = transaction.ToAccountBeforeBalance,
				ToReference = transaction.ToReference,
				TransactionAmount = transaction.TransactionAmount,
				TransactionTypeId = transaction.TransactionTypeId
			}).ToList();
		}
	}
}
