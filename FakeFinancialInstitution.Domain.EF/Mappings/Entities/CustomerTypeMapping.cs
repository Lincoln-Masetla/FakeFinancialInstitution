using FakeFinancialInstitution.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FakeFinancialInstitution.Domain.EF.Mappings.Entities
{
    public sealed class CustomerTypeMapping : EntityMapping<CustomerType>
    {
        public override void Configure(EntityTypeBuilder<CustomerType> builder)
        {
            builder.ToTable("CustomerType");
        }
    }
}
