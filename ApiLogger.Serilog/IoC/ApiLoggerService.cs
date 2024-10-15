using ApiLogger.Serilog;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ApiLoggerService
    {
        public static IServiceCollection AddApiLogger(this IServiceCollection services)
        {
            return services.AddTransient<IApiLoggerFunctions, ApiLoggerFunctions>();
        }
    }
}
