using AgiletyFramework.DBModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelDto
{
    public class MenuDto
    {
        public Guid Id { get; set; }
        public string? MenuText { get; set; }
        public int? MenuType { get; set; }
        public string? Icon { get; set; }
        public string? WebUrlName { get; set; }
        public string? WebUrl { get; set; }
        public string? VueFilePath { get; set; }
        public byte? IsLeafNode { get; set; }
        public Object Component { get; set; } = null;

        public ICollection<MenuDto>? Children { get; set; } = [];

    }
}
