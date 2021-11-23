using System;
using System.Collections.Generic;
using System.Text;

namespace FakeFinancialInstitution.Domain.Entities
{
	public class Account : IEntity
	{
		public Account()
		{
			Id = Guid.NewGuid();
			CreatedDate = DateTime.UtcNow;
			UpdatedDate = DateTime.Now;
			IsActive = true;
		}
		public Guid? Id { get; set; }
		public string AccountNumber { get; set; }
		public string AccountName { get; set; }
		public Guid AccountTypeId { get; set; }
		public Guid CustomerId { get; set; }
		public Decimal Balance { get; set; }
		public bool IsActive { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime UpdatedDate { get; set; }

	}
}
