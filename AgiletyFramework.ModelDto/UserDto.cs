using AgiletyFramework.DBModels.Entities;

namespace ModelDto
{
    public class UserDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Password { get; set; }
        public int UserType { get; set; }
        public string? Phone { get; set; }
        public string? Mobile { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public string? WeChat { get; set; }
        public string? QQ { get; set; }
        public int Gender { get; set; }
        public string? Imageurl { get; set; }
        public DateTime LastLoginTime { get; set; }
        public bool IsEnable { get; set; }
        public DateTime CreateTime { get; set; } = DateTime.Now;
        public DateTime ModifyTime { get; set; }
        public int Status { get; set; }
    }
}
