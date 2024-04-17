using Autofac.Extras.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using AgiletyFramework.Support.AutofacAop;
using AgiletyFramework.DBModels.Entities;
using System.Linq.Expressions;


namespace AgiletyFramework.IBusinessServices
{

    [Intercept(typeof(CustomLogInterceptor))]
    public interface IUserService : IBaseService
    {
        public ICollection<UserEntity> GetAllUser();
        public UserEntity AddUser(UserEntity newUser);
        public UserEntity AddUser(UserEntity newUser, RoleEntity role);
        public UserEntity Login(string username, string password);
    }
}
