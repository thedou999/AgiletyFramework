using AgiletyFramework.Commons;
using AgiletyFramework.DBModels.Entities;
using AgiletyFramework.IBusinessServices;
using ModelDto;
using AgiletyFramework.WebCore;
using Autofac.Core;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelDto;
using System.Text.Json;
using EasyDESEncrypt;
using Autofac;
using AgiletyFramework.BusinessServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Claims;

namespace AgiletyFramework.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = false,GroupName = nameof(ApiVersions.V1))]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> logger;
        private readonly IUserService userService;
        private readonly IRoleService roleService;
        private readonly IMapper mapper;
        public UserController(ILogger<UserController> logger, IComponentContext componentContext, IMapper mapper )
        {
            this.logger = logger;
            this.userService = componentContext.Resolve<IUserService>();
            this.roleService = componentContext.Resolve<IRoleService>();
            this.mapper = mapper;

            Console.WriteLine(logger.ToString());
            Console.WriteLine(userService.ToString());
            Console.WriteLine(mapper.ToString());
        }


        [HttpGet]
        public async Task<ActionResult<ApiDataResult<PagingData<UserDto>>>> GetUserPageAsync(int pageindex,int pageSize,string? searchString = null)
        {
            PagingData<UserEntity> pageDataUserEntity;

            if (string.IsNullOrEmpty(searchString))
            {
                pageDataUserEntity = userService.QueryPage<UserEntity, int>(o => true, pageSize, pageindex, o => o.Id, false);
            }
            else
            {
                pageDataUserEntity = userService.QueryPage<UserEntity, int>(o => o.Name.Contains(searchString), pageSize, pageindex, o => o.Id, false);
                pageDataUserEntity.SearchString = searchString;
            }


            if(pageDataUserEntity == null)
            {
                return new JsonResult(ApiDataResult<PagingData<UserDto>>.NotFoundedResult());
            }

            var  pageDataUserDto = mapper.Map<PagingData<UserEntity>, PagingData<UserDto>>(pageDataUserEntity);
            if(pageDataUserEntity == null)
            {
                return new JsonResult(ApiDataResult<PagingData<UserDto>>.NotFoundedResult());
            }
            
            return new JsonResult(new ApiDataResult<PagingData<UserDto>>()
            {
                Success=true,
                Message="数据发送",
                Data=pageDataUserDto,
                OValue=null
            });
        }


        [HttpPost]
        public async Task<ActionResult<ApiDataResult<UserDto>>> AddUser([FromBody]AddUserDto addUserDto)
        {
            var result = new ApiDataResult<UserDto>();

            var samenameUser = userService.Query<UserEntity>(u => u.Name.Equals(addUserDto.Name)).FirstOrDefault();
            if(samenameUser != null)
            {
                result.Success = false;
                result.Message = "用户名重复";
                result.Data = null;
                result.OValue = null;
                return new JsonResult(result);
            }

            UserEntity newUser = mapper.Map<AddUserDto, UserEntity>(addUserDto);

            if(newUser.UserType == (int)UserTypeEnum.Administrator)
            {
                var AdministratorRole = roleService.Query<RoleEntity>(r => r.RoleName.Equals("Administrator")).FirstOrDefault();
                userService.AddUser(newUser, AdministratorRole);
            }
            else if(newUser.UserType == (int)UserTypeEnum.GeneralUser)
            {
                var GeneralUserRole = roleService.Query<RoleEntity>(r => r.RoleName.Equals("GeneralUser")).FirstOrDefault();
                userService.AddUser(newUser, GeneralUserRole);
            }

            var userDto = mapper.Map<UserEntity, UserDto>(newUser);

            if(userDto.Id >= 0)
            {
                result.Success = true;result.Message = "成功";result.Data = userDto;result.OValue = null;
                return new JsonResult(result);
            }
            else
            {
                result.Success = false;result.Message = "失败";result.Data = userDto;result.OValue = null;
                return new JsonResult(result);
            }
        }


        [HttpPost]
        public async Task<ActionResult<ApiDataResult<ICollection<UserDto>>>> GetAllUser()
        {
            var result = new ApiDataResult<ICollection<UserDto>>();

            var userList = userService.GetAllUser();
            if(userList == null)
            {
                return ApiDataResult<ICollection<UserDto>>.NotFoundedResult();
            }

            var userDtoList = mapper.Map<ICollection<UserEntity>, ICollection<UserDto>>(userList);
            if(userList == null)
            {
                return ApiDataResult<ICollection<UserDto>>.NotFoundedResult();
            }


            return new JsonResult(new ApiDataResult<ICollection<UserDto>>()
            {
                Success = true,
                Message = "获得所有用户",
                Data = userDtoList,
                OValue = null
            });
        }

        [HttpGet]
        public async Task<ActionResult> GetUserMenus(string userName)
        {
            var user = userService.GetAllUser().Where(u => u.Name.Equals(userName)).FirstOrDefault();
            if(user == null)
            {
                return new JsonResult(new ApiResult() { Success = false, Message = "没有该用户" });
            }

            var menuDtos = mapper.Map<ICollection<MenuEntity>, ICollection<MenuDto>>(user.Roles.FirstOrDefault().Menus);
            return new JsonResult(new ApiDataResult<ICollection<MenuDto>>()
            {
                Success = true,
                Message = "接收数据",
                Data = menuDtos,
                OValue = null
            });
        }

        [HttpGet]
        public async Task<IActionResult> AuthenrizationTest()
        {
            string user;
            try
            {
                user = this.HttpContext.User.ToString();
            }catch(Exception e)
            {
                return new JsonResult("false");
            }

            return new JsonResult(user);
        }

    }
}
