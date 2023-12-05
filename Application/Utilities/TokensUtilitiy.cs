using Domain.User;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Utilities
{
    public class TokensUtilitiy : ITokenUtility
    {
        private readonly IConfiguration _configuration;

        public TokensUtilitiy()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            _configuration = builder.Build();
        }

        public string GenerateJwtToken(User user)
        {
            var secretKey = _configuration["Authentication:SecretKey"];
            var audience = _configuration["Authentication:Audience"];

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);
            var claimsForToken = new List<Claim>();
            claimsForToken.Add(new Claim("UserID", user.Id.ToString()));
            claimsForToken.Add(new Claim("UserName", user.UserName));
            claimsForToken.Add(new Claim("Role", user.Role.ToString()));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new[]
                    {
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim(ClaimTypes.Role, user.Role.ToString()),
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                    }
                ),
                Expires = DateTime.UtcNow.AddHours(6),
                Audience = audience,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
