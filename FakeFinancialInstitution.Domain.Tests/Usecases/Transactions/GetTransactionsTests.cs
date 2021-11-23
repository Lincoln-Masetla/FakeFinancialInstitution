using FakeFinancialInstitution.Domain.Entities;
using FakeFinancialInstitution.Models.Constants;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FakeFinancialInstitution.Domain.Tests.Usecases.Transactions
{
	using static Testing;
	public class GetTransactionsTests
	{
		Random generator = new Random();
		[Test]
		public async Task GetTransactions_AccountNumber_ShouldFailWithAccountDoesNotExit()
		{
			// Arrange
			var AccountNumber = Guid.NewGuid().ToString();

			// Act
			var result = await GetTransactionsWithUsecaseAsync(AccountNumber);

			//Assert
			result.Should().NotBeNull();
			result.AdditionalData.Should().BeNull();
			result.Message.Should().Be(ErrorMessage.AccountDoesNotExit);
			result.Payload.Should().BeNull();
			result.IsSuccess.Should().Be(false);
		}

		[Test]
		public async Task GetTransactions_AccountNumber__ShouldPass()
		{
			// Arrange

			var accountType = new AccountType
			{
				AccountTypeName = Guid.NewGuid().ToString(),
				AccountTypeDescription = Guid.NewGuid().ToString()
			};
			CreateAccountTypeWithRepo(accountType);

			var customerType = new CustomerType
			{
				CustomerTypeName = Guid.NewGuid().ToString(),
				CustomerTypeDescription = Guid.NewGuid().ToString()
			};
			CreateCustomerTypeWithRepo(customerType);

			var customer = new Customer
			{
				CustomerEmail = $"{ Guid.NewGuid().ToString()}@test.com",
				CustomerIdNumber = generator.Next(0, 999999).ToString() + generator.Next(0, 9999).ToString() + generator.Next(0, 999).ToString(),
				CustomerName = Guid.NewGuid().ToString(),
				CustomerPhone = generator.Next(0, 999999).ToString() + generator.Next(0, 9999).ToString(),
				CustomerTypeId = customerType.Id.Value

			};

			CreateCustomerWithRepo(customer);

			var fromAccount = CreateAccountWithRepo(new Account
			{
				AccountName = Guid.NewGuid().ToString(),
				AccountNumber = Guid.NewGuid().ToString(),
				AccountTypeId = accountType.Id.Value,
				CustomerId = customer.Id.Value,
				Balance = 1000.0M,
			});

			var toAccount = CreateAccountWithRepo(new Account
			{
				AccountName = Guid.NewGuid().ToString(),
				AccountNumber = Guid.NewGuid().ToString(),
				AccountTypeId = accountType.Id.Value,
				CustomerId = customer.Id.Value,
				Balance = 1000.0M,
			});

			var transactionType = CreateAccountTransactionTypeWithRepo(new AccountTransactionType
			{
				TransactionTypeName = Guid.NewGuid().ToString(),
			});

			var accountTransaction = CreateAccountTransactionWithRepo( new AccountTransaction
			{
				FromAccountBalance = fromAccount.Balance,
				FromAccountBeforeBalance = fromAccount.Balance - 300,
				FromAccountId = fromAccount.Id.Value,
				ToAccountBalance = toAccount.Balance + 300,
				ToAccountBeforeBalance = toAccount.Balance,
				FromReference = Guid.NewGuid().ToString(),
				ToAccountId = toAccount.Id.Value,
				ToReference = Guid.NewGuid().ToString(),
				TransactionAmount = 300.00M,
				TransactionTypeId = transactionType.Id.Value
			});



			// Act
			var result = await GetTransactionsWithUsecaseAsync(toAccount.AccountNumber);

			// Assert
			result.Should().NotBeNull();
			result.AdditionalData.Should().BeNull();
			result.Message.Should().BeNull();
			result.Payload.Should().NotBeNull();
			result.IsSuccess.Should().Be(true);
			result.Payload.Count.Should().BeGreaterThan(0);
			result.Payload[0].Id.Should().Be(accountTransaction.Id.Value);
			result.Payload[0].FromAccountBalance.Should().Be(accountTransaction.FromAccountBalance);
			result.Payload[0].FromAccountBeforeBalance.Should().Be(accountTransaction.FromAccountBeforeBalance);
			result.Payload[0].FromAccountId.Should().Be(accountTransaction.FromAccountId);
			result.Payload[0].ToAccountBalance.Should().Be(accountTransaction.ToAccountBalance);
			result.Payload[0].ToAccountBeforeBalance.Should().Be(accountTransaction.ToAccountBeforeBalance);
			result.Payload[0].FromReference.Should().Be(accountTransaction.FromReference);
			result.Payload[0].ToAccountId.Should().Be(accountTransaction.ToAccountId);
			result.Payload[0].ToReference.Should().Be(accountTransaction.ToReference);
			result.Payload[0].TransactionAmount.Should().Be(accountTransaction.TransactionAmount);
			result.Payload[0].TransactionTypeId.Should().Be(accountTransaction.TransactionTypeId);
		}
	}
}
