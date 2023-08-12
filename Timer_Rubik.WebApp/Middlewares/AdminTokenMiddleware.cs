using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Timer_Rubik.WebApp.Attributes;

namespace Timer_Rubik.WebApp.Middlewares
{
    public class AdminTokenMiddleware
    {
        private readonly string secret;
        private readonly string adminId;
        private readonly RequestDelegate _next;

        public AdminTokenMiddleware(RequestDelegate next, IConfiguration config)
        {
            _next = next;
            secret = config.GetSection("Token_Secret").Value!;
            adminId = config.GetSection("Admin_Id").Value!;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.GetEndpoint()?.Metadata?.GetMetadata<AdminTokenAttribute>() != null)
            {
                var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();
                if (authHeader != null && authHeader.StartsWith("Bearer "))
                {
                    var token = authHeader.Substring("Bearer ".Length);

                    // Xác thực JWT token
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.UTF8.GetBytes(secret);

                    try
                    {
                        var claimsPrincipal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                        {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(key),
                            ValidateIssuer = false,
                            ValidateAudience = false
                        }, out var validatedToken);

                        var ruleId = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == "RuleId")?.Value;

                        if (ruleId.ToString().Equals(adminId))
                        {
                            context.User = claimsPrincipal;
                            await _next(context);
                        } else
                        {
                            context.Response.StatusCode = 403;
                            await context.Response.WriteAsync("You does not have permision to access");
                        }
                    }
                    catch (Exception)
                    {
                        context.Response.StatusCode = 401;
                        await context.Response.WriteAsync("Invalid token");
                        return;
                    }
                } else
                {
                    context.Response.StatusCode = 401;
                    await context.Response.WriteAsync("Missing or invalid authorization header");
                    return;
                }
            } else
            {
                await _next(context);
            }
        }
    }
}
