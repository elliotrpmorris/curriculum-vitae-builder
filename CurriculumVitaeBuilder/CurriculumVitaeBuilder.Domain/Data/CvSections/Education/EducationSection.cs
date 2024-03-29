﻿// <copyright file="EducationSection.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Domain.Data.CvSections.Education
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Education section.
    /// </summary>
    public class EducationSection : CvSection
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EducationSection"/> class.
        /// </summary>
        /// <param name="id">The section identifier.</param>
        /// <param name="cvId">The CV identifier.</param>
        /// <param name="educationEstablishments">The collection of education establishments.</param>
        public EducationSection(
            Guid id,
            Guid cvId,
            IList<EducationEstablishment> educationEstablishments)
            : base(id, cvId)
        {
            if (id == default)
            {
                throw new ArgumentException(nameof(id));
            }

            if (cvId == default)
            {
                throw new ArgumentException(nameof(cvId));
            }

            this.EducationEstablishments = educationEstablishments
                ?? throw new ArgumentNullException(nameof(educationEstablishments));

            this.Title = "Education";
        }

        /// <summary>
        /// Gets the collection of education establishments.
        /// </summary>
        public IList<EducationEstablishment> EducationEstablishments { get; }

        /// <summary>
        /// Gets or sets the Title.
        /// </summary>
        public override string Title { get; set; }
    }
}
