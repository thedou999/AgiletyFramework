using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac.Extensions.DependencyInjection;
using Autofac;
using AgiletyFramework.BusinessServices;
using AgiletyFramework.IBusinessServices;
using Microsoft.Extensions.Hosting;
using Autofac.Extras.DynamicProxy;
using AgiletyFramework.Support.AutofacAop;

namespace AgiletyFramework.WebCore.AutofacExtend
{
    public static class AutofacExtensions
    {
        public static void AddAutofacExt(this ConfigureHostBuilder Host)
        {
            Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
            Host.ConfigureContainer<ContainerBuilder>( ContainerBuilder =>
            {
                ContainerBuilder.RegisterType<UserService>().As<IUserService>().EnableClassInterceptors();
                ContainerBuilder.RegisterType<LogService>().As<ILogService>();
                ContainerBuilder.RegisterType<CustomLogInterceptor>();
                ContainerBuilder.RegisterType<MenuService>().As<IMenuService>();
                ContainerBuilder.RegisterType<RoleService>().As<IRoleService>();
            });

        }
    }
}
