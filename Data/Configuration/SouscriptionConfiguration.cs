using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ClientMonitoring.Web.Domain.Entities;
using System.Reflection.Emit;

namespace ClientMonitoring.Web.Data.Configuration;

internal class SouscriptionConfiguration : IEntityTypeConfiguration<Souscription>
{
    public void Configure(EntityTypeBuilder<Souscription> builder)
    {
        builder.ToTable("Souscription");
        //builder.HasIndex(e => new { e.IdElecteur, e.IdListeElectoralCirconscription }).IsUnique(true);
        //builder.HasIndex(e => new { e.Nin, e.IdListeElectoralCirconscription }).IsUnique(true);
        builder.Property(e => e.CreatedAt).HasConversion(v => v.ToUniversalTime(), v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
    }
}

