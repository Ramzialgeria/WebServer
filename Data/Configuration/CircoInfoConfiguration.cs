using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ClientMonitoring.Web.Domain.Entities;
using System.Reflection.Emit;

namespace ClientMonitoring.Web.Data.Configuration;

internal class CircoInfoConfiguration : IEntityTypeConfiguration<CircoInfo>
{
    public void Configure(EntityTypeBuilder<CircoInfo> builder)
    {
        builder.ToTable("CircoInfo");
        builder.HasIndex(e => new { e.CodeCirconscription, e.IdInfoValidation }).IsUnique();
        builder.Property(e => e.CreatedAt).HasConversion(v => v.ToUniversalTime(), v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
    }
}

