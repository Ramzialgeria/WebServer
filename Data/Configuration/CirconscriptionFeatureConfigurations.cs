using ClientMonitoring.Web.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ClientMonitoring.Web.Data.Configuration;
internal class CirconscriptionFeatureConfigurations : IEntityTypeConfiguration<CirconscriptionFeature>
{
    public void Configure(EntityTypeBuilder<CirconscriptionFeature> builder)
    {
        builder.ToTable("Circofeatures");
        builder.HasKey(s => new { s.FeatureId,s.CirconscriptionId });
        
    }
}