using FakeFinancialInstitution.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FakeFinancialInstitution.Domain.EF.Mappings.Entities
{
    public sealed class AccountMapping : EntityMapping<Account>
    {
        public override void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable("Account");
        }
    }
}
