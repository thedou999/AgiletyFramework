using AgiletyFramework.DBModels.Entities;
using AgiletyFramework.DBModels.EntityTypeConfigs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AgiletyFramework.DBModels.DbContexts
{
    public class AgiletyDbContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<RoleEntity> Roles { get; set; }
        public DbSet<MenuEntity> Menus { get; set; }
        public DbSet<UserRoleMapEntity> UserRoleMaps { get; init; }
        public DbSet<RoleMenuMapEntity> RoleMenuMaps { get; init; }
        public DbSet<SystemLog> SystemLogs { get; set; }

        public AgiletyDbContext(DbContextOptions<AgiletyDbContext> options) : base(options)
        {
        
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserEntityConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(RoleEntityConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MenuEntityConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserRoleMapEntityConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(RoleMenuMapEntityConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SystemLogConfiguration).Assembly);
        }


    }
}
