using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ClientMonitoring.Web.Domain.Entities;
using System.Reflection.Emit;

namespace ClientMonitoring.Web.Data.Configuration;

internal class CandidatInfoConfiguration : IEntityTypeConfiguration<CandidatInfo>
{
    public void Configure(EntityTypeBuilder<CandidatInfo> builder)
    {
        builder.ToTable("CandidatInfo");
        // gerer les doublants par applictaion 
        // on laisse double inscription et on les declare 
        //builder.HasIndex(e => e.Nin).IsUnique();
        //builder.HasIndex(e => e.IdElecteur).IsUnique();
        //builder.HasIndex(e => new { e.NInscription, e.CodeCommuneVote}).IsUnique();
        builder.Property(e => e.DateNaissance).HasConversion(v => v.ToUniversalTime(), v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
        builder.Property(e => e.CreatedAt).HasConversion(v => v.ToUniversalTime(), v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
        builder.Navigation(e => e.NiveauEtude).AutoInclude();
        builder.Navigation(e=>e.Fonction).AutoInclude();


    }
}

