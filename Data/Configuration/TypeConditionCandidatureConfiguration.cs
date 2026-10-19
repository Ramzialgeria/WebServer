using ClientMonitoring.Web.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClientMonitoring.Web.Data.Configuration
{
    public class TypeConditionCandidatureConfiguration : IEntityTypeConfiguration<TypeConditionCandidature>
    {
        public void Configure(EntityTypeBuilder<TypeConditionCandidature> builder)
        {
            builder.Property(tc => tc.IdTypeCondition).IsRequired();
             
            builder.Property(tc => tc.DescriptionAr)
                .IsRequired();

        }
    }
}
