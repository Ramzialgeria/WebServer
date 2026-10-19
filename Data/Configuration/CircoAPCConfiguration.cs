using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ClientMonitoring.Web.Domain.Entities;
using System.Reflection.Emit;

namespace ClientMonitoring.Web.Data.Configuration;

internal class CircoAPCConfiguration : IEntityTypeConfiguration<CircoAPC>
{
    public void Configure(EntityTypeBuilder<CircoAPC> builder)
    {
        builder.ToTable("CircoAPC");
      
    }
}

