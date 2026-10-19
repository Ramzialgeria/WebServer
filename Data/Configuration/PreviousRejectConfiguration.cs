using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ClientMonitoring.Web.Domain.Entities;
using System.Reflection.Emit;

namespace ClientMonitoring.Web.Data.Configuration;

internal class PreviousRejectConfiguration : IEntityTypeConfiguration<PreviousReject>
{
    public void Configure(EntityTypeBuilder<PreviousReject> builder)
    {
        builder.ToTable("PreviousReject");
    }
}

