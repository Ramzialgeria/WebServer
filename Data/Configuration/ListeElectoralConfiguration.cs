using ClientMonitoring.Web.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ClientMonitoring.Web.Data.Configuration
{

    internal class ListeElectoralConfiguration : IEntityTypeConfiguration<ListeElectoral>
    {
        public void Configure(EntityTypeBuilder<ListeElectoral> builder)
        {
            builder.ToTable("ListeElectoral");
            //builder.HasIndex(e => new { e.IdListe, e.CodeCirconscription, e.IdScrutin }).IsUnique();
            builder.Property(e => e.CreatedAt).HasConversion(v => v.ToUniversalTime(), v => DateTime.SpecifyKind(v, DateTimeKind.Utc));

            builder.Property(e => e.DateFirstRetrait).HasConversion(v => v.ToUniversalTime(), v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
            builder.Property(e => e.DateFirstRetraitSystem).HasConversion(v => v.ToUniversalTime(), v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
            //builder.Property(e => e.DateDepotDossier).HasConversion(v => v.ToUniversalTime(), v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
            //builder.Property(e => e.DateDepotDossierSystem).HasConversion(v => v.ToUniversalTime(), v => DateTime.SpecifyKind(v, DateTimeKind.Utc));

            builder.Navigation(e => e.PartiePolitique).AutoInclude();
            //builder.Navigation(e => e.Circonscription).AutoInclude();
            //builder.Navigation(e => e.Scrutin).AutoInclude();


        }
    }

    internal class ListeElectoralAPWConfiguration : IEntityTypeConfiguration<ListeElectoralAPW>
    {
        public void Configure(EntityTypeBuilder<ListeElectoralAPW> builder)
        {
            builder.ToTable("ListeElectoralAPW");

            builder.HasIndex(x => new
            {
                x.IdListe,
                x.CodeCircoAPW,
                x.IdScrutin
            }).IsUnique();

            builder.Navigation(x => x.CircoAPW).AutoInclude();

            builder.Navigation(e => e.Scrutin).AutoInclude();
        }
    }

    internal class ListeElectoralAPCConfiguration : IEntityTypeConfiguration<ListeElectoralAPC>
    {
        public void Configure(EntityTypeBuilder<ListeElectoralAPC> builder)
        {
            builder.ToTable("ListeElectoralAPC");

            builder.HasIndex(x => new
            {
                x.IdListe,
                x.CodeCircoAPC,
                x.IdScrutin
            }).IsUnique();

            builder.Navigation(x => x.CircoAPC).AutoInclude();

            builder.Navigation(e => e.Scrutin).AutoInclude();
        }
    }

    internal class DossierListeElectoralConfiguration : IEntityTypeConfiguration<DossierListeElectoral>
    {
        public void Configure(EntityTypeBuilder<DossierListeElectoral> builder)
        {
            builder.ToTable("DossierListeElectoral");
            builder.HasIndex(e => new { e.IdListeElectoral, e.IdType }).IsUnique();
            builder.Property(e => e.CreatedAt).HasConversion(v => v.ToUniversalTime(), v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
            builder.Navigation(e => e.TypeDossier).AutoInclude();
            builder.Navigation(e => e.ListeElectoral).AutoInclude();
        }
    }

    internal class TypeDossierConfiguration : IEntityTypeConfiguration<TypeDossier>
    {
        public void Configure(EntityTypeBuilder<TypeDossier> builder)
        {
            builder.ToTable("TypeDossier");
            builder.Property(e => e.CreatedAt).HasConversion(v => v.ToUniversalTime(), v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
        }
    }
}
