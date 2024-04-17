using AgiletyFramework.DBModels.EntityTypeConfigs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgiletyFramework.DBModels.Entities
{
    [EntityTypeConfiguration(typeof(UserEntityConfiguration))]
    public class UserEntity : BaseModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Password { get; set; }
        public int? UserType { get; set; }
        public string? Phone { get; set; }
        public string? Mobile { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public string? WeChat { get; set; }
        public string? QQ { get; set; }
        public int Gender { get; set; }
        public string? Imageurl { get; set; }
        public DateTime LastLoginTime { get; set; }

        public ICollection<RoleEntity> Roles { get; set; } = [];
        public ICollection<UserRoleMapEntity> UserRoleMaps { get; set; } = [];

    }
}
