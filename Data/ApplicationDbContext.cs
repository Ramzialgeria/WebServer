using ClientMonitoring.Web.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ClientMonitoring.Web.Data;

public class ApplicationDbContext
    : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    // Central web application users
    // Comes from AspNetUsers through IdentityDbContext.

    public DbSet<Client> Clients =>
        Set<Client>();

    // Users that will be placed in activation files
    public DbSet<AppUser> CommuneUsers =>
        Set<AppUser>();

    public DbSet<CommuneClientStatus> CommuneClientStatuses =>
        Set<CommuneClientStatus>();

    protected override void OnModelCreating(
        ModelBuilder b)
    {
        base.OnModelCreating(b);

        b.Entity<Client>()
            .HasIndex(x => x.ClientCode)
            .IsUnique();

        b.Entity<AppUser>(entity =>
        {
            entity.ToTable("CommuneUsers");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.Id)
                .HasColumnName("id");

            entity.Property(x => x.Username)
                .HasColumnName("username");

            entity.Property(x => x.PasswordHash)
                .HasColumnName("passwordhash");

            entity.Property(x => x.IdStructure)
                .HasColumnName("idstructure");

            entity.Property(x => x.Role)
                .HasColumnName("role");
        });
    }
}