using System;

namespace RestaurantOrderRouting.Infrastructure.Logging
{
    /// <summary>
    /// Service that logs messages. It's just a simplification for a Logger service.
    /// </summary>
    public static class Logger
    {
        public static void Log(string message, LogSeverity severity)
        {
            Console.WriteLine($"[{DateTime.UtcNow}][{severity}] {message}");
        }

        public static void LogException(Exception ex)
        {
            Console.WriteLine($"[{DateTime.UtcNow}][{LogSeverity.Error}] {ex.Message}");
        }
    }
}
