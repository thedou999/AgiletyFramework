using AgiletyFramework.DBModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelDto
{
    public class RoleDto
    {
        public int Id { get; set; }
        public string? RoleName { get; set; }

        public ICollection<MenuDto> Menus { get; set; } = [];
    }
}
