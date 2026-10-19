using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ClientMonitoring.Web.Domain.Entities;
using System.Reflection.Emit;

namespace ClientMonitoring.Web.Data.Configuration;

internal class RecoursConfiguration : IEntityTypeConfiguration<Recours>
{
    public void Configure(EntityTypeBuilder<Recours> builder)
    {
        builder.ToTable("Recours");
        builder.HasIndex(e => new { e.TypeRecours, e.IdResultCandidatHisto }).IsUnique();
        builder.Property(e => e.DateRecour).HasConversion(v => v.ToUniversalTime(), v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
        builder.Property(e => e.DateRecourSystem).HasConversion(v => v.ToUniversalTime(), v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
    }
}

