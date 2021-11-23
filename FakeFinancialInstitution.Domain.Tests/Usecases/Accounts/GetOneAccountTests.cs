using FakeFinancialInstitution.Domain.Entities;
using FakeFinancialInstitution.Models.Constants;
using FakeFinancialInstitution.Models.RequestModels;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FakeFinancialInstitution.Domain.Tests.Usecases.Accounts
{
	using static Testing;
	public class GetOneAccountTests
	{
		Random generator = new Random();
		[Test]
		public async Task GetOneAccount_AccountNumber_ShouldFailWithAccountDoesNotExit()
		{
			// Arrange
			var AccountNumber = Guid.NewGuid().ToString();

			// Act
			var result = await GetOneAccountWithUsecaseAsync(AccountNumber);

			//Assert
			result.Should().NotBeNull();
			result.AdditionalData.Should().BeNull();
			result.Message.Should().Be(ErrorMessage.AccountDoesNotExit);
			result.Payload.Should().BeNull();
			result.IsSuccess.Should().Be(false);
		}

		[Test]
		public async Task GetOneAccount_AccountNumber__ShouldPass()
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

			var account = CreateAccountWithRepo(new Account
			{
				AccountName = Guid.NewGuid().ToString(),
				AccountNumber = Guid.NewGuid().ToString(),
				AccountTypeId = accountType.Id.Value,
				CustomerId = customer.Id.Value,
				Balance = 1000.0M,
			});

			

			// Act
			var result = await GetOneAccountWithUsecaseAsync(account.AccountNumber);

			// Assert
			result.Should().NotBeNull();
			result.AdditionalData.Should().BeNull();
			result.Message.Should().BeNull();
			result.Payload.Should().NotBeNull();
			result.IsSuccess.Should().Be(true);
			result.Payload.Id.Should().NotBeNull();
			result.Payload.AccountName.Should().Be(account.AccountName);
			result.Payload.AccountTypeId.Should().Be(account.AccountTypeId);
			result.Payload.Balance.Should().Be(account.Balance);
		}
	}
}
