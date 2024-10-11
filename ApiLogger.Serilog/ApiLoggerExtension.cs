using Microsoft.AspNetCore.Builder;

namespace ApiLogger.Serilog
{
    public static class ApiLoggerExtension
    {
        public static IApplicationBuilder UseApiLoggerSerilogMiddleware(this IApplicationBuilder builder) 
        {
            return builder.UseMiddleware<ApiLoggerMiddleware>();
        }
    }
}
