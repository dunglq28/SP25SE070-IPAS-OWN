﻿using CapstoneProject_SP25_IPAS_Service.Payloads.Response;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;

namespace CapstoneProject_SP25_IPAS_API.Middleware
{
    public class TokenValidationMiddleware
    {
        private readonly RequestDelegate _next;

        public TokenValidationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (token != null)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                try
                {
                    var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;
                    if (jwtToken != null)
                    {
                        var expirationDate = jwtToken.ValidTo;
                        if (expirationDate < DateTime.UtcNow)
                        {
                            var response = new BaseResponse
                            {
                                StatusCode = StatusCodes.Status401Unauthorized,
                                Message = "Token is expired!",
                                Data = null,
                                IsSuccess = false
                            };
                            context.Response.ContentType = "application/json";
                            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                            await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
                            return;
                        }
                    }
                }
                catch (Exception)
                {
                    context.Response.StatusCode = 401; // Unauthorized
                    await context.Response.WriteAsync("Invalid Token");
                    return;
                }
            }
            await _next(context);
        }
    }
}
