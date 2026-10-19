using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ClientMonitoring.Web.Domain.Entities;
using System.Reflection.Emit;

namespace ClientMonitoring.Web.Data.Configuration;

internal class TypeMuttedConfiguration : IEntityTypeConfiguration<TypeMutted>
{
    public void Configure(EntityTypeBuilder<TypeMutted> builder)
    {
        builder.ToTable("TypeMutted");        
       
    }
}

