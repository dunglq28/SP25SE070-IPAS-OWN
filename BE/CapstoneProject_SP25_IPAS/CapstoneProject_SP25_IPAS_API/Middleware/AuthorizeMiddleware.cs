using Microsoft.AspNetCore.Mvc;

namespace CapstoneProject_SP25_IPAS_API.Middleware
{
    public class AuthorizeMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthorizeMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Response.StatusCode == StatusCodes.Status401Unauthorized)
            {
                context.Response.ContentType = "application/problem+json";
                var problemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status401Unauthorized,
                    Title = "Unauthorized",
                    Detail = "Bạn chưa được xác thực",
                };
                await context.Response.WriteAsJsonAsync(problemDetails);
            }
            else if (context.Response.StatusCode == StatusCodes.Status403Forbidden)
            {
                context.Response.ContentType = "application/problem+json";
                var problemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status403Forbidden,
                    Title = "Forbidden",
                    Detail = "Bạn không có quyền truy cập vào tài nguyên này",
                };
                await context.Response.WriteAsJsonAsync(problemDetails);
            }
            await _next(context);
        }
    }
}
