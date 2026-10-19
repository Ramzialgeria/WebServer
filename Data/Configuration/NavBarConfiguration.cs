using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ClientMonitoring.Web.Domain.Entities;

namespace ClientMonitoring.Web.Data.Configuration;

internal class NavBarConfiguration : IEntityTypeConfiguration<Navbar>
{
    public void Configure(EntityTypeBuilder<Navbar> builder)
    {
        builder.ToTable("Navbar");
        builder.HasIndex(e => new { e.Controller, e.View, e.Structure }).IsUnique();
        builder.Property(e => e.DateDebut).HasConversion(v => v.ToUniversalTime(), v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
        builder.Property(e => e.DateFin).HasConversion(v => v.ToUniversalTime(), v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
        builder.Property(e => e.CreatedAt).HasConversion(v => v.ToUniversalTime(), v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
        builder.Navigation(e => e.Type).AutoInclude();
    }
}

internal class NavBarTypeConfiguration : IEntityTypeConfiguration<NavbarType>
{
    public void Configure(EntityTypeBuilder<NavbarType> builder)
    {
        builder.ToTable("NavbarType");
        builder.HasIndex(e => new { e.Description}).IsUnique();
        builder.Property(e => e.CreatedAt).HasConversion(v => v.ToUniversalTime(), v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
    }
}