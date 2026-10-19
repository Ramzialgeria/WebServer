using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ClientMonitoring.Web.Domain.Entities;
using System.Reflection.Emit;

namespace ClientMonitoring.Web.Data.Configuration;

internal class ConditionConfiguration : IEntityTypeConfiguration<Condition>
{
    public void Configure(EntityTypeBuilder<Condition> builder)
    {
        builder.ToTable("Condition");
        builder.Property(e => e.CreatedAt).HasConversion(v => v.ToUniversalTime(), v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
    }
}

internal class ConditionListeConfiguration : IEntityTypeConfiguration<ConditionListe>
{
    public void Configure(EntityTypeBuilder<ConditionListe> builder)
    {
        builder.ToTable("ConditionListe");
        builder.HasIndex(e => new { e.IdCondition, e.IdListeElectoral }).IsUnique();
        builder.Property(e => e.CreatedAt).HasConversion(v => v.ToUniversalTime(), v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
        builder.Navigation(e => e.Condition).AutoInclude();
        builder.Navigation(e => e.ListeElectoral).AutoInclude();
    }
}

internal class ConditionCandidatConfiguration : IEntityTypeConfiguration<ConditionCandidat>
{
    public void Configure(EntityTypeBuilder<ConditionCandidat> builder)
    {
        builder.ToTable("ConditionCandidat");
        builder.HasIndex(e => new { e.IdCondition, e.IdCandidat }).IsUnique();
        builder.Property(e => e.CreatedAt).HasConversion(v => v.ToUniversalTime(), v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
        builder.Navigation(e => e.Condition).AutoInclude();
        builder.Navigation(e => e.Candidature).AutoInclude();
    }
}