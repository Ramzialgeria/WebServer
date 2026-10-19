using ClientMonitoring.Web.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ClientMonitoring.Web.Data.Configuration;

internal class FeaturesConfigurations : IEntityTypeConfiguration<Features>
{
    public void Configure(EntityTypeBuilder<Features> builder)
    {
        builder.ToTable("features");
        builder.HasKey(s => s.Id);
        builder.HasIndex(s => s.FeatureName).IsUnique();
    }
}

