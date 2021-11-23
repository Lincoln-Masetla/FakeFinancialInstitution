using FakeFinancialInstitution.Domain.Entities;
using FakeFinancialInstitution.Models.Constants;
using FakeFinancialInstitution.Models.RequestModels;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FakeFinancialInstitution.Domain.Tests.Usecases.Transactions
{
    using static Testing;
	public class TranferFundsTests
	{
		Random generator = new Random();
		[Test]
		public async Task TransferFunds_TransferRequestModel_ShouldFailWithFromAccountNotFound()
		{
			// Arrange
			var tranfer = new TransferRequestModel
			{
				FromAccountNumber = Guid.NewGuid().ToString(),
				Amount = 100.00M,
				FromReference = Guid.NewGuid().ToString(),
				ToAccountNumber = Guid.NewGuid().ToString(),
				ToReference = Guid.NewGuid().ToString()
			};
			// Act
			var result = await TranferWithUsecaseAsync(tranfer);

			//Assert
			result.Should().NotBeNull();
			result.AdditionalData.Should().BeNull();
			result.Message.Should().Be(ErrorMessage.FromAccountNotFound);
			result.Payload.Should().BeNull();
			result.IsSuccess.Should().Be(false);
		}


		[Test]
		public async Task TransferFunds_TransferRequestModel_ShouldFailWithToAccountNotFound()
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

			var tranfer = new TransferRequestModel
			{
				FromAccountNumber = fromAccount.AccountNumber,
				Amount = 100.00M,
				FromReference = Guid.NewGuid().ToString(),
				ToAccountNumber = Guid.NewGuid().ToString(),
				ToReference = Guid.NewGuid().ToString()
			};
			// Act
			var result = await TranferWithUsecaseAsync(tranfer);

			//Assert
			result.Should().NotBeNull();
			result.AdditionalData.Should().BeNull();
			result.Message.Should().Be(ErrorMessage.ToAccountNotFound);
			result.Payload.Should().BeNull();
			result.IsSuccess.Should().Be(false);
		}


		[Test]
		public async Task TransferFunds_TransferRequestModel_ShouldFailWithSameAccounttranfer()
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

			var tranfer = new TransferRequestModel
			{
				FromAccountNumber = fromAccount.AccountNumber,
				Amount = 100.00M,
				FromReference = Guid.NewGuid().ToString(),
				ToAccountNumber = fromAccount.AccountNumber,
				ToReference = Guid.NewGuid().ToString()
			};
			// Act
			var result = await TranferWithUsecaseAsync(tranfer);

			//Assert
			result.Should().NotBeNull();
			result.AdditionalData.Should().BeNull();
			result.Message.Should().Be(ErrorMessage.SameAccounttranfer);
			result.Payload.Should().BeNull();
			result.IsSuccess.Should().Be(false);
		}


		[Test]
		public async Task TransferFunds_TransferRequestModel_ShouldFailWithInsufficientFunds()
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

			var tranfer = new TransferRequestModel
			{
				FromAccountNumber = fromAccount.AccountNumber,
				Amount = 1500.00M,
				FromReference = Guid.NewGuid().ToString(),
				ToAccountNumber = toAccount.AccountNumber,
				ToReference = Guid.NewGuid().ToString()
			};
			// Act
			var result = await TranferWithUsecaseAsync(tranfer);

			//Assert
			result.Should().NotBeNull();
			result.AdditionalData.Should().BeNull();
			result.Message.Should().Be(ErrorMessage.InsufficientFunds);
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

			var tranfer = new TransferRequestModel
			{
				FromAccountNumber = fromAccount.AccountNumber,
				Amount = 500.00M,
				FromReference = Guid.NewGuid().ToString(),
				ToAccountNumber = toAccount.AccountNumber,
				ToReference = Guid.NewGuid().ToString()
			};
			// Act
			var result = await TranferWithUsecaseAsync(tranfer);

			//Assert
			result.Should().NotBeNull();
			result.AdditionalData.Should().BeNull();
			result.Message.Should().BeNull();
			result.Payload.Should().NotBeNull();
			result.IsSuccess.Should().Be(true);
			result.Payload.FromAccount.AccountNumber.Should().Be(fromAccount.AccountNumber);
			result.Payload.FromAccount.Amount.Should().Be(tranfer.Amount);
			result.Payload.FromAccount.Balance.Should().Be(fromAccount.Balance - tranfer.Amount);
			result.Payload.ToAccount.AccountNumber.Should().Be(toAccount.AccountNumber);
			result.Payload.ToAccount.Amount.Should().Be(tranfer.Amount);
			result.Payload.ToAccount.Balance.Should().Be(toAccount.Balance + tranfer.Amount);
		}
	}
}
