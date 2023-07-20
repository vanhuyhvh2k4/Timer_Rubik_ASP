using Microsoft.AspNetCore.Mvc;
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
                await context.Response.WriteAsync("ok");
            } else
            {
                await _next(context);
            }
        }
    }
}
