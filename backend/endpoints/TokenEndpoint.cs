using backend.classes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace backend.endpoints
{
    public static class TokenEndpoint
    {
        public static async Task<IActionResult> Connect(
            HttpContext ctx,
            JwtOptions jwtOptions)
        {
            // Check for JSON content type
            if (!ctx.Request.ContentType.Equals("application/json", StringComparison.OrdinalIgnoreCase))
                return new BadRequestObjectResult(new { Error = "Invalid Request on Content Type" });

            // Read the JSON body
            var requestBody = await new StreamReader(ctx.Request.Body).ReadToEndAsync();
            var loginRequest = JsonSerializer.Deserialize<LoginRequest>(requestBody);

            if (loginRequest == null)
                return new BadRequestObjectResult(new { Error = "Invalid JSON" });

            if (string.IsNullOrEmpty(loginRequest.grant_type))
                return new BadRequestObjectResult(new { Error = "Invalid Request on Grant Type" });

            if (string.IsNullOrEmpty(loginRequest.username))
                return new BadRequestObjectResult(new { Error = "Invalid Request on Username" });

            if (string.IsNullOrEmpty(loginRequest.password))
                return new BadRequestObjectResult(new { Error = "Invalid Request on Password" });

            var tokenExpiration = TimeSpan.FromSeconds(jwtOptions.ExpirationInSeconds);
            var accessToken = CreateAccessToken(
                jwtOptions,
                loginRequest.username,
                TimeSpan.FromSeconds(jwtOptions.ExpirationInSeconds),
                new[] { "read_todo", "create_todo" });
            
            return new OkObjectResult(new
            {
                access_token = accessToken,
                expiration = (int)tokenExpiration.TotalMinutes,
                type = "bearer"
            });
        }

        private static string CreateAccessToken(
            JwtOptions jwtOptions,
            string username,
            TimeSpan expiration,
            string[] permissions)
        {
            var keyBytes = Encoding.ASCII.GetBytes(jwtOptions.SigningKey);
            var symmetricKey = new SymmetricSecurityKey(keyBytes);

            var signingCredentials = new SigningCredentials(
                symmetricKey,
                SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>()
            {
                new Claim("sub", username),
                new Claim("name", username),
                new Claim("aud", jwtOptions.Audience)
            };

            var roleClaims = permissions.Select(x => new Claim("role", x));
            claims.AddRange(roleClaims);

            var token = new JwtSecurityToken(
                issuer: jwtOptions.Issuer,
                audience: jwtOptions.Audience,
                claims: claims,
                expires: DateTime.Now.Add(expiration),
                signingCredentials: signingCredentials);

            var rawToken = new JwtSecurityTokenHandler().WriteToken(token);
            return rawToken;
        }

        private class LoginRequest
        {
            public string grant_type { get; set; }
            public string username { get; set; }
            public string password { get; set; }
        }
    }
}
