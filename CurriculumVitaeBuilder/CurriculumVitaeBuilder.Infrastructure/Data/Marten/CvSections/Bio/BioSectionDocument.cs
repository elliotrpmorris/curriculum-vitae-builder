// <copyright file="BioSectionDocument.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Infrastructure.Data.Marten.CvSections.Bio
{
    using System;

    /// <summary>
    /// Bio Section Document.
    /// </summary>
    public class BioSectionDocument : CvSectionDocument
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BioSectionDocument"/> class.
        /// </summary>
        /// <param name="id">The section identifier.</param>
        /// <param name="cvId">The CV identifier.</param>
        /// <param name="fullName">The full name.</param>
        /// <param name="city">The city.</param>
        public BioSectionDocument(
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
