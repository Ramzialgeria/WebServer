using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ClientMonitoring.Web.Domain.Entities;
using System.Reflection.Emit;

namespace ClientMonitoring.Web.Data.Configuration;

internal class JugementCorrespondanceConfiguration : IEntityTypeConfiguration<JugementCorrespondance>
{
    public void Configure(EntityTypeBuilder<JugementCorrespondance> builder)
    {
        builder.ToTable("JugementCorrespondance");
        builder.Property(e => e.CreatedAt).HasConversion(v => v.ToUniversalTime(), v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
    }
}

