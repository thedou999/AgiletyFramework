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
using ModelDto;

namespace AgiletyFramework.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = false,GroupName = nameof(ApiVersions.V1))]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class MenuController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IRoleService roleService;
        private readonly IMenuService menuService;
        private readonly ILogger<MenuController> logger;
        private readonly IMapper mapper;

        public MenuController(IMapper mapper, ILogger<MenuController> logger, IComponentContext componentContext)
        {
            this.mapper = mapper;
            this.logger = logger;
            this.menuService = componentContext.Resolve<IMenuService>();
            this.roleService = componentContext.Resolve<IRoleService>();
            this.userService = componentContext.Resolve<IUserService>();
        }


        [HttpPost]
        public async Task<ActionResult> AddMenuTestAsync()
        {
            MenuEntity user = new MenuEntity()
            {
                MenuText = "用户管理",
                MenuType = (int)MenuTypeEnum.Menu,
                Icon = "UserFilled",
                WebUrlName = "user",
                WebUrl = "/user",
                VueFilePath = "../views/user/index.vue",
                IsLeafNode = 0
            };
            MenuEntity role = new MenuEntity()
            {
                MenuText = "角色管理",
                MenuType = (int)MenuTypeEnum.Menu,
                Icon = "User",
                WebUrlName = "role",
                WebUrl = "/role",
                VueFilePath = "../views/role/index.vue",
                IsLeafNode = 0
            };
            MenuEntity menu = new MenuEntity()
            {
                MenuText = "菜单管理",
                MenuType = (int)MenuTypeEnum.Menu,
                Icon = "menu",
                WebUrlName = "menu",
                WebUrl = "/menu",
                VueFilePath = "../views/menu/index.vue",
                IsLeafNode = 0
            };
            MenuEntity log = new MenuEntity()
            {
                MenuText = "日志管理",
                MenuType = (int)MenuTypeEnum.Menu,
                Icon = "Document",
                WebUrlName = "log",
                WebUrl = "/log",
                VueFilePath = "../views/log/index.vue",
                IsLeafNode = 0
            };

            menuService.AddMenu(user);
            menuService.AddMenu(role);
            menuService.AddMenu(menu);
            menuService.AddMenu(log);

            var menus = menuService.GetAllMenuEntities();
            var menuDtos = mapper.Map<List<MenuEntity>, List<MenuDto>>(menus);

            return new JsonResult(new ApiDataResult<List<MenuDto>>()
            {
                Success = true,
                Message = "数据添加",
                Data = menuDtos
            });

//            const items = reactive<IMenu[]>([
//{
//            MenuText: '用户管理',
//    WebUrl: '/user',
//    Icon: 'UserFilled',
//    Child: []
//},
//{
//            MenuText: '菜单管理',
//    WebUrl: '/menu',
//    Icon: 'menu',
//    Child: []
//},
//{
//            MenuText: '日志管理',
//    WebUrl: '/log',
//    Icon: "Document",
//    Child: []
//},
//])

//            import userIndex from '../views/user/index.vue'
//import logIndex from '../views/log/index.vue'
//import menuIndex from '../views/menu/index.vue'
        }


        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> GetAllMenu()
        {
            var menus = menuService.GetAllMenuEntities();
            var menuDtos = mapper.Map<ICollection<MenuEntity>, ICollection<MenuDto>>(menus);

            return new JsonResult(new ApiDataResult<ICollection<MenuDto>>()
            {
                Success = true,
                Message = "接受数据",
                Data = menuDtos,
                OValue = null
            });

        }

        [HttpGet]
        public async Task<ActionResult> DeleteMenuTest()
        {
            var menuManagement =  menuService.Query<MenuEntity>(m => true);
            menuService.Delete<MenuEntity>(menuManagement);

            var role = mapper.Map<RoleEntity, RoleDto>(roleService.GetAllRoleEntity().FirstOrDefault());
            return new JsonResult(role);

        }



    }
}
