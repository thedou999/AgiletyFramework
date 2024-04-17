using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AgiletyFramework.WebCore.SwaggerExtend
{
    public static class SwaggerExtensions
    {
        public static void AddSwaggerExt(this IServiceCollection services,string? title,string? description)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(option =>
            {
                foreach (var version in typeof(ApiVersions).GetEnumNames())
                {
                    //配置多个swagger文档
                    option.SwaggerDoc(version, new OpenApiInfo()
                    {
                        Title = !string.IsNullOrEmpty(title) ? title : $"title:敏捷开发api文档",
                        Version = $"version:{version}",
                        Description = !string.IsNullOrEmpty(description) ? description : $"description:{description}"
                    });
                }

                var file = Path.Combine(AppContext.BaseDirectory,
                    $"{AppDomain.CurrentDomain.FriendlyName}.xml");
                option.IncludeXmlComments(file, true);
                option.OrderActionsBy(o => o.RelativePath);

                //支持jwt
                #region
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description = "请输入token，格式为 Bearer xxxxxxxxxx",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme()
                        {
                           Reference = new OpenApiReference()
                           {
                               Type = ReferenceType.SecurityScheme,
                               Id="Bearer"
                           }
                        },
                        new string[]{}
                    }
                }
                );
                #endregion
            });
        }

        public static void UseSwaggerExt(this WebApplication app, string? versionDocName)
        {
            app.UseSwagger();
            app.UseSwaggerUI(option =>
            {
                foreach (var version in typeof(ApiVersions).GetEnumNames())
                {
                    //配置swagger路径和右上角的api文档名称
                    option.SwaggerEndpoint($"/swagger/{version}/swagger.json",
                       !string.IsNullOrEmpty(versionDocName) ? versionDocName : $"api文档{version}");
                }

            });
        }


    }
}
