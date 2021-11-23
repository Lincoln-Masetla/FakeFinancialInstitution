using System;
using System.Collections.Generic;
using System.Text;

namespace FakeFinancialInstitution.Models.ResponseModels
{
	public class AccountTransactionResponseModel
	{
		public Guid Id { get; set; }
		public Guid TransactionTypeId { get; set; }
		public Decimal TransactionAmount { get; set; }
		public Guid FromAccountId { get; set; }
		public Guid ToAccountId { get; set; }
		public string FromReference { get; set; }
		public string ToReference { get; set; }
		public decimal FromAccountBeforeBalance { get; set; }
		public decimal ToAccountBeforeBalance { get; set; }
		public decimal FromAccountBalance { get; set; }
		public decimal ToAccountBalance { get; set; }
		public DateTime CreatedDate { get; set; }
	}
}
