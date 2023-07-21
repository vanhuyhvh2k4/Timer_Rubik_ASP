using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Timer_Rubik.WebApp.Attributes;

namespace Timer_Rubik.WebApp.Middlewares
{
    public class AdminTokenMiddleware
    {
        private readonly RequestDelegate _next;

        public AdminTokenMiddleware(RequestDelegate next)
        {
            _next = next;
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
                    var key = Encoding.UTF8.GetBytes("timer-rubik-secret-access-token");

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

                        if (ruleId.ToString().Equals("231429c6-1fa3-11ee-9b01-a02bb82e10f9"))
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
