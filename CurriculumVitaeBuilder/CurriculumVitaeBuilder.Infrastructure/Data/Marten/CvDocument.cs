// <copyright file="CvDocument.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Infrastructure.Data.Marten
{
    using System;

    /// <summary>
    /// CV document.
    /// </summary>
    public class CvDocument
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CvDocument"/> class.
        /// </summary>
        /// <param name="id">The CV identifier.</param>
        /// <param name="userId">The user identifier.</param>
        public CvDocument(
            Guid id,
            Guid userId)
        {
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
