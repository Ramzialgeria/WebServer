using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ClientMonitoring.Web.Domain.Entities;
using System.Reflection.Emit;

namespace ClientMonitoring.Web.Data.Configuration;

internal class Previous4percentConfiguration : IEntityTypeConfiguration<Previous4percent>
{
    public void Configure(EntityTypeBuilder<Previous4percent> builder)
    {
        builder.ToTable("Previous4percent");
    }
}

