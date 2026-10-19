using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ClientMonitoring.Web.Domain.Entities;
using System.Reflection.Emit;

namespace ClientMonitoring.Web.Data.Configuration;

internal class PvSouscriptionConfiguration : IEntityTypeConfiguration<PvSouscription>
{
    public void Configure(EntityTypeBuilder<PvSouscription> builder)
    {
        builder.ToTable("PvSouscription");
        builder.HasOne(p => p.ListeElectoralCirconscription).WithOne(l => l.PvSouscription).HasForeignKey<PvSouscription>(p => p.IdListeElectoralCirconscription);
        builder.Property(e => e.CreatedAt).HasConversion(v => v.ToUniversalTime(), v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
    }
}

