using System;
using System.Collections.Generic;
using System.Text;

namespace FakeFinancialInstitution.Domain.Entities
{
    public interface IEntity
    {
        Guid? Id { get; set; }
    }
}
