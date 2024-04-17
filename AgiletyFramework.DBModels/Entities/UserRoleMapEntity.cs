using AgiletyFramework.DBModels.EntityTypeConfigs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgiletyFramework.DBModels.Entities
{
    [EntityTypeConfiguration(typeof(UserRoleMapEntityConfiguration))]
    public class UserRoleMapEntity : BaseModel
    {
        public int UserId { get; set; }
        public UserEntity User { get; set; }

        public int RoleId { get; set; }
        public RoleEntity Role { get; set; }
    }
}
