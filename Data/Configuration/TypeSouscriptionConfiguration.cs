using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ClientMonitoring.Web.Domain.Entities;
using System.Reflection.Emit;

namespace ClientMonitoring.Web.Data.Configuration;

internal class TypeSouscriptionConfiguration : IEntityTypeConfiguration<TypeSouscription>
{
    public void Configure(EntityTypeBuilder<TypeSouscription> builder)
    {
        builder.ToTable("TypeSouscription");
       
    }
}

