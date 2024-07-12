using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace backend.classes
{
    public class JwtService
    {
        private readonly JwtOptions jwtOptions;
        public JwtService(IOptions<JwtOptions> jwtOptions)
        {
            this.jwtOptions = jwtOptions.Value;
        }
        public string GenerateToken(string documentNumber, string fullName)
        {
            var claims = new []
            {
                new Claim(ClaimTypes.Name, documentNumber),
                new Claim(ClaimTypes.Role, fullName)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SigningKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                jwtOptions.Issuer,
                jwtOptions.Audience,
                claims,
                expires: DateTime.Now.AddSeconds(jwtOptions.ExpirationInSeconds),
                signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}