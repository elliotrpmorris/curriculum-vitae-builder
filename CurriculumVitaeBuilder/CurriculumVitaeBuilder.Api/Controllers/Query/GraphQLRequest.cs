// <copyright file="GraphQLRequest.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Api.Controllers.Query
{
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Represents a graphql query request.
    /// </summary>
    public class GraphQLRequest
    {
        /// <summary>
        /// Gets or sets the query.
        /// </summary>
        public string? Query { get; set; }

        /// <summary>
        /// Gets or sets the query variables.
        /// </summary>
        public JObject? Variables { get; set; }
    }
}
