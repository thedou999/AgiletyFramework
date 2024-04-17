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
    public class MenuEntityConfiguration : IEntityTypeConfiguration<MenuEntity>
    {
        public void Configure(EntityTypeBuilder<MenuEntity> builder)
        {
            builder.ToTable("Menu");
            builder.HasKey(o => o.Id);
            builder.HasMany(o => o.Roles).WithMany(o => o.Menus);
            builder.HasOne<MenuEntity>(o => o.Parent).WithMany(o => o.Children).HasForeignKey(o => o.ParentId);
            
        }
    }
}
