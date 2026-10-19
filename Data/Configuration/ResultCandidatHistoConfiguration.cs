using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ClientMonitoring.Web.Domain.Entities;
using System.Reflection.Emit;

namespace ClientMonitoring.Web.Data.Configuration;

internal class ResultCandidatHistoConfiguration : IEntityTypeConfiguration<ResultCandidatHisto>
{
    public void Configure(EntityTypeBuilder<ResultCandidatHisto> builder)
    {
        builder.ToTable("ResultCandidatHisto");
        builder.HasIndex(e => new { e.IdCandidat, e.NumResult }).IsUnique();
        builder.Property(e => e.DateResult).HasConversion(v => v.ToUniversalTime(), v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
        builder.Property(e => e.DateResultSystem).HasConversion(v => v.ToUniversalTime(), v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
    }
}

