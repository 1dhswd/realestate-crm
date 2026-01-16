using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstateCRM.API.Middleware
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var start = DateTime.UtcNow;

            await _next(context);

            var elapsed = (DateTime.UtcNow - start).TotalMilliseconds;
            var userId = context.User?.FindFirst("nameidentifier")?.Value;

            _logger.LogInformation(
                "HTTP {Method} {Path} => {StatusCode} | {Elapsed} ms | User: {UserId}",
                context.Request.Method,
                context.Request.Path,
                context.Response.StatusCode,
                elapsed,
                userId ?? "Anonymous"
            );
        }
    }

}
