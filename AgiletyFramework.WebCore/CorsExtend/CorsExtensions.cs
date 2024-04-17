using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AgiletyFramework.WebCore.CorsExtend
{
    public static class CorsExtensions
    {
        public static void AddCorsExt(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("default", builder =>
                {
                    builder.AllowAnyHeader()
                    .AllowAnyOrigin()
                    .AllowAnyMethod();
                });
            });
        }

        public static void UseCorsExt(this WebApplication app)
        {
            app.UseCors("default");
        }


    }
}
