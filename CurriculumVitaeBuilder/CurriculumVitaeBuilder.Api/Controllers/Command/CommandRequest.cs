// <copyright file="CommandRequest.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Api.Controllers.Command
{
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Represents a command request object.
    /// </summary>
    public class CommandRequest
    {
        /// <summary>
        /// Gets or sets the command name.
        /// </summary>
        public string? Command { get; set; }

        /// <summary>
        /// Gets or sets the command body.
        /// </summary>
        public JObject? Body { get; set; }
    }
}
