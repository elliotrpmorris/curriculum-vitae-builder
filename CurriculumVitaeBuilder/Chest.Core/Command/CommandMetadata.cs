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

        public string CommandName { get; }

        public DateTime Timestamp { get; }

        public string CorrelationId { get; }

        public dynamic Context { get; }
    }
}
