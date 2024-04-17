using AgiletyFramework.DBModels.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgiletyFramework.DBModels.EntityTypeConfigs
{
    public class RoleEntityConfiguration : IEntityTypeConfiguration<RoleEntity>
    {
        public void Configure(EntityTypeBuilder<RoleEntity> builder)
        {
            builder.ToTable("Role");
            builder.HasKey(o => o.Id);
            builder.Property(o => o.RoleName).HasMaxLength(20);
            builder.HasMany(o => o.Users).WithMany(o => o.Roles);
            builder.HasMany<MenuEntity>(o => o.Menus)
                .WithMany(o => o.Roles)
                .UsingEntity<RoleMenuMapEntity>(
                    m => m.HasOne<MenuEntity>(o => o.Menu).WithMany(o => o.RoleMenuMaps).HasForeignKey(o => o.MenuId),
                    r => r.HasOne<RoleEntity>(o => o.Role).WithMany(o => o.RoleMenuMaps).HasForeignKey(o => o.RoleId)
                );
        }
    }
}
