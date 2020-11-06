// <copyright file="CommandNameNotDefinedException.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace Chest.Core.Exceptions
{
    using System;

    /// <summary>
    /// An exception that is thrown when trying to resolve a command name from
    /// the <see cref="CommandNameAttribute"/> when the attribute is not present.
    /// </summary>
    [Serializable]
    public class CommandNameNotDefinedException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandNameNotDefinedException"/> class.
        /// </summary>
        /// <param name="commandType">The type of command.</param>
        public CommandNameNotDefinedException(Type commandType)
            : base($"Command name not defined: {commandType.Name}")
        {
        }
    }
}
