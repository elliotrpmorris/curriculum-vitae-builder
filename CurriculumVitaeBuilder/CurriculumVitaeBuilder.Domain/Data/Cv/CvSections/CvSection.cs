// <copyright file="CvSection.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Domain.Data.Cv.CvSections
{
    using System;

    /// <summary>
    /// CV section.
    /// </summary>
    public abstract class CvSection
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CvSection"/> class.
        /// </summary>
        /// <param name="id">The section identifier.</param>
        /// <param name="cvId">The CV identifier.</param>
        public CvSection(
            Guid id,
            Guid cvId)
        {
            if (id == default)
            {
                throw new ArgumentException(nameof(id));
            }

            if (cvId == default)
            {
                throw new ArgumentException(nameof(cvId));
            }

            this.Id = id;
            this.CvId = cvId;

            // this.Title = "Section Title";
        }

        /// <summary>
        /// Gets the section identifier.
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Gets the CV identifier.
        /// </summary>
        public Guid CvId { get; }

        /// <summary>
        /// Gets or sets the section title.
        /// </summary>
        public abstract string Title { get; set; }
    }
}
