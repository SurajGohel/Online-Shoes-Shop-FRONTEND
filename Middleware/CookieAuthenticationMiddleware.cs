using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Online_Shoes_Shop.Middleware
{
    public class CookieAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public CookieAuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Check if cookies exist
            var username = context.Request.Cookies["username"];
            var role = context.Request.Cookies["role"];

            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(role))
            {
                // Create claims from the cookies
                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, role)
            };

                // Create a claims identity and set it on the HttpContext
                var identity = new ClaimsIdentity(claims, "CookieAuth");
                var principal = new ClaimsPrincipal(identity);
                

                context.User = principal;
            }

            // Continue the request pipeline
            await _next(context);
        }
    }
}
