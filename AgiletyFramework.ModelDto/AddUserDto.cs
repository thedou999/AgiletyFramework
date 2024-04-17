using AgiletyFramework.DBModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelDto
{
    public class AddUserDto
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public int UserType { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string WeChat { get; set; }
        public string QQ { get; set; }
        public int Gender { get; set; }
        public string Imageurl { get; set; }
        public bool IsEnabled { get; set; }
    }
}
