using AgiletyFramework.Commons;
using AgiletyFramework.DBModels.DbContexts;
using AgiletyFramework.DBModels.Entities;
using AgiletyFramework.IBusinessServices;
using ModelDto;
using AgiletyFramework.WebCore;
using AgiletyFramework.WebCore.Request;
using Autofac;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Validations;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace AgiletyFramework.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = false,GroupName = nameof(ApiVersions.V1))]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TestController : ControllerBase
    {
        private readonly ILogger<TestController> logger;
        private readonly IUserService userService;
        private readonly IMapper mapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="componentContext"></param>
        /// <param name="mapper"></param>
        public TestController(ILogger<TestController> logger, IComponentContext componentContext, IMapper mapper)
        {
            this.logger = logger;
            this.userService = componentContext.Resolve<IUserService>();
            this.mapper = mapper;
        }


        
        [HttpGet]
        /// <summary>
        /// 测试
        /// </summary>
        /// <returns></returns>
        public ActionResult DbContextTest()
        {
            var user = userService.Query<UserEntity>(u => u.Name == "tempName").First();
            var userDto = mapper.Map<UserEntity, UserDto>(user);

            return new JsonResult(new ApiDataResult<UserDto>()
            {
                Success = true,
                Message = "userDto",
                Data = userDto,
                OValue = null
            });
        }



    }
}
