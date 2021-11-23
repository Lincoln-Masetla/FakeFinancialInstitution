using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FakeFinancialInstitution.Models.RequestModels
{
		public class AccountRequestModel
		{
			[Required]
			public string AccountName { get; set; }
			[Required]
			public Guid AccountTypeId { get; set; }
			[Required]
			public decimal Balance { get; set; }
	}
}
