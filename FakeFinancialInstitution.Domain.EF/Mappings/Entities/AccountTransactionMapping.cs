using FakeFinancialInstitution.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FakeFinancialInstitution.Domain.EF.Mappings.Entities
{
    public sealed class AccountTransactionMapping : EntityMapping<AccountTransaction>
    {
        public override void Configure(EntityTypeBuilder<AccountTransaction> builder)
        {
            builder.ToTable("AccountTransaction");
        }
    }
}
