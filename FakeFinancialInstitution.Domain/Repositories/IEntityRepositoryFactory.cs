using System;
using System.Collections.Generic;
using System.Text;

namespace FakeFinancialInstitution.Domain.Repositories
{
    public interface IEntityRepositoryFactory
    {
        IEntityRepository CreateRepository();
    }
}
