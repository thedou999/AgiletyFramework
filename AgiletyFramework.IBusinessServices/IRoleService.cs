using AgiletyFramework.DBModels.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AgiletyFramework.IBusinessServices
{
    public interface IRoleService : IBaseService
    {
        public List<RoleEntity> GetAllRoleEntity();
        public RoleEntity AddRoleEntity(RoleEntity roleEntity);
        public RoleEntity? GetRoleByRoleName(string roleName);
        public List<RoleEntity> GetRoleTreeByUserId(int userId);

    }
}
