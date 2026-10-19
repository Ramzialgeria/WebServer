using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ClientMonitoring.Web.Domain.Entities;
using System.Reflection.Emit;

namespace ClientMonitoring.Web.Data.Configuration;

internal class PartiePolitiqueConfiguration : IEntityTypeConfiguration<PartiePolitique>
{
    public void Configure(EntityTypeBuilder<PartiePolitique> builder)
    {
        builder.ToTable("PartiePolitique");
        builder.Property(e => e.CreatedAt).HasConversion(v => v.ToUniversalTime(), v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
        builder.Property(e => e.DateCreation).HasConversion(v => v.ToUniversalTime(), v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
    }
}

internal class PartieConfiguration : IEntityTypeConfiguration<Partie>
{
    public void Configure(EntityTypeBuilder<Partie> builder)
    {
        builder.ToTable("Partie");
    }
}

internal class LibreConfiguration : IEntityTypeConfiguration<Libre>
{
    public void Configure(EntityTypeBuilder<Libre> builder)
    {
        builder.ToTable("Libre");
    }
}

internal class UnionConfiguration : IEntityTypeConfiguration<Union>
{
    public void Configure(EntityTypeBuilder<Union> builder)
    {
        builder.ToTable("Union");
    }
}
