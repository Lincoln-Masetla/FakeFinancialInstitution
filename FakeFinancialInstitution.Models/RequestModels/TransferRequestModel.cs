using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FakeFinancialInstitution.Models.RequestModels
{
	public class TransferRequestModel
	{
		[Required]
		public string FromAccountNumber { get; set; }
		[Required]
		public string ToAccountNumber { get; set; }
		[Required]
		public decimal Amount { get; set; }
		[Required]
		public string FromReference { get; set; }
		[Required]
		public string ToReference { get; set; }
	}
}
