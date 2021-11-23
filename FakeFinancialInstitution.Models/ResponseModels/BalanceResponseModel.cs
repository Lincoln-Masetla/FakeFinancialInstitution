using System;
using System.Collections.Generic;
using System.Text;

namespace FakeFinancialInstitution.Models.ResponseModels
{
	public class BalanceResponseModel
	{
		public string AccountNumber { get; set; }
		public decimal Amount { get; set; }
		public decimal Balance { get; set; }
	}
}
