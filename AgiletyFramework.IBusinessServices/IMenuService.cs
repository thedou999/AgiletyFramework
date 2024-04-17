using AgiletyFramework.DBModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgiletyFramework.IBusinessServices
{
    public interface IMenuService : IBaseService
    {
        public List<MenuEntity> GetAllMenuEntities();
        public MenuEntity AddMenu(MenuEntity menuEntity);
        public MenuEntity GetMenuByMenuText(string menuText);
        public List<MenuEntity> GetMenuTreeByUserId(int userId);
    }
}
