using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ClientMonitoring.Web.Domain.Entities;
using System.Reflection.Emit;

namespace ClientMonitoring.Web.Data.Configuration;

internal class ScrutinConfiguration : IEntityTypeConfiguration<Scrutin>
{
    public void Configure(EntityTypeBuilder<Scrutin> builder)
    {
        builder.ToTable("Scrutin");
        builder.HasIndex(e => e.DateScrutin).IsUnique();
        builder.HasIndex(e => e.Description).IsUnique();
        builder.Property(e => e.DateScrutin).HasConversion(v => v.ToUniversalTime(), v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
        builder.Property(e => e.CreatedAt).HasConversion(v => v.ToUniversalTime(), v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
    }
}

