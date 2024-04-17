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
    public class UserRoleMapEntityConfiguration : IEntityTypeConfiguration<UserRoleMapEntity>
    {
        public void Configure(EntityTypeBuilder<UserRoleMapEntity> builder)
        {
            builder.ToTable("UserRoleMap");
            builder.HasKey(o => new {o.UserId,o.RoleId});
        }
    }
}
