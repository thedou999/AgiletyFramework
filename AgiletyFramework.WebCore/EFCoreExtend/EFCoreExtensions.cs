using AgiletyFramework.DBModels.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgiletyFramework.WebCore.WebApplicationBuilderExtend;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Builder;

namespace AgiletyFramework.WebCore.EFCoreExtend
{
    public static class EFCoreExtensions
    {
        public static IServiceCollection AddDbContextExt<T>(this WebApplicationBuilder builder,string ConnectionAlias) where T : DbContext
        {
            string? ConnectionString = builder.Configuration.GetConnectionString("default");

            if (ConnectionString == null)
            {
                throw new NullReferenceException();
                return builder.Services;
            }

            builder.Services.AddDbContext<DbContext, T>(builder =>
            {
                builder.UseSqlServer(ConnectionString);
            });
            return builder.Services;

        }
        

    }
}
