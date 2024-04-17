using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ModelDto;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthenticationWebApi.JwtServices
{
    public class JwtHSService : AbstractJwtService
    {
        public JwtHSService(IOptionsSnapshot<JwtOptions> jwtOptions, IMemoryCache memoryCache) : base(jwtOptions, memoryCache)
        {
        }

        public override string CreateAccessToken(UserDto userDto)
        {
            var userList = UserToClaim(userDto);
            var jwt = WriteToken(userList.ToArray(), TimeSpan.FromSeconds(3));
            return jwt;
        }

        public override string CreateRefreshToken(UserDto userDto)
        {
            var refreshTokenId = Guid.NewGuid().ToString();
            List<Claim> claims = new List<Claim>()
            {
                new Claim("refreshTokenId",refreshTokenId)
            };
            string jwt = WriteToken(claims.ToArray(), TimeSpan.FromDays(3));
            memoryCache.Set(refreshTokenId, userDto, DateTime.Now.AddDays(3));
            return jwt;
        }

        private string WriteToken(Claim[] claims,TimeSpan timeSpan)
        {
            SigningCredentials credentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Value.SigningKey)),
                SecurityAlgorithms.HmacSha256);

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                audience: jwtOptions.Value.Audience,
                issuer: jwtOptions.Value.Issuer,
                claims: claims,
                expires: DateTime.Now,
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

        }
    }
}
