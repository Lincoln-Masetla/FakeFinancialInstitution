using System;

namespace FakeFinancialInstitution.Models.ResponseModels
{
	public class CustomerResponseModel
	{
		public Guid? Id { get; set; }
		public bool IsActive { get; set; }
		public string CustomerName { get; set; }
		public Guid CustomerTypeId { get; set; }
		public string CustomerPhone { get; set; }
		public string CustomerEmail { get; set; }
		public string CustomerIdNumber { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime UpdatedDate { get; set; }
	}
}
