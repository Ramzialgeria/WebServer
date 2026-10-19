using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ClientMonitoring.Web.Domain.Entities;
using System.Reflection.Emit;

namespace ClientMonitoring.Web.Data.Configuration;

internal class RepresentantConfiguration : IEntityTypeConfiguration<Representant>
{
    public void Configure(EntityTypeBuilder<Representant> builder)
    {
        builder.ToTable("Representant");
        builder.HasIndex(e => e.Nin).IsUnique();
        builder.Property(e => e.DateNaissance).HasConversion(v => v.ToUniversalTime(), v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
        builder.Property(e => e.CreatedAt).HasConversion(v => v.ToUniversalTime(), v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
    }
}

