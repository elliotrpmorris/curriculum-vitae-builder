// <copyright file="BioSection.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Domain.Data.Cv.CvSections.Bio
{
    using System;

    /// <summary>
    /// Bio section.
    /// </summary>
    public class BioSection : CvSection
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BioSection"/> class.
        /// </summary>
        /// <param name="id">The section identifier.</param>
        /// <param name="cvId">The CV identifier.</param>
        /// <param name="title">The section title.</param>
        /// <param name="subtitle">The optional section subtitle.</param>
        /// <param name="fullName">The full name.</param>
        /// <param name="dateOfBirth">The date of birth.</param>
        public BioSection(
            Guid id,
            Guid cvId,
            string title,
            string? subtitle,
            string fullName,
            DateTime dateOfBirth)
            : base(id, cvId, title, subtitle)
        {
            if (string.IsNullOrWhiteSpace(fullName))
            {
                throw new ArgumentException(nameof(fullName));
            }

            if (dateOfBirth == default)
            {
                throw new ArgumentException(nameof(dateOfBirth));
            }

            this.FullName = fullName;
            this.DateOfBirth = dateOfBirth;

            this.Title = "Bio";
        }

        /// <summary>
        /// Gets the full name.
        /// </summary>
        public string FullName { get; }

        /// <summary>
        /// Gets the date of birth.
        /// </summary>
        public DateTime DateOfBirth { get; }
    }
}
