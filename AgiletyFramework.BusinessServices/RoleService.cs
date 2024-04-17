using AgiletyFramework.DBModels.Entities;
using AgiletyFramework.IBusinessServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgiletyFramework.BusinessServices
{
    public class RoleService :  BaseService,IRoleService
    {
        public RoleService(DbContext dbContext) : base(dbContext)
        {

        }

        public RoleEntity AddRoleEntity(RoleEntity roleEntity)
        {
            return Insert<RoleEntity>(roleEntity);
        }

        public List<RoleEntity> GetAllRoleEntity()
        {
            return Query<RoleEntity>(r => true).Include(r => r.Menus).ToList();
        }

        public RoleEntity? GetRoleByRoleName(string roleName)
        {
            return Query<RoleEntity>(r => r.RoleName.Equals(roleName)).Include(r => r.Menus).FirstOrDefault();
        }

        public List<RoleEntity> GetRoleTreeByUserId(int userId)
        {
            return Query<UserEntity>(u => u.Id == userId).Include(u => u.Roles).ThenInclude(r => r.Menus)
                .Select<UserEntity, ICollection<RoleEntity>>(u => u.Roles).FirstOrDefault().ToList();
        }
    }
}
