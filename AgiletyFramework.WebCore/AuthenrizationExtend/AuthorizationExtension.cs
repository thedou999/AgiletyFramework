using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Runtime.CompilerServices;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json;
using AgiletyFramework.Commons;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;

namespace AgiletyFramework.WebCore.AuthenrizationExtend
{
    public static class AuthorizationExtension
    {
        public static void AddAuthorizationExt(this WebApplicationBuilder builder)
        {
            JwtOptions jwtOptions = new JwtOptions();
            builder.Configuration.Bind("JwtOptions", jwtOptions);

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = InitTokenValidationParameters(jwtOptions);
                    options.Events = new JwtBearerEvents
                    {
                        OnChallenge = InitOnChallenge,
                        OnForbidden = InitOnForbidden,
                    };
                });
        }

        public static TokenValidationParameters InitTokenValidationParameters(JwtOptions jwtOptions)
        {
            return new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidAudience = jwtOptions.Audience,
                ValidIssuer = jwtOptions.Issuer,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SigningKey)),
                ClockSkew = TimeSpan.Zero,
                AudienceValidator = (m,n,z) => true,
                LifetimeValidator = (notBefore,expires,securityToken,validationParameters) => true
            };
        }

        public static Task InitOnChallenge(JwtBearerChallengeContext context)
        {
            context.HandleResponse();
            var payload = JsonSerializer.Serialize(new ApiDataResult<int>()
            {
                Success = false,
                Message = "token错误，鉴权未通过",
                Data = 0,
                OValue = 401
            });

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status200OK;
            context.Response.WriteAsync(payload);
            return Task.CompletedTask;
        }

        public static Task InitOnForbidden(ForbiddenContext context)
        {
            var payload = JsonSerializer.Serialize(new ApiDataResult<int>()
            {
                Success = false,
                Message = "未达到权限等级，鉴权未通过",
                Data = 0,
                OValue = 403
            });

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status200OK;
            context.Response.WriteAsync(payload);
            return Task.CompletedTask;
        }
    }
}
