using MicroondasMVC_Benner.Models.API;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MicroondasMVC_Benner.Repository.Token
{
    public class TokenService
    {
        public static string GenerateToken(UserAuthModel userModel, string secretKey)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, userModel.User ?? ""),
                    new Claim(ClaimTypes.NameIdentifier, userModel.Id.ToString() ?? "")
                }),
                Expires = DateTime.UtcNow.AddHours(2), 
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
