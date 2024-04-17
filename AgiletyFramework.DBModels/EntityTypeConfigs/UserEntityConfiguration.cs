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
    public class UserEntityConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.ToTable("User");
            builder.HasKey(o => o.Id);
            builder.Property(o => o.Name).HasMaxLength(20);
            builder.HasMany<RoleEntity>(o => o.Roles)
                .WithMany(o => o.Users)
                .UsingEntity<UserRoleMapEntity>(
                    u => u.HasOne<RoleEntity>(o => o.Role).WithMany(o => o.UserRoleMaps).HasForeignKey(o => o.RoleId),
                    r => r.HasOne<UserEntity>(o => o.User).WithMany(o => o.UserRoleMaps).HasForeignKey(o => o.UserId)
                );
        }
    }
}
