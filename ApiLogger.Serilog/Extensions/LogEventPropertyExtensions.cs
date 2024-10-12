using Serilog.Core;

namespace Serilog.Events
{
    internal static class LogEventPropertyExtensions
    {
        public static void CreatePropertyIfHasValue(this LogEvent logEvent, ILogEventPropertyFactory propertyFactory, string name, object? obj, bool destructureObjects = false)
        {
            if (obj != null)
            {
                logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty(name, obj, destructureObjects));
            }
        }
    }
}
