using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ClientMonitoring.Web.Domain.Entities;
using System.Reflection.Emit;

namespace ClientMonitoring.Web.Data.Configuration;

internal class RetraitDossierConfiguration : IEntityTypeConfiguration<RetraitDossier>
{
    public void Configure(EntityTypeBuilder<RetraitDossier> builder)
    {
        builder.ToTable("RetraitDossier");
        builder.Property(e => e.DateRetrait).HasConversion(v => v.ToUniversalTime(), v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
        builder.Property(e => e.DateRetraitSystem).HasConversion(v => v.ToUniversalTime(), v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
    }
}

