using ClientMonitoring.Web.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClientMonitoring.Web.Data.Configuration
{
    internal class DesisteListeConfiguration : IEntityTypeConfiguration<DesisteListe>
    {
        public void Configure(EntityTypeBuilder<DesisteListe> builder)
        {
            // 🔑 Primary Key
            builder.HasKey(d => d.IdDesisteListe);

            // 🔗 One-to-One relationship
            builder.HasOne(d => d.ListeElectoralCirconscription)
                   .WithOne(l => l.DesisteListe)
                   .HasForeignKey<DesisteListe>(d => d.IdListeCirconscription)
                   .OnDelete(DeleteBehavior.Cascade);

            // 🚫 Enforce ONE-TO-ONE at DB level
            builder.HasIndex(d => d.IdListeCirconscription)
                   .IsUnique();

            // 📌 Required fields
            builder.Property(d => d.Remarque)
                   .IsRequired();

            builder.Property(d => d.CreatedBy)
                   .IsRequired();

            // ⏱ Dates
            builder.Property(d => d.DateDesiste)
                   .IsRequired();

            builder.Property(d => d.DateDesisteSystem)
                   .IsRequired();

            // Optional fields
            builder.Property(d => d.UpdatedBy)
                   .IsRequired(false);

            builder.Property(d => d.UpdatedAt)
                   .IsRequired(false);
            builder.Property(e => e.DateDesiste).HasConversion(v => v.ToUniversalTime(), v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
            builder.Property(e => e.DateDesisteSystem).HasConversion(v => v.ToUniversalTime(), v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
        }
    }
}
