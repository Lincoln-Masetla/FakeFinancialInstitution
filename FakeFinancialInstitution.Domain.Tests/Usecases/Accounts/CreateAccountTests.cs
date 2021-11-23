using FakeFinancialInstitution.Domain.Entities;
using FakeFinancialInstitution.Models.Constants;
using FakeFinancialInstitution.Models.RequestModels;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

//NB did now worry about managing state. Used unique values and linq queries to get the correct testing values
//Naming is as follows WhatMethodToTest_Parameters_WhatYouAreTesting
namespace FakeFinancialInstitution.Domain.Tests.Usecases.Accounts
{
	using static Testing;
	public class CreateAccountTests
	{
		Random generator = new Random();
		[Test]
		public async Task CreateAccount_BankAccountForCustomerRequestModel_ShouldFailWithInvalidAccountType()
		{
			// Arrange
			var customerType = new CustomerType
			{
				CustomerTypeName = Guid.NewGuid().ToString(),
				CustomerTypeDescription = Guid.NewGuid().ToString()
			};
			CreateCustomerTypeWithRepo(customerType);

			var request = new BankAccountForCustomerRequestModel
			{
				Accounts = new List<AccountRequestModel>
				{
					new AccountRequestModel
					{
						AccountName = Guid.NewGuid().ToString(),
						AccountTypeId = Guid.NewGuid(),
						Balance = 0.00M
					}
				},
				Customer = new CustomerRequestModel
				{
					CustomerEmail = $"{ Guid.NewGuid().ToString()}@test.com",
					CustomerIdNumber = generator.Next(0, 999999).ToString() + generator.Next(0, 9999).ToString() + generator.Next(0, 999).ToString(),
					CustomerName = Guid.NewGuid().ToString(),
					CustomerPhone = generator.Next(0, 999999).ToString() + generator.Next(0, 9999).ToString(),
					CustomerTypeId = customerType.Id.Value,
				}
			};

			// Act
			var result = await CreateAccountWithUsecaseAsync(request);

			// Assert
			result.Should().NotBeNull();
			result.AdditionalData.Should().BeNull();
			result.Message.Should().Be(ErrorMessage.InvalidAccountType);
			result.Payload.Should().BeNull();
			result.IsSuccess.Should().Be(false);

		}

		[Test]
		public async Task CreateAccount_BankAccountForCustomerRequestModel_ShouldFailWithInvalidCustomerType()
		{
			// Arrange
			var accountType = new AccountType
			{
				AccountTypeName = Guid.NewGuid().ToString(),
				AccountTypeDescription = Guid.NewGuid().ToString()
			};
			CreateAccountTypeWithRepo(accountType);

			var request = new BankAccountForCustomerRequestModel
			{
				Accounts = new List<AccountRequestModel>
				{
					new AccountRequestModel
					{
						AccountName = Guid.NewGuid().ToString(),
						AccountTypeId = accountType.Id.Value,
						Balance = 0.00M
					}
				},
				Customer = new CustomerRequestModel
				{
					CustomerEmail = $"{ Guid.NewGuid().ToString()}@test.com",
					CustomerIdNumber = generator.Next(0, 999999).ToString() + generator.Next(0, 9999).ToString() + generator.Next(0, 999).ToString(),
					CustomerName = Guid.NewGuid().ToString(),
					CustomerPhone = generator.Next(0, 999999).ToString() + generator.Next(0, 9999).ToString(),
					CustomerTypeId = Guid.NewGuid(),
				}
			};

			// Act
			var result = await CreateAccountWithUsecaseAsync(request);

			// Assert
			var code = ErrorMessage.InvalidCustomerType;
			result.Should().NotBeNull();
			result.AdditionalData.Should().BeNull();
			result.Message.Should().Be(code);
			result.Payload.Should().BeNull();
			result.IsSuccess.Should().Be(false);

		}

		[Test]
		public async Task CreateAccount_BankAccountForCustomerRequestModel_ShouldFailCustomerPhoneNumberExists()
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
				CustomerTypeId = customerType.Id.Value,

			};

			CreateCustomerWithRepo(customer);

			var request = new BankAccountForCustomerRequestModel
			{
				Accounts = new List<AccountRequestModel>
				{
					new AccountRequestModel
					{
						AccountName = Guid.NewGuid().ToString(),
						AccountTypeId = accountType.Id.Value,
						Balance = 0.00M
					}
				},
				Customer = new CustomerRequestModel
				{
					CustomerEmail = $"{ Guid.NewGuid().ToString()}@test.com",
					CustomerIdNumber = generator.Next(0, 999999).ToString() + generator.Next(0, 9999).ToString() + generator.Next(0, 999).ToString(),
					CustomerName = Guid.NewGuid().ToString(),
					CustomerPhone = customer.CustomerPhone,
					CustomerTypeId = customerType.Id.Value,
				}
			};

			// Act
			var result = await CreateAccountWithUsecaseAsync(request);

			// Assert
			result.Should().NotBeNull();
			result.AdditionalData.Should().BeNull();
			result.Message.Should().Be(ErrorMessage.CustomerPhoneNumberExists);
			result.Payload.Should().BeNull();
			result.IsSuccess.Should().Be(false);

		}

		[Test]
		public async Task CreateAccount_BankAccountForCustomerRequestModel_ShouldFailCustomerEmailExists()
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
				CustomerTypeId = customerType.Id.Value,

			};

			CreateCustomerWithRepo(customer);

			var request = new BankAccountForCustomerRequestModel
			{
				Accounts = new List<AccountRequestModel>
				{
					new AccountRequestModel
					{
						AccountName = Guid.NewGuid().ToString(),
						AccountTypeId = accountType.Id.Value,
						Balance = 0.00M
					}
				},
				Customer = new CustomerRequestModel
				{
					CustomerEmail = customer.CustomerEmail,
					CustomerIdNumber = generator.Next(0, 999999).ToString() + generator.Next(0, 9999).ToString() + generator.Next(0, 999).ToString(),
					CustomerName = Guid.NewGuid().ToString(),
					CustomerPhone = generator.Next(0, 999999).ToString() + generator.Next(0, 9999).ToString(),
					CustomerTypeId = customerType.Id.Value,
				}
			};

			// Act
			var result = await CreateAccountWithUsecaseAsync(request);

			// Assert
			result.Should().NotBeNull();
			result.AdditionalData.Should().BeNull();
			result.Message.Should().Be(ErrorMessage.CustomerEmailExists);
			result.Payload.Should().BeNull();
			result.IsSuccess.Should().Be(false);

		}

		[Test]
		public async Task CreateAccount_BankAccountForCustomerRequestModel_ShouldFailCustomerIdNumberExists()
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
				CustomerTypeId = customerType.Id.Value,

			};

			CreateCustomerWithRepo(customer);

			var request = new BankAccountForCustomerRequestModel
			{
				Accounts = new List<AccountRequestModel>
				{
					new AccountRequestModel
					{
						AccountName = Guid.NewGuid().ToString(),
						AccountTypeId = accountType.Id.Value,
						Balance = 0.00M
					}
				},
				Customer = new CustomerRequestModel
				{
					CustomerEmail = $"{ Guid.NewGuid().ToString()}@test.com",
					CustomerIdNumber = customer.CustomerIdNumber,
					CustomerName = Guid.NewGuid().ToString(),
					CustomerPhone = generator.Next(0, 999999).ToString() + generator.Next(0, 9999).ToString(),
					CustomerTypeId = customerType.Id.Value,
				}
			};

			// Act
			var result = await CreateAccountWithUsecaseAsync(request);

			// Assert
			result.Should().NotBeNull();
			result.AdditionalData.Should().BeNull();
			result.Message.Should().Be(ErrorMessage.CustomerIDNumberExists);
			result.Payload.Should().BeNull();
			result.IsSuccess.Should().Be(false);

		}

		[Test]
		public async Task CreateAccount_BankAccountForCustomerRequestModel_ShouldPass()
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

			var request = new BankAccountForCustomerRequestModel
			{
				Accounts = new List<AccountRequestModel>
				{
					new AccountRequestModel
					{
						AccountName = Guid.NewGuid().ToString(),
						AccountTypeId = accountType.Id.Value,
						Balance = 0.00M
					}
				},
				Customer = new CustomerRequestModel
				{
					CustomerEmail = $"{ Guid.NewGuid().ToString()}@test.com",
					CustomerIdNumber = generator.Next(0, 999999).ToString() + generator.Next(0, 9999).ToString() + generator.Next(0, 999).ToString(),
					CustomerName = Guid.NewGuid().ToString(),
					CustomerPhone = generator.Next(0, 999999).ToString() + generator.Next(0, 9999).ToString(),
					CustomerTypeId = customerType.Id.Value,
				}
			};

			// Act
			var result = await CreateAccountWithUsecaseAsync(request);

			// Assert
			result.Should().NotBeNull();
			result.AdditionalData.Should().BeNull();
			result.Message.Should().BeNull();
			result.Payload.Should().NotBeNull();
			result.IsSuccess.Should().Be(true);
			result.Payload.Accounts.Count.Should().Be(1);
			result.Payload.Accounts[0].Id.Should().NotBeNull();
			result.Payload.Accounts[0].AccountName.Should().Be(request.Accounts[0].AccountName);
			result.Payload.Accounts[0].AccountTypeId.Should().Be(request.Accounts[0].AccountTypeId);
			result.Payload.Accounts[0].Balance.Should().Be(request.Accounts[0].Balance);
			result.Payload.Customer.CustomerEmail.Should().Be(request.Customer.CustomerEmail);
			result.Payload.Customer.CustomerIdNumber.Should().Be(request.Customer.CustomerIdNumber);
			result.Payload.Customer.CustomerName.Should().Be(request.Customer.CustomerName);
			result.Payload.Customer.CustomerPhone.Should().Be(request.Customer.CustomerPhone);
			result.Payload.Customer.CustomerTypeId.Should().Be(request.Customer.CustomerTypeId);
		}
	}
}
