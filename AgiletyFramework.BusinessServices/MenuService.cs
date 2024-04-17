using AgiletyFramework.DBModels.Entities;
using AgiletyFramework.IBusinessServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgiletyFramework.BusinessServices
{
    public class MenuService : BaseService, IMenuService
    {
        public MenuService(DbContext dbContext) : base(dbContext)
        {
        }

        public MenuEntity AddMenu(MenuEntity menuEntity)
        {
            return Insert<MenuEntity>(menuEntity);
        }

        public List<MenuEntity> GetAllMenuEntities()
        {
            return Query<MenuEntity>(m => true).ToList();
        }

        public MenuEntity? GetMenuByMenuText(string menuText)
        {
            return Query<MenuEntity>(m => m.MenuText.Equals(menuText)).FirstOrDefault();
        }

        public List<MenuEntity> GetMenuTreeByUserId(int userId)
        {
            List<MenuEntity> menuEntities = new List<MenuEntity>();
            foreach (var role in Query<UserEntity>(u => u.Id == userId).Select(u => u.Roles).FirstOrDefault())
            {
                menuEntities.AddRange(role.Menus);
            }

            return menuEntities;
        }
    }
}
