using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ClientMonitoring.Web.Domain.Entities;
using System.Reflection.Emit;

namespace ClientMonitoring.Web.Data.Configuration;

internal class CandidatureConfiguration : IEntityTypeConfiguration<Candidature>
{
    public void Configure(EntityTypeBuilder<Candidature> builder)
    {
        builder.ToTable("Candidature");
        builder.HasIndex(e => new { e.IdListeElectoral, e.IdCandidatInfo}).IsUnique();
        builder.HasIndex(e => new { e.IdListeElectoral, e.Order }).IsUnique();
        builder.Property(e => e.CreatedAt).HasConversion(v => v.ToUniversalTime(), v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
        builder.Navigation(e => e.ListeElectoral).AutoInclude();
        builder.Navigation(e => e.CandidatInfo).AutoInclude();
        builder.HasQueryFilter(e => e.IdTypeMutted == null);

        builder.HasAlternateKey(e => new
        {
            e.IdCandidat,
            e.IdListeElectoral
        });

        //builder.HasOne(c => c.CandidatureRemplace)
    //.WithOne(c => c.RemplacePar)
    //.HasForeignKey<Candidature>(c => c.IdCandidatureRemplace)
    //.OnDelete(DeleteBehavior.Restrict);
    }
}

internal class DossierCandidatConfiguration : IEntityTypeConfiguration<DossierCandidat>
{
    public void Configure(EntityTypeBuilder<DossierCandidat> builder)
    {
        builder.ToTable("DossierCandidat");
        builder.HasIndex(e => new { e.IdCandidat, e.IdType }).IsUnique();
        builder.Property(e => e.CreatedAt).HasConversion(v => v.ToUniversalTime(), v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
        builder.Navigation(e => e.Candidature).AutoInclude();
        builder.Navigation(e => e.TypeDossier).AutoInclude();
    }
}