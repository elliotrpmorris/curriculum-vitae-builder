// <copyright file="UserQueryContext.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Api.Controllers.Query.UserRoot
{
    using System;

    public class UserQueryContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserQueryContext"/> class.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        public UserQueryContext(Guid userId)
        {
            if (userId == default)
            {
                throw new ArgumentException(nameof(userId));
            }

            this.UserId = userId;
        }

        /// <summary>
        /// Gets the user identifier.
        /// </summary>
        public Guid UserId { get; }
    }
}
