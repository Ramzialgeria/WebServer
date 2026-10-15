using Microsoft.EntityFrameworkCore;

public class ElectionDbContext : DbContext
{
    public ElectionDbContext(
        DbContextOptions<ElectionDbContext> options)
        : base(options)
    {
    }

    public DbSet<ListeElectoralAPC> ListeElectoralAPC => Set<ListeElectoralAPC>();

    public DbSet<ListeElectoralAPW> ListeElectoralAPW => Set<ListeElectoralAPW>();

    public DbSet<Candidature> Candidature => Set<Candidature>();

    public DbSet<CircoAPC> CircoAPC => Set<CircoAPC>();

    public DbSet<CircoAPW> CircoAPW => Set<CircoAPW>();
}