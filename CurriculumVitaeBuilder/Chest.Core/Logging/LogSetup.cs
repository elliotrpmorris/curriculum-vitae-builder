// <copyright file="LogSetup.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace Chest.Core.Logging
{

    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Configures the static application logger.
    /// </summary>
    public static class LogSetup
    {
        /// <summary>
        /// Using the provided logger factory, creates a logger instance to be used as the default static logger.
        /// </summary>
        /// <param name="factory">The logger factory instance.</param>
        /// <param name="name">The name of the logger.</param>
        public static void SetDefaultLogger(this ILoggerFactory factory, string name = "BJSS")
        {
            Logger.SetLogger(factory.CreateLogger(name));
        }

        /// <summary>
        /// Sets the <see cref="ILogger"/> instance to be used by the default static logger.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> instance to use.</param>
        public static void SetDefaultLogger(ILogger logger)
        {
            Logger.SetLogger(logger);
        }

        /// <summary>
        /// Sets the <see cref="ILogger"/> instance to be used by the default static logger.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> instance to use.</param>
        public static void SetAsDefaultLogger(this ILogger logger)
        {
            Logger.SetLogger(logger);
        }
    }
}
