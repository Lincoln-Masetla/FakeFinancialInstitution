using FakeFinancialInstitution.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FakeFinancialInstitution.Domain.EF.Mappings.Entities
{
    public sealed class AccountTransactionTypeMapping : EntityMapping<AccountTransactionType>
    {
        public override void Configure(EntityTypeBuilder<AccountTransactionType> builder)
        {
            builder.ToTable("AccountTransactionType");
        }
    }
}
