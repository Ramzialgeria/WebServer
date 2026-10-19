using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

using System.Reflection.Emit;
using ClientMonitoring.Web.Domain.Identity;

namespace ClientMonitoring.Web.Data.Configuration.IDentityConfigurations;

internal class CommuneAdressConfiguration : IEntityTypeConfiguration<Structure>
{
    public void Configure(EntityTypeBuilder<Structure> builder)
    {
        builder.ToTable("Structure");
        builder.HasIndex(s => s.IdStructure).IsUnique();
    }
}
internal class WilayaConfiguration : IEntityTypeConfiguration<Wilaya>
{
    public void Configure(EntityTypeBuilder<Wilaya> builder)
    {
        builder.ToTable("Wilaya");
        builder.Navigation(e => e.Communes).AutoInclude();
    }
}

internal class CommuneConfiguration : IEntityTypeConfiguration<Commune>
{
    public void Configure(EntityTypeBuilder<Commune> builder)
    {
        builder.ToTable("Commune");
        builder.HasOne(p => p.Wilaya)
            .WithMany(p => p.Communes)
            .HasForeignKey(p => p.CodeWilaya)
            .HasPrincipalKey(p => p.IdStructure);
    }
}



