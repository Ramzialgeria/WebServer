using ClientMonitoring.Web.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClientMonitoring.Web.Data.Configuration;

//internal class AppUserConfiguartion : IEntityTypeConfiguration<AppUser>
//{
//    public void Configure(EntityTypeBuilder<AppUser> builder)
//    {
//        builder.ToTable("Users");
//        builder.HasIndex(e => e.PhoneNumber).IsUnique();
//        builder.HasIndex(e => e.Email).IsUnique();
//        builder.HasIndex(e => e.Nin).IsUnique();
//        builder.Property(e => e.DateNaissance).HasConversion(v => v.ToUniversalTime(), v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
//        builder.Navigation(e => e.Structure).AutoInclude();
//        builder.Navigation(e => e.Fonction).AutoInclude();
//        builder.Navigation(e => e.Roles).AutoInclude();
//        builder.Navigation(e => e.LoginHistories).AutoInclude();
//        builder.HasMany(e => e.Roles)
//           .WithMany(e => e.Users)
//           .UsingEntity<IdentityUserRole<string>>(
//               j =>
//               {
//                   j.ToTable("UserRoles"); // Specify the join table name
//               }
//           );
//    }
//}
//internal class AppRolesConfiguartion : IEntityTypeConfiguration<AppRole>
//{
//    public void Configure(EntityTypeBuilder<AppRole> builder)
//    {
//        builder.ToTable("Roles");
//        builder.Property(p => p.Description).HasMaxLength(256).HasDefaultValue("وصف المهام غير مدرج");
//        builder.HasMany(r => r.Users).WithMany(u => u.Roles).UsingEntity<IdentityUserRole<string>>(
//            j =>
//            {
//                j.ToTable("UserRoles"); // Specify the join table name
//            }
//        );
//    }
//}
