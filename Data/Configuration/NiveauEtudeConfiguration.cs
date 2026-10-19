using ClientMonitoring.Web.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClientMonitoring.Web.Data.Configuration;

internal class NiveauEtudeConfiguration : IEntityTypeConfiguration<NiveauEtude>
{
    public void Configure(EntityTypeBuilder<NiveauEtude> builder)
    {
        builder.ToTable("NiveauxEtude");
        builder.HasKey(x => x.Id);
        builder.Property(x=>x.Description).IsRequired();

    }
}

