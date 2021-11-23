using FakeFinancialInstitution.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FakeFinancialInstitution.Domain.EF.Mappings.Entities
{
    public sealed class AccountTypeMapping : EntityMapping<AccountType>
    {
        public override void Configure(EntityTypeBuilder<AccountType> builder)
        {
            builder.ToTable("AccountType");
        }
    }
}
