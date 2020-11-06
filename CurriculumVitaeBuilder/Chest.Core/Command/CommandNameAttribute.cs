// <copyright file="CommandNameAttribute.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace Chest.Core.Command
{
    using System;

    /// <summary>
    /// Specifies the name of a command.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class CommandNameAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandNameAttribute"/> class.
        /// </summary>
        /// <param name="name">The command name.</param>
        public CommandNameAttribute(string name)
        {
            this.Name = name;
        }

        /// <summary>
        /// Gets the command name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets or sets the command version.
        /// </summary>
        public int Version { get; set; } = 1;
    }
}
