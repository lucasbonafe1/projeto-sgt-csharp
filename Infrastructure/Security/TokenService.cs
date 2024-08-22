using Microsoft.IdentityModel.Tokens;
using SGT.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SGT.Infrastructure.Security
{
    public class TokenService
    {
        public static object GenerateToken(UserEntity user)
        {
            var key = Encoding.ASCII.GetBytes(Key.Secret); // TODO: Rever melhor forma de uso pro meu caso
            var tokenConfig = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("userId", user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenConfig);
            var tokenString  = tokenHandler.WriteToken(token);

            return new
            {
                token = tokenString
            };
        }
    }
}
