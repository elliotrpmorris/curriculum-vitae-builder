// <copyright file="CommandRegistrationException.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace Chest.Core.Exceptions
{
    using System;

    /// <summary>
    /// An exception that is thrown when there is a problem registering commands
    /// and their handlers.
    /// </summary>
    [Serializable]
    public class CommandRegistrationException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandRegistrationException"/> class.
        /// </summary>
        /// <param name="message">The exception message.</param>
        public CommandRegistrationException(string message)
            : base(message)
        {
        }
    }
}
