// Utility class for standardized logging across test execution.
// Outputs informational and error messages to the console with timestamps.

using System;

namespace SauceDemoTests.Utils
{
    public static class Logger
    {
        // Logs an informational message with the current timestamp.
        public static void Info(string message)
        {
            Console.WriteLine($"[INFO] {DateTime.Now:HH:mm:ss} - {message}");
        }

        // Logs an error message with the current timestamp.
        public static void Error(string message)
        {
            Console.WriteLine($"[ERROR] {DateTime.Now:HH:mm:ss} - {message}");
        }
    }
}
