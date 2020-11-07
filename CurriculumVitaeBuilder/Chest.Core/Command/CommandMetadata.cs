// <copyright file="CommandMetadata.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace Chest.Core.Command
{
    using System;

    /// <summary>
    /// Command metadata.
    /// </summary>
    public class CommandMetadata
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandMetadata"/> class.
        /// </summary>
        /// <param name="commandName">The command name.</param>
        /// <param name="timestamp">The timestamp.</param>
        /// <param name="correlationId">The correlation identifier.</param>
        /// <param name="context">The context.</param>
        public CommandMetadata(
            string commandName,
            DateTime timestamp,
            string correlationId,
            dynamic? context = null)
        {
            this.CommandName = commandName;
            this.Timestamp = timestamp;
            this.CorrelationId = correlationId;
            this.Context = context;
        }

        /// <summary>
        /// Gets the command name.
        /// </summary>
        public string CommandName { get; }

        /// <summary>
        /// Gets the timestamp.
        /// </summary>
        public DateTime Timestamp { get; }

        /// <summary>
        /// Gets the correlation identifier.
        /// </summary>
        public string CorrelationId { get; }

        /// <summary>
        /// Gets the context.
        /// </summary>
        public dynamic? Context { get; }
    }
}
