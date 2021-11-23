using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FakeFinancialInstitution.Models.RequestModels
{
	public class CustomerRequestModel
	{
		[Required]
		public string CustomerName { get; set; }
		[Required]
		public Guid CustomerTypeId { get; set; }
		[Required]
		public string CustomerPhone { get; set; }
		[Required]
		[DataType(DataType.EmailAddress)]
		[EmailAddress]
		public string CustomerEmail { get; set; }
		[Required]
		public string CustomerIdNumber { get; set; }
	}
}
