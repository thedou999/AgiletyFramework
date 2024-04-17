using AgiletyFramework.Commons;
using AgiletyFramework.DBModels.Entities;
using AgiletyFramework.IBusinessServices;
using AgiletyFramework.WebCore;
using Autofac;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace AgiletyFramework.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi =false,GroupName =nameof(ApiVersions.V1))]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class RoleController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IRoleService roleService;
        private readonly IMenuService menuService;
        private readonly IMapper mapper;
        private readonly ILogger<RoleController> logger;

        public RoleController(ILogger<RoleController> logger, IMapper mapper, IComponentContext componentContext)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.userService = componentContext.Resolve<IUserService>();
            this.menuService = componentContext.Resolve<IMenuService>();
            this.roleService = componentContext.Resolve<IRoleService>();
        }

        [HttpPost]
        public async Task<ActionResult> AddRoleTestAsync()
        {
            RoleEntity newRole1 = new RoleEntity()
            {
                RoleName = "GeneralUser"
            };
            RoleEntity newRole2= new RoleEntity()
            {
                RoleName = "Administrator"
            };

            var roleFromDb1 = roleService.AddRoleEntity(newRole1);
            var roleFromDb2 = roleService.AddRoleEntity(newRole2);

            if(roleFromDb1.Id<0 || roleFromDb2.Id < 0)
            {
                return new JsonResult(new ApiResult() { Success = false, Message = "添加失败" });
            }
            else
            {
                return new JsonResult(new ApiResult() { Success = true, Message = "添加成功" });
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetAllRoleWithMenuTextAsync()
        {
            var texts = new List<string>();

            var dbContext = roleService.GetDbContext();
            var roles = dbContext.Set<RoleEntity>().Include(r => r.RoleMenuMaps).Include(r => r.Menus).ToList();
            foreach (var role in roles)
            {
                foreach (var menu in role.Menus)
                {
                    texts.Add($"{role.RoleName} {menu.MenuText}");
                }

            }
            return new JsonResult(texts);
        }

        [HttpPost]
        public async Task<ActionResult> AddMenuToRoleTestAsync()
        {
            var dbContext = roleService.GetDbContext();

            try
            {
                var roles = roleService.GetAllRoleEntity();
                var menus = menuService.GetAllMenuEntities();

                roles.ForEach(role =>
                {
                    foreach (var menu in menus)
                    {
                        role.Menus.Add(menu);
                    }
                });

                var result = dbContext.SaveChanges();
                roleService.Commit();

                if(result <=0)
                {
                    throw new Exception("error");
                }
            }catch(Exception e)
            {
                return new JsonResult(new ApiResult()
                {
                    Success = false,
                    Message = "添加失败"
                });
            }

            return new JsonResult(new ApiResult()
            {
                Success = true,
                Message = "添加成功"
            });
        }


    }
}
