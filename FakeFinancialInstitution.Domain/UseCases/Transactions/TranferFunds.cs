using FakeFinancialInstitution.Domain.Contexts;
using FakeFinancialInstitution.Domain.Entities;
using FakeFinancialInstitution.Models;
using FakeFinancialInstitution.Models.Constants;
using FakeFinancialInstitution.Models.RequestModels;
using FakeFinancialInstitution.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeFinancialInstitution.Domain.UseCases.Transactions
{
	public sealed class TranferFunds : DomainUseCase<ServiceResponse<TransferResponseModel>>
	{

		public TransferRequestModel request;

		public TranferFunds(DomainContext domainContext)
		   : base(domainContext)
		{

		}

		protected override Tuple<bool, ServiceResponse<TransferResponseModel>> VerifyStateInternal()
		{
			var repo = Context.Repository;
			var fromAccount = repo.GetSingle<Account>(account => account.AccountNumber == request.FromAccountNumber);
			var toAccount = repo.GetSingle<Account>(account => account.AccountNumber == request.ToAccountNumber);

			if (fromAccount == null)
			{
				return Tuple.Create(false, new ServiceResponse<TransferResponseModel>
				{
					IsSuccess = false,
					Message = ErrorMessage.FromAccountNotFound
				});
			}

			if (toAccount == null)
			{
				return Tuple.Create(false, new ServiceResponse<TransferResponseModel>
				{
					IsSuccess = false,
					Message = ErrorMessage.ToAccountNotFound
				});
			}

			if (toAccount == fromAccount)
			{
				return Tuple.Create(false, new ServiceResponse<TransferResponseModel>
				{
					IsSuccess = false,
					Message = ErrorMessage.SameAccounttranfer
				});
			}

			if (fromAccount.Balance < request.Amount)
			{
				return Tuple.Create(false, new ServiceResponse<TransferResponseModel>
				{
					IsSuccess = false,
					Message = ErrorMessage.InsufficientFunds
				});
			}

			return Tuple.Create(true, new ServiceResponse<TransferResponseModel>());
		}

		protected override Task<ServiceResponse<TransferResponseModel>> ExecuteInternal()
		{
			var repo = Context.Repository;

			var fromAccount = repo.GetSingle<Account>(account => account.AccountNumber == request.FromAccountNumber);
			var toAccount = repo.GetSingle<Account>(account => account.AccountNumber == request.ToAccountNumber);
			var transactionType = repo.GetSingle<AccountTransactionType>(accountTransactionType => accountTransactionType.TransactionTypeName == Constants.Trasfer);

			fromAccount.Balance -= request.Amount;
			toAccount.Balance += request.Amount;
			repo.Update(fromAccount);
			repo.Update(toAccount);
			AddTransaction(repo, fromAccount, toAccount, transactionType);
			repo.Commit();
			
			return Task.FromResult(new ServiceResponse<TransferResponseModel>
			{
				Payload = CreateResponse(fromAccount, toAccount)
			});
		}

		private TransferResponseModel CreateResponse(Account fromAccount, Account toAccount)
		{
			return new TransferResponseModel
			{
				FromAccount = new BalanceResponseModel
				{
					Amount = request.Amount,
					Balance = fromAccount.Balance,
					AccountNumber = fromAccount.AccountNumber
				},
				ToAccount = new BalanceResponseModel
				{
					Amount = request.Amount,
					Balance = toAccount.Balance,
					AccountNumber = toAccount.AccountNumber
				},
			};
		}

		private void AddTransaction(Repositories.IEntityRepository repo, Account fromAccount, Account toAccount, AccountTransactionType transactionType)
		{
			var transaction = new AccountTransaction
			{
				TransactionAmount = request.Amount,
				FromAccountId = fromAccount.Id.Value,
				ToAccountId = toAccount.Id.Value,
				ToReference = request.ToReference,
				FromReference = request.FromReference,
				TransactionTypeId = transactionType.Id.Value,
				FromAccountBalance = fromAccount.Balance,
				ToAccountBalance = toAccount.Balance,
				FromAccountBeforeBalance = fromAccount.Balance + request.Amount,
				ToAccountBeforeBalance = toAccount.Balance - request.Amount
			};
			repo.Add(transaction);
		}

	}
}
