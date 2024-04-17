using Castle.Core.Logging;
using Castle.DynamicProxy;
using log4net.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgiletyFramework.Support.AutofacAop
{
    public class CustomLogInterceptor : IInterceptor
    {
        private readonly ILogger<CustomLogInterceptor> logger;

        public CustomLogInterceptor(ILogger<CustomLogInterceptor> logger)
        {
            this.logger = logger;
        }

        /// <summary>
        /// aop实现
        /// </summary>
        /// <param name="invocation"></param>
        public void Intercept(IInvocation invocation)
        {
            logger.LogInformation("before invocation");
            invocation.Proceed();
            logger.LogInformation("after invocation");
        }
    }
}
