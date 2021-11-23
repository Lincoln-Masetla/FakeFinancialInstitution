using FakeFinancialInstitution.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FakeFinancialInstitution.Domain.EF.Mappings.Entities
{
    public sealed class CustomerMapping : EntityMapping<Customer>
    {
        public override void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customer");
        }
    }
}
