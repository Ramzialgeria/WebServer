using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ClientMonitoring.Web.Domain.Entities;
using System.Reflection.Emit;

namespace ClientMonitoring.Web.Data.Configuration;

internal class EnqueteResultConfiguration : IEntityTypeConfiguration<EnqueteResult>
{
    public void Configure(EntityTypeBuilder<EnqueteResult> builder)
    {
        builder.ToTable("EnqueteResult");
        builder.Property(e => e.DateResult).HasConversion(v => v.ToUniversalTime(), v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
    }
}

