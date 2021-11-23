using System;

namespace FakeFinancialInstitution.Models.ResponseModels
{
	public class AccountResponseModel
	{
		public Guid? Id { get; set; }
		public string AccountNumber { get; set; }
		public string AccountName { get; set; }
		public Guid AccountTypeId { get; set; }
		public Guid CustomerId { get; set; }
		public decimal Balance { get; set; }
		public bool IsActive { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime UpdatedDate { get; set; }
	}
}
