using ClientMonitoring.Web.Models;
using Microsoft.EntityFrameworkCore;

public class StructureDbContext : DbContext
{
    public StructureDbContext(
        DbContextOptions<StructureDbContext> options)
        : base(options)
    {
    }

    public DbSet<Wilaya> Wilayas => Set<Wilaya>();

    public DbSet<Commune> Communes => Set<Commune>();

    public DbSet<Center_V> Center_V => Set<Center_V>();

    public DbSet<BureauV> BureauV => Set<BureauV>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Wilaya>(entity =>
        {
            entity.ToTable("Wilaya");

            entity.HasKey(x => x.codeWilaya);

            entity.Property(x => x.codeWilaya)
                .HasColumnName("codeWilaya");

            entity.Property(x => x.Libele)
                .HasColumnName("Libele");
        });

        modelBuilder.Entity<Commune>(entity =>
        {
            entity.ToTable("Commune");

            entity.HasKey(x => x.codecommune);

            entity.Property(x => x.codecommune)
                .HasColumnName("codecommune");

            entity.Property(x => x.libeleCommune)
                .HasColumnName("libeleCommune");

            entity.Property(x => x.codewilaya)
                .HasColumnName("codewilaya");

            entity.HasOne(x => x.Wilaya)
                .WithMany(x => x.Communes)
                .HasForeignKey(x => x.codewilaya)
                .HasPrincipalKey(x => x.codeWilaya);
        });

        modelBuilder.Entity<Center_V>(entity =>
        {
            entity.ToTable("Center_V");

            entity.HasKey(x => x.ID_center);

            entity.Property(x => x.ID_center)
                .HasColumnName("ID_center");

            entity.Property(x => x.descriptio_ar)
                .HasColumnName("descriptio_ar");

            entity.Property(x => x.adress)
                .HasColumnName("adress");

            entity.Property(x => x.id_structure)
                .HasColumnName("id_structure");
        });

        modelBuilder.Entity<BureauV>(entity =>
        {
            entity.ToTable("BureauV");

            entity.HasKey(x => x.ID_Bureau);

            entity.Property(x => x.ID_Bureau)
                .HasColumnName("ID_Bureau");

            entity.Property(x => x.CBureau)
                .HasColumnName("CBureau");

            entity.Property(x => x.Nb_Inscrits)
                .HasColumnName("Nb_Inscrits");

            entity.Property(x => x.ID_center)
                .HasColumnName("ID_center");
        });
    }
}