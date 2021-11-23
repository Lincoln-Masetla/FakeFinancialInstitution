using System;
using System.Collections.Generic;
using System.Text;

namespace FakeFinancialInstitution.Domain.Entities
{
	public class CustomerType : IEntity
	{
		public CustomerType()
		{
			Id = Guid.NewGuid();
			CreatedDate = DateTime.UtcNow;
			UpdatedDate = DateTime.Now;
			IsActive = true;
		}
		public Guid? Id { get; set; }
		public string CustomerTypeName { get; set; }
		public bool IsActive { get; set; }
		public string CustomerTypeDescription { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime UpdatedDate { get; set; }
	}
}
