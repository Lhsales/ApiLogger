using Microsoft.AspNetCore.Http;

namespace ApiLogger.Serilog
{
    internal class ApiLoggerMiddleware
    {
        private readonly RequestDelegate _next;

        public ApiLoggerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {            
            await _next(context);
        }

    }
}
