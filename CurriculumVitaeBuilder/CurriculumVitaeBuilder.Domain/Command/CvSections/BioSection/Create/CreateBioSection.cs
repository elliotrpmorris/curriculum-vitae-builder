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
        public CreateBioSection(
            Guid userId,
            Guid cvId,
            string fullName,
            string city,
            string? title = null)
        {
            this.UserId = userId;
            this.CvId = cvId;
            this.FullName = fullName;
            this.City = city;
            this.Title = title;
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

        /// <summary>
        /// Gets the Title.
        /// </summary>
        public string? Title { get; }
    }
}
