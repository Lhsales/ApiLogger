using Microsoft.AspNetCore.Http;
using Serilog;
using ServiceStack.Text;

namespace ApiLogger.Serilog
{
    public class ApiLoggerFunctions : RequestResponseEnricher, IApiLoggerFunctions
    {
        private readonly ILogger _logger;
        protected RecyclableMemoryStreamManager RecyclableMemory { get; } = new RecyclableMemoryStreamManager();

        public ApiLoggerFunctions()
        {
            _logger = Log.Logger.ForContext(this);
        }

        public async Task CreateRequest(HttpRequest request)
        {
            try
            {
                IpAddress = request.HttpContext?.Connection?.RemoteIpAddress?.ToString();
                Scheme = request.Scheme?.ToUpper();
                Method = request.Method;
                RequestPath = $"{request.Path}{request.QueryString}";
                RequestFullPath = $"{request.Host.Value}{request.Path}{request.QueryString}";
                RequestBody = await GetRequestBody(request);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
            }
        }
        public async Task CreateResponse(HttpContext context, RequestDelegate next)
        {
            try
            {
                using (var responseBody = RecyclableMemory.GetStream())
                {
                    Stream originalBodyStream = context.Response.Body;
                    context.Response.Body = responseBody;

                    await next(context);

                    responseBody.Seek(0, SeekOrigin.Begin);
                    await responseBody.CopyToAsync(originalBodyStream);

                    ResponseBody = responseBody.ReadAsString();
                }
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                Exception = ex;
            }
            finally
            {
                StatusCode = context.Response.StatusCode;
            }
        }
        public void WriteLog(HttpContext context)
        {
            var statusCode = context.Response.StatusCode;

            switch (statusCode)
            {
                case int when statusCode >= 100 && statusCode < 200: //Information
                case int when statusCode >= 200 && statusCode < 300: //Success
                case int when statusCode >= 300 && statusCode < 400: //Redirection
                    _logger.Information(MESSAGE_FORMAT);
                    break;
                case int when statusCode >= 400 && statusCode < 500: // client side error
                    _logger.Warning(MESSAGE_FORMAT);
                    break;
                case int when statusCode >= 500 && statusCode < 600: // server side error
                    _logger.Error(Exception, MESSAGE_FORMAT);
                    break;
                default:
                    _logger.Error(Exception, MESSAGE_FORMAT);
                    break;
            }
        }

        private async Task<string> GetRequestBody(HttpRequest request)
        {
            request.EnableBuffering();

            using (var requestStream = RecyclableMemory.GetStream())
            {
                await request.Body.CopyToAsync(requestStream);
                request.Body.Seek(0, SeekOrigin.Begin);
                return requestStream.ReadAsString();
            }
        }
    }
}
