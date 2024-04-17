using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using ModelDto;
using System.Security.Claims;

namespace AuthenticationWebApi.JwtServices
{
    public abstract class AbstractJwtService
    {
        protected readonly IOptionsSnapshot<JwtOptions> jwtOptions;
        protected readonly IMemoryCache memoryCache;

        protected AbstractJwtService(IOptionsSnapshot<JwtOptions> jwtOptions, IMemoryCache memoryCache)
        {
            this.jwtOptions = jwtOptions;
            this.memoryCache = memoryCache;
        }

        public abstract string CreateAccessToken(UserDto userDto);
        public abstract string CreateRefreshToken(UserDto userDto);
        public virtual List<Claim> UserToClaim(UserDto userDto)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim("Id",userDto.Id.ToString()),
                new Claim("Name",userDto.Name.ToString()),
                new Claim("Password",userDto.Password.ToString()),
                new Claim("UserType",userDto.UserType.ToString()),
                new Claim("Phone",userDto.Phone.ToString()),
                new Claim("Mobile",userDto.Mobile.ToString()),
                new Claim("Address",userDto.Address.ToString()),
                new Claim("Email",userDto.Email.ToString()),
                new Claim("WeChat",userDto.WeChat.ToString()),
                new Claim("QQ",userDto.QQ.ToString()),
                new Claim("Gender",userDto.Gender.ToString()),
                new Claim("Imageurl",userDto.Imageurl.ToString()),
                new Claim("LastLoginTime",userDto.LastLoginTime.ToString()),
                new Claim("IsEnable",userDto.IsEnable.ToString()),
                new Claim("CreateTime",userDto.CreateTime.ToString()),
                new Claim("ModifyTime",userDto.ModifyTime.ToString()),
                new Claim("Status",userDto.Status.ToString()),
            };
            return claims;
        } 


    }
}
