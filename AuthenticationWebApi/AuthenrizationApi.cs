using AgiletyFramework.Commons;
using AgiletyFramework.DBModels.Entities;
using AgiletyFramework.IBusinessServices;
using AgiletyFramework.WebCore;
using AuthenticationWebApi.JwtServices;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using ModelDto;
using System.Runtime.CompilerServices;

namespace AuthenticationWebApi
{
    public static class AuthenrizationApi
    {
        public static void LoginApi(this WebApplication app)
        {
            app.MapPost("auth/Account",
                (
                [FromServices] IUserService userService,
                [FromServices] AbstractJwtService jwtService,
                [FromServices] IMapper mapper,
                string username, string password
                    ) =>
            {
                var user = userService.Login(username, password);
                if (user == null)
                {
                    return new ApiResult()
                    {
                        Success = false,
                        Message = "用户名或密码错误"
                    };
                }
                var userDto = mapper.Map<UserEntity, UserDto>(user);

                var accessToken = jwtService.CreateAccessToken(userDto);
                var refreshToken = jwtService.CreateRefreshToken(userDto);

                return new ApiDataResult<object>()
                {
                    Success = true,
                    Message = "登录成功，请接收token",
                    Data = new
                    {
                        accessToken = accessToken,
                        refreshToken = refreshToken,
                        user = userDto
                    },
                    OValue = null
                };
            }).WithGroupName(ApiVersions.V1.ToString()).WithDescription("登录");

            app.MapGet("auth/Account",
                [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
            (
                [FromServices] IUserService userService,
                [FromServices] AbstractJwtService jwtService,
                [FromServices] IMapper mapper,
                [FromServices] IMemoryCache memoryCache,
                HttpContext context
                    ) =>
            {

                var refreshTokenId = "";
                try
                {
                    refreshTokenId = context.User.FindFirst("refreshTokenId").Value;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                if (string.IsNullOrWhiteSpace(refreshTokenId))
                {
                    return new ApiResult()
                    {
                        Success = false,
                        Message = "刷新token不存在或失效，请重新登录"
                    };
                }

                var userFromToken = memoryCache.Get<UserDto>(refreshTokenId);

                if (userFromToken == null)
                {
                    return new ApiResult()
                    {
                        Success = false,
                        Message = "找不到用户，请重新登录"
                    };
                }

                var userFromDb = userService.Login(userFromToken.Name, userFromToken.Password);
                if (userFromDb == null)
                {
                    return new ApiResult()
                    {
                        Success = false,
                        Message = "登录信息已失效，请重新登录",
                    };
                }

                var userDto = mapper.Map<UserEntity, UserDto>(userFromDb);
                var accessToken = jwtService.CreateAccessToken(userDto);
                var refreshToken = jwtService.CreateRefreshToken(userDto);

                return new ApiDataResult<object>()
                {
                    Success = true,
                    Message = "无感刷新成功",
                    Data = new
                    {
                        accessToken = accessToken,
                        refreshToken = refreshToken,
                        user = userDto
                    },
                    OValue = null
                };
            }).WithGroupName(ApiVersions.V1.ToString()).WithDescription("鉴权");
        }
    }
}
