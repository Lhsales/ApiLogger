using Microsoft.AspNetCore.Http;
using Serilog.Context;

namespace ApiLogger.Serilog
{
    internal class ApiLoggerMiddleware
    {
        private readonly RequestDelegate _next;

        public ApiLoggerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IApiLoggerFunctions apiLoggerFunctions)
        {
            using (LogContext.Push(new RequestResponseEnricher()))
            {
                await apiLoggerFunctions.CreateRequest(context.Request);

                await apiLoggerFunctions.CreateResponse(context, _next);

                apiLoggerFunctions.WriteLog(context);
            }
        }
    }
}
