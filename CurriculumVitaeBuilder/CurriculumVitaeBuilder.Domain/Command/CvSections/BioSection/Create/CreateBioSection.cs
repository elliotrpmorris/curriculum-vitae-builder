// <copyright file="CreateBioSection.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Domain.Command.CvSections.BioSection.Create
{
    using System;

    using Chest.Core.Command;

    /// <summary>
    /// Create CV Bio Section Command.
    /// </summary>
    [CommandName("CVSECTION/CREATE_BIO_SECTION")]
    public class CreateBioSection : ICommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateBioSection"/> class.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="cvId">The cv identifier.</param>
        /// <param name="fullName">The full name.</param>
        /// <param name="city">The city.</param>
        public CreateBioSection(
            Guid userId,
            Guid cvId,
            string fullName,
            string city)
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
        public string FullName { get; }

        /// <summary>
        /// Gets the city.
        /// </summary>
        public string City { get; }
    }
}
