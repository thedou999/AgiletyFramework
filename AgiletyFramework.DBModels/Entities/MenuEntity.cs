using AgiletyFramework.DBModels.EntityTypeConfigs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgiletyFramework.DBModels.Entities
{
    [EntityTypeConfiguration(typeof(MenuEntityConfiguration))]
    public class MenuEntity : BaseModel
    {
        public Guid Id { get; set; }
        public string? MenuText { get; set; }
        public int? MenuType { get; set; }
        public string? Icon { get; set; }
        public string? WebUrlName { get; set; }
        public string? WebUrl { get; set; }
        public string? VueFilePath { get; set; }
        public byte? IsLeafNode { get; set; }

        public MenuEntity? Parent { get; set; } 
        public Guid? ParentId { get; set; }
        public ICollection<MenuEntity> Children { get; set; } = [];

        public ICollection<RoleEntity> Roles { get; set; } = [];
        public ICollection<RoleMenuMapEntity> RoleMenuMaps { get; set; } = [];
    }
}
