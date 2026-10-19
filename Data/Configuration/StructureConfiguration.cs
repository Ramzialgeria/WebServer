using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ClientMonitoring.Web.Domain.Entities;
using System.Reflection.Emit;

namespace ClientMonitoring.Web.Data.Configuration;

//internal class StructureConfiguration : IEntityTypeConfiguration<Structure>
//{
//    public void Configure(EntityTypeBuilder<Structure> builder)
//    {
//        builder.ToTable("Structure");
//        builder.HasIndex(s => s.IdStructure).IsUnique();
//    }
//}
//internal class WilayaConfiguration : IEntityTypeConfiguration<Wilaya>
//{
//    public void Configure(EntityTypeBuilder<Wilaya> builder)
//    {
//        builder.ToTable("Wilaya");
//        builder.Navigation(e => e.Communes).AutoInclude();
//    }
//}

//internal class CommuneConfiguration : IEntityTypeConfiguration<Commune>
//{
//    public void Configure(EntityTypeBuilder<Commune> builder)
//    {
//        builder.ToTable("Commune");
//        builder.HasOne(p => p.Wilaya)
//            .WithMany(p => p.Communes)
//            .HasForeignKey(p => p.CodeWilaya)
//            .HasPrincipalKey(p => p.IdStructure);
//    }
//}

internal class CircoAPWConfiguration : IEntityTypeConfiguration<CircoAPW>
{
    public void Configure(EntityTypeBuilder<CircoAPW> builder)
    {
        builder.ToTable("CircoAPW");
        //   builder.Property(e => e.CreatedAt).HasConversion(v => v.ToUniversalTime(), v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
        builder.Navigation(e => e.JugementCorrespondanceSent).AutoInclude(false);
        builder.Navigation(e => e.JugementCorrespondanceReturn).AutoInclude(false);
    }
}



