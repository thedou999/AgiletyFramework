
using AgiletyFramework.BusinessServices;
using AgiletyFramework.DBModels.DbContexts;
using AgiletyFramework.IBusinessServices;
using AgiletyFramework.WebCore.AuthenrizationExtend;
using AgiletyFramework.WebCore.AutoMapperExtend;
using AgiletyFramework.WebCore.CorsExtend;
using AgiletyFramework.WebCore.DESEncryptExtend;
using AgiletyFramework.WebCore.EFCoreExtend;
using AgiletyFramework.WebCore.SwaggerExtend;
using AuthenticationWebApi.JwtServices;

namespace AuthenticationWebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

            builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("JwtOptions"));

            builder.AddDESEncryptExt();

            builder.Services.AddMemoryCache();

            builder.Services.AddSwaggerExt("apiÎÄµµ","´ËÎªÃèÊö");

            builder.Services.AddAutoMapperExt();

            builder.Services.AddCorsExt();

            builder.AddDbContextExt<AgiletyDbContext>("default");

            builder.Services.AddAutoMapperExt();

            builder.Services.AddScoped<AbstractJwtService,JwtHSService>();
            builder.Services.AddScoped<IUserService, UserService>();

            builder.AddAuthorizationExt();

            var app = builder.Build();

            app.LoginApi();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerExt("");
            }

            app.UseCorsExt();

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            

            app.Run();
        }
    }
}
