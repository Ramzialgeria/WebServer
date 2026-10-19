using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ClientMonitoring.Web.Domain.Entities;
using System.Reflection.Emit;

namespace ClientMonitoring.Web.Data.Configuration;

internal class RecoursListeConfiguration : IEntityTypeConfiguration<RecoursListe>
{
    public void Configure(EntityTypeBuilder<RecoursListe> builder)
    {
        builder.ToTable("RecoursListe");
        builder.HasIndex(e => new { e.TypeRecours, e.IdResultatListHisto }).IsUnique();
        builder.Property(e => e.DateRecour).HasConversion(v => v.ToUniversalTime(), v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
        builder.Property(e => e.DateRecourSystem).HasConversion(v => v.ToUniversalTime(), v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
    }
}

