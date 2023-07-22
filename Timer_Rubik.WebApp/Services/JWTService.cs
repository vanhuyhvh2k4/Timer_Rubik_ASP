using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Timer_Rubik.WebApp.Interfaces;

namespace Timer_Rubik.WebApp.Services
{
    public class JWTService : IJWTService
    {
        private readonly string secret;
        private readonly int exprise;

        public JWTService(IConfiguration config)
        {
            secret = config.GetSection("Token_Secret").Value!;
            exprise = int.Parse(config.GetSection("Token_Exp").Value!);
        }

        public string GenerateAccessToken(string userId, string ruleId)
        {
            var claims = new[]
            {
                new Claim("UserId", userId),
                new Claim("RuleId", ruleId),
            };

            // Create the token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(exprise),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                                                            SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            // Return the token as a string
            return tokenHandler.WriteToken(token);
        }
    }
}
