using System;
using System.Collections.Generic;
using System.Text;

namespace FakeFinancialInstitution.Domain.Entities
{
	public class AccountType : IEntity
	{
		public AccountType()
		{
			Id = Guid.NewGuid();
			CreatedDate = DateTime.UtcNow;
			UpdatedDate = DateTime.Now;
			IsActive = true;
		}
		public Guid? Id { get; set; }
		public string AccountTypeName { get; set; }
		public bool IsActive { get; set; }
		public string AccountTypeDescription { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime UpdatedDate { get; set; }
	}
}
