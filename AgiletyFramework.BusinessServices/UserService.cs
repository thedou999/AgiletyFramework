using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AgiletyFramework.DBModels.Entities;
using AgiletyFramework.IBusinessServices;
using EasyDESEncrypt;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace AgiletyFramework.BusinessServices
{
    public class UserService : BaseService, IUserService
    {
        public UserService(DbContext dbContext) : base(dbContext)
        {
        }

        public UserEntity AddUser(UserEntity newUser)
        {
            return Insert<UserEntity>(newUser);
        }

        public UserEntity AddUser(UserEntity newUser, RoleEntity role)
        {
            newUser.Roles.Add(role);
            return Insert<UserEntity>(newUser);
        }

        public ICollection<UserEntity> GetAllUser()
        {
            return dbContext.Set<UserEntity>().Include(u => u.Roles).ThenInclude(r => r.Menus).ToList();
        }

        public UserEntity? Login(string username,string password)
        {
            if(username.IsNullOrEmpty() || password.IsNullOrEmpty())
            {
                return null;
            }

            password = DESEncryptContext.DESEncrypt(password);

            UserEntity? user =  Query<UserEntity>(u => u.Name == username && u.Password == password).FirstOrDefault();

            return user;
        }

    }
}
