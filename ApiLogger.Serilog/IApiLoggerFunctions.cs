using Microsoft.AspNetCore.Http;

namespace ApiLogger.Serilog
{
    public interface IApiLoggerFunctions
    {
        Task CreateRequest(HttpRequest request);
        Task CreateResponse(HttpContext context, RequestDelegate next);
        void WriteLog(HttpContext context);
    }
}
