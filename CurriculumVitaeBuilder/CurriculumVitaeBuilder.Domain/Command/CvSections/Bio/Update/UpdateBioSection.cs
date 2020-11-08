// <copyright file="UpdateBioSection.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Domain.Command.CvSections.Bio.Update
{
    using System;

    using Chest.Core.Command;

    /// <summary>
    /// Create Section Command.
    /// </summary>
    [CommandName("CVSECTION/UPDATE_BIO_SECTION")]
    public class UpdateBioSection : ICommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateBioSection"/> class.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="cvId">The cv identifier.</param>
        /// <param name="fullName">The full name.</param>
        /// <param name="city">The city.</param>
        public UpdateBioSection(
            Guid userId,
            Guid cvId,
            string? fullName = null,
            string? city = null)
        {
            this.UserId = userId;
            this.CvId = cvId;
            this.FullName = fullName;
            this.City = city;
        }

        /// <summary>
        /// Gets the user identifier.
        /// </summary>
        public Guid UserId { get; }

        /// <summary>
        /// Gets the CV identifier.
        /// </summary>
        public Guid CvId { get; }

        /// <summary>
        /// Gets the full name.
        /// </summary>
        public string? FullName { get; }

        /// <summary>
        /// Gets the city.
        /// </summary>
        public string? City { get; }
    }
}
