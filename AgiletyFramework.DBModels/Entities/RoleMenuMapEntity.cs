using AgiletyFramework.DBModels.EntityTypeConfigs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgiletyFramework.DBModels.Entities
{
    [EntityTypeConfiguration(typeof(RoleMenuMapEntityConfiguration))]
    public class RoleMenuMapEntity : BaseModel
    {
        public int RoleId { get; set; }
        public RoleEntity Role { get; set; }

        public Guid MenuId { get; set; }
        public MenuEntity Menu { get; set; }
    }
}
