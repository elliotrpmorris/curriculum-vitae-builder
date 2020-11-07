// <copyright file="Cv.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Domain.Data
{
    using System;

    /// <summary>
    /// CV.
    /// </summary>
    public class Cv
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Cv"/> class.
        /// </summary>
        /// <param name="id">The CV identifier.</param>
        /// <param name="userId">The user identifier.</param>
        public Cv(
            Guid id,
            Guid userId)
        {
            if (id == default)
            {
                throw new ArgumentException(nameof(id));
            }

            if (userId == default)
            {
                throw new ArgumentException(nameof(userId));
            }

            this.Id = id;
            this.UserId = userId;
        }

        /// <summary>
        /// Gets the CV identifier.
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Gets the user identifier.
        /// </summary>
        public Guid UserId { get; }
    }
}
