using System;
using System.Collections.Generic;
using System.Text;

namespace FakeFinancialInstitution.Models.ResponseModels
{
	public class BankAccountForCustomerResponseModel
	{
		public List<AccountResponseModel> Accounts { get; set; }
		public CustomerResponseModel Customer { get; set; }
	}
}
