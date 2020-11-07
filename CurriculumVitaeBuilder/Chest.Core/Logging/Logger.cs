// <copyright file="Logger.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace Chest.Core.Logging
{
    using System;

    using Microsoft.Extensions.Logging;

    using MsLogger = Microsoft.Extensions.Logging.LoggerExtensions;

    /// <summary>
    /// A static logging provider.
    /// </summary>
    public static class Logger
    {
        private static ILogger DefaultLogger { get; set; } =
            LoggerFactory.Create(builder => builder.AddConsole()).CreateLogger("BJSS");

        /// <summary>
        /// Logs an error message.
        /// </summary>
        /// <param name="message">The log message.</param>
        public static void LogError(string message)
        {
            MsLogger.LogError(DefaultLogger, message.Stamp());
        }

        /// <summary>
        /// Logs an error message.
        /// </summary>
        /// <param name="exception">The exception to log.</param>
        /// <param name="message">The log message.</param>
        public static void LogError(Exception exception, string message)
        {
            MsLogger.LogError(DefaultLogger, exception, message.Stamp());
        }

        /// <summary>
        /// Logs an information message.
        /// </summary>
        /// <param name="message">The log message.</param>
        public static void LogInformation(string message)
        {
            MsLogger.LogInformation(DefaultLogger, message.Stamp());
        }

        /// <summary>
        /// Logs an information message.
        /// </summary>
        /// <param name="exception">The exception to log.</param>
        /// <param name="message">The log message.</param>
        public static void LogInformation(Exception exception, string message)
        {
            MsLogger.LogInformation(DefaultLogger, exception, message.Stamp());
        }

        /// <summary>
        /// Logs a warning message.
        /// </summary>
        /// <param name="message">The log message.</param>
        public static void LogWarning(string message)
        {
            MsLogger.LogWarning(DefaultLogger, message.Stamp());
        }

        /// <summary>
        /// Logs a warning message.
        /// </summary>
        /// <param name="exception">The exception to log.</param>
        /// <param name="message">The log message.</param>
        public static void LogWarning(Exception exception, string message)
        {
            MsLogger.LogWarning(DefaultLogger, exception, message.Stamp());
        }

        /// <summary>
        /// Logs a trace message.
        /// </summary>
        /// <param name="message">The log message.</param>
        public static void LogTrace(string message)
        {
            MsLogger.LogTrace(DefaultLogger, message.Stamp());
        }

        /// <summary>
        /// Logs a trace message.
        /// </summary>
        /// <param name="exception">The exception to log.</param>
        /// <param name="message">The log message.</param>
        public static void LogTrace(Exception exception, string message)
        {
            MsLogger.LogTrace(DefaultLogger, exception, message.Stamp());
        }

        /// <summary>
        /// Logs a debug message.
        /// </summary>
        /// <param name="message">The log message.</param>
        public static void LogDebug(string message)
        {
            MsLogger.LogDebug(DefaultLogger, message.Stamp());
        }

        /// <summary>
        /// Logs a debug message.
        /// </summary>
        /// <param name="exception">The exception to log.</param>
        /// <param name="message">The log message.</param>
        public static void LogDebug(Exception exception, string message)
        {
            MsLogger.LogDebug(DefaultLogger, exception, message.Stamp());
        }

        /// <summary>
        /// Configures the static logger instance.
        /// </summary>
        /// <param name="logger">The logger instance.</param>
        internal static void SetLogger(ILogger logger)
        {
            DefaultLogger = logger;
        }

        private static string Stamp(this string message)
        {
            return $"{DateTime.Now:u} | {message}";
        }
    }
}