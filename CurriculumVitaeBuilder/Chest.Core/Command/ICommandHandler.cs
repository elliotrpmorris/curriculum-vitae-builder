// <copyright file="ICommandHandler.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace Chest.Core.Command
{
    using System.Threading.Tasks;

    /// <summary>
    /// An object that can handle commands of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The command object type.</typeparam>
    public interface ICommandHandler<in T>
        where T : ICommand
    {
        /// <summary>
        /// Handles a command of <typeparamref name="T"/>.
        /// </summary>
        /// <param name="command">The command instance that should be handled.</param>
        /// <param name="metadata">Metadata for this command.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task Handle(T command, CommandMetadata metadata);
    }
}
