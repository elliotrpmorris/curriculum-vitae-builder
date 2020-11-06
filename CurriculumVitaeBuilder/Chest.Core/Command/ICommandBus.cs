// <copyright file="ICommandBus.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace Chest.Core.Command
{
    using System.Threading.Tasks;

    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Interface for the command bus.
    /// </summary>
    public interface ICommandBus
    {
        /// <summary>
        /// Send a command onto the bus to be validated and executed.
        /// </summary>
        /// <param name="command">The raw command object.</param>
        /// <param name="commandName">The command name.</param>
        /// <param name="metadata">The command metadata.</param>
        /// <param name="validateOnly">Whether the command should be validated, or validated and executed.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// </exception>
        Task Send(
            JObject command,
            string commandName,
            CommandMetadata metadata,
            bool validateOnly = false);
    }
}
