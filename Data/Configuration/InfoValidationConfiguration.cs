using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ClientMonitoring.Web.Domain.Entities;
using System.Reflection.Emit;

namespace ClientMonitoring.Web.Data.Configuration;

internal class InfoValidationConfiguration : IEntityTypeConfiguration<InfoValidation>
{
    public void Configure(EntityTypeBuilder<InfoValidation> builder)
    {
        builder.ToTable("InfoValidation");
        
    }
}

