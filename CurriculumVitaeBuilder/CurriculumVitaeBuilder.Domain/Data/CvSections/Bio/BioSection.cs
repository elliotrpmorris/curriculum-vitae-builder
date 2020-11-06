// <copyright file="BioSection.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Domain.Data.CvSections.Bio
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
        /// <param name="fullName">The full name.</param>
        /// <param name="city">The city.</param>
        public BioSection(
            Guid id,
            Guid cvId,
            string fullName,
            string city)
            : base(id, cvId)
        {
            if (string.IsNullOrWhiteSpace(fullName))
            {
                throw new ArgumentException(nameof(fullName));
            }

            if (city == default)
            {
                throw new ArgumentException(nameof(city));
            }

            this.FullName = fullName;
            this.City = city;

            this.Title = "Bio";
        }

        /// <summary>
        /// Gets the full name.
        /// </summary>
        public string FullName { get; }

        /// <summary>
        /// Gets the city.
        /// </summary>
        public string City { get; }

        /// <summary>
        /// Gets or sets the Title.
        /// </summary>
        public override string Title { get; set; }
    }
}
