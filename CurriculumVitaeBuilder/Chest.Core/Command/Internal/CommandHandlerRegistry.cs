// <copyright file="CommandHandlerRegistry.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace Chest.Core.Command.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Chest.Core.Exceptions;

    /// <inheritdoc />
    internal class CommandHandlerRegistry : ICommandHandlerRegistry
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandHandlerRegistry"/> class.
        /// </summary>
        /// <param name="handlerDetails">The list of handler details to hold in the registry.</param>
        public CommandHandlerRegistry(IEnumerable<CommandHandlerDetail> handlerDetails)
        {
            this.HandlerDetails = handlerDetails;
        }

        private IEnumerable<CommandHandlerDetail> HandlerDetails { get; }

        /// <inheritdoc />
        public CommandHandlerDetail FindByName(string name)
        {
            var handlerDetail =
                this.HandlerDetails.FirstOrDefault(
                    h => h.CommandName.Equals(name, StringComparison.InvariantCultureIgnoreCase));

            if (handlerDetail == null)
            {
                throw new CommandHandlerNotFoundException();
            }

            return handlerDetail;
        }

        /// <inheritdoc />
        public CommandHandlerDetail FindByType(Type type)
        {
            var handlerDetail = this.HandlerDetails.FirstOrDefault(h => h.CommandType == type);

            if (handlerDetail == null)
            {
                throw new CommandHandlerNotFoundException();
            }

            return handlerDetail;
        }

        /// <inheritdoc />
        public CommandHandlerDetail FindByTypeName(string typeName)
        {
            var handlerDetail =
                this.HandlerDetails.FirstOrDefault(
                    h => h.CommandType.Name.Equals(typeName, StringComparison.InvariantCultureIgnoreCase));

            if (handlerDetail == null)
            {
                throw new CommandHandlerNotFoundException();
            }

            return handlerDetail;
        }
    }
}
