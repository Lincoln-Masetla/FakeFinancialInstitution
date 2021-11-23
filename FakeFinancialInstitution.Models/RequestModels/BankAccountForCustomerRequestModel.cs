using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FakeFinancialInstitution.Models.RequestModels
{
	public class BankAccountForCustomerRequestModel
	{
		[Required]
		public List<AccountRequestModel> Accounts { get; set; }
		[Required]
		public CustomerRequestModel Customer { get; set; }
	}
}
