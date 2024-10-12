using Serilog.Core;
using Serilog.Events;

namespace ApiLogger.Serilog
{
    public class RequestResponseEnricher : ILogEventEnricher
    {
        protected const string MESSAGE_FORMAT = "{Scheme} {Method} {RequestPath} responded {StatusCode} in {Elapsed:0.000} seconds";

        public RequestResponseEnricher() 
        {
            StartTime = DateTime.Now.ToTimeSpan();
        }

        protected string? IpAddress { get; set; }
        protected string? Scheme { get; set; }
        protected string? Method { get; set; }
        protected string? RequestPath { get; set; }
        protected string? RequestFullPath { get; set; }
        protected string? RequestBody { get; set; }
        protected int? StatusCode { get; set; }
        protected string? ResponseBody { get; set; }
        protected TimeSpan StartTime { get; private set; }
        protected TimeSpan EndTime => GetEndTime();
        protected double? Elapsed { get; private set; }
        protected Exception? Exception { get; set; }

        private TimeSpan GetEndTime()
        {
            var endTime = DateTime.Now.ToTimeSpan();
            Elapsed = Math.Round(endTime.Subtract(StartTime).TotalSeconds, 3);
            return endTime;
        }
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            logEvent.CreatePropertyIfHasValue(propertyFactory, "IpAddress", IpAddress);
            logEvent.CreatePropertyIfHasValue(propertyFactory, "Scheme", Scheme);
            logEvent.CreatePropertyIfHasValue(propertyFactory, "Method", Method);
            logEvent.CreatePropertyIfHasValue(propertyFactory, "RequestPath", RequestPath);
            logEvent.CreatePropertyIfHasValue(propertyFactory, "RequestFullPath", RequestFullPath);
            logEvent.CreatePropertyIfHasValue(propertyFactory, "RequestBody", RequestBody);
            logEvent.CreatePropertyIfHasValue(propertyFactory, "ResponseBody", ResponseBody);
            logEvent.CreatePropertyIfHasValue(propertyFactory, "StatusCode", StatusCode);
            logEvent.CreatePropertyIfHasValue(propertyFactory, "Time-Start", StartTime.FormatHour());
            logEvent.CreatePropertyIfHasValue(propertyFactory, "Time-End", EndTime.FormatHour());
            logEvent.CreatePropertyIfHasValue(propertyFactory, "Elapsed", Elapsed);
        }
    }
}
