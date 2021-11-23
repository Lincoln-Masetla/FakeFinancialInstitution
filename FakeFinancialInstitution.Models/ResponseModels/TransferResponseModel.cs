using System;
using System.Collections.Generic;
using System.Text;

namespace FakeFinancialInstitution.Models.ResponseModels
{
	public class TransferResponseModel
	{
		public BalanceResponseModel FromAccount { get; set; }
		public BalanceResponseModel ToAccount { get; set; }
	}
}
