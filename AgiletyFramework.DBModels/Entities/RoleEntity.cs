using AgiletyFramework.DBModels.EntityTypeConfigs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgiletyFramework.DBModels.Entities
{
    [EntityTypeConfiguration(typeof(RoleEntityConfiguration))]
    public class RoleEntity : BaseModel
    {
        public int Id { get; set; }
        public string? RoleName { get; set; }

        public ICollection<UserEntity> Users { get; set; } = [];
        public ICollection<UserRoleMapEntity> UserRoleMaps { get; set; } = [];
        public ICollection<MenuEntity> Menus { get; set; } = [];
        public ICollection<RoleMenuMapEntity> RoleMenuMaps { get; set; } = [];
    }
}
