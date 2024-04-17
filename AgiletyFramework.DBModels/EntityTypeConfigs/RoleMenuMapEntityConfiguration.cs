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
    public class RoleMenuMapEntityConfiguration : IEntityTypeConfiguration<RoleMenuMapEntity>
    {
        public void Configure(EntityTypeBuilder<RoleMenuMapEntity> builder)
        {
            builder.ToTable("RoleMenuMap");
            builder.HasKey(o => new {o.RoleId,o.MenuId});
        }
    }
}
