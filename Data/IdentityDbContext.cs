using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ClientMonitoring.Web.Domain.Identity;
using static System.Runtime.InteropServices.JavaScript.JSType;
using ClientMonitoring.Web.Data.Configuration;
using ClientMonitoring.Web.Data.Configuration.IDentityConfigurations;

namespace ClientMonitoring.Web.Data;


public class IdentityDbContext(DbContextOptions<IdentityDbContext> options) : IdentityDbContext<AppUser, AppRole, string>(options)
{
    public DbSet<LoginHistory> LoginHistories { get; set; }
    public DbSet<Fonction> Fonction { get; set; }
    public DbSet<Structure> Structure { get; set; }
    public DbSet<Central> Central { get; set; }
    public DbSet<Commune> Commune { get; set; }
    public DbSet<Wilaya> Wilaya { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema("public");
        modelBuilder.ApplyConfiguration(new AppUserConfiguartion());
        modelBuilder.ApplyConfiguration(new AppRolesConfiguartion());
        modelBuilder.ApplyConfiguration(new LoginHistoriqueConfiguartion());
        modelBuilder.ApplyConfiguration(new WilayaConfiguration());
        modelBuilder.ApplyConfiguration(new CommuneConfiguration());

        modelBuilder.Entity<IdentityUserClaim<string>>(entity => entity.ToTable("UserClaims"));
        modelBuilder.Entity<IdentityUserLogin<string>>(entity => entity.ToTable("UserLogins"));
        modelBuilder.Entity<IdentityRoleClaim<string>>(entity => entity.ToTable("RoleClaims"));
        modelBuilder.Entity<IdentityUserToken<string>>(entity => entity.ToTable("UserTokens"));
    }

}