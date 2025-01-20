using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace CapstoneProject_SP25_IPAS_API.Middleware
{
    //public class AuthorizeMiddleware : AuthorizeAttribute, IAuthorizationFilter
    //{
    //private readonly RequestDelegate _next;

    //public AuthorizeMiddleware(RequestDelegate next)
    //{
    //    _next = next;
    //}

    //public async Task Invoke(HttpContext context)
    //{
    //    if (context.Response.StatusCode == StatusCodes.Status401Unauthorized)
    //    {
    //        context.Response.ContentType = "application/problem+json";
    //        var problemDetails = new ProblemDetails
    //        {
    //            Status = StatusCodes.Status401Unauthorized,
    //            Title = "Unauthorized",
    //            Detail = "Bạn chưa được xác thực",
    //        };
    //        await context.Response.WriteAsJsonAsync(problemDetails);
    //    }
    //    else if (context.Response.StatusCode == StatusCodes.Status403Forbidden)
    //    {
    //        context.Response.ContentType = "application/problem+json";
    //        var problemDetails = new ProblemDetails
    //        {
    //            Status = StatusCodes.Status403Forbidden,
    //            Title = "Forbidden",
    //            Detail = "Bạn không có quyền truy cập vào tài nguyên này",
    //        };
    //        await context.Response.WriteAsJsonAsync(problemDetails);
    //    }
    //    await _next(context);
    //}

    //public void OnAuthorization(AuthorizationFilterContext context)
    //{
    //    var user = context.HttpContext.User;

    //    // Kiểm tra nếu chưa đăng nhập
    //    if (!user.Identity.IsAuthenticated)
    //    {
    //        context.Result = new UnauthorizedResult();
    //        return;
    //    }

    //    // Lấy danh sách roles từ Claims
    //    var userRoles = user.Claims
    //                        .Where(c => c.Type == ClaimTypes.Role)
    //                        .Select(c => c.Value)
    //                        .ToList();

    //    // Nếu user không có quyền truy cập vào bất kỳ role nào
    //    if (!_roles.Any(role => userRoles.Contains(role)))
    //    {
    //        context.Result = new ForbidResult();
    //        return;
    //    }
    //}
    //}
}
