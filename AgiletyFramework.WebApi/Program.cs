using AgiletyFramework.WebCore;
using Microsoft.OpenApi.Models;
using AgiletyFramework.WebCore.SwaggerExtend;
using AgiletyFramework.WebCore.CorsExtend;
using AgiletyFramework.DBModels;
using AgiletyFramework.DBModels.DbContexts;
using Microsoft.EntityFrameworkCore;
using AgiletyFramework.WebCore.EFCoreExtend;
using AgiletyFramework.WebCore.WebApplicationBuilderExtend;
using AgiletyFramework.IBusinessServices;
using AgiletyFramework.BusinessServices;
using Autofac.Extensions.DependencyInjection;
using Autofac;
using AgiletyFramework.WebCore.AutofacExtend;
using System.Dynamic;
using AgiletyFramework.WebCore.AutoMapperExtend;
using AgiletyFramework.WebCore.DESEncryptExtend;
using AgiletyFramework.WebCore.DownloadFileExtend;
using AgiletyFramework.Commons;
using AgiletyFramework.WebCore.AuthenrizationExtend;

namespace AgiletyFramework.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.SetBuilder();

            builder.AddDESEncryptExt();
            builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("JwtOptions"));

            builder.Services.AddSwaggerExt("apiÎÄµµ","´ËÎªÃèÊö");
            builder.AddDbContextExt<AgiletyDbContext>("default");
            builder.Logging.AddLog4Net("configs/log4net.config");
            builder.Services.AddCorsExt();

            #region Autofac
            builder.Host.AddAutofacExt();
            #endregion
            #region IOC
            //builder.Services.AddTransient<IUserService,UserService>();
            //builder.Services.AddTransient<IMenuService,MenuService>();
            #endregion

            builder.Services.AddAutoMapperExt();


            builder.AddAuthorizationExt();

            var app = builder.Build();

            //app.UseMiddleware<DownloadImagesMiddleware>(Directory.GetCurrentDirectory());

            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerExt("");
            }
            app.UseCorsExt();

            app.UseAuthentication();
            app.UseAuthorization();

            

            app.MapControllers();
            app.Run();
        }
    }
}
