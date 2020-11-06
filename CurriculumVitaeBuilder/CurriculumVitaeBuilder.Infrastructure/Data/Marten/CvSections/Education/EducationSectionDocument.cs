// <copyright file="EducationSectionDocument.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Infrastructure.Data.Marten.CvSections.Education
{
    using System;
    using System.Collections.Generic;

    using CurriculumVitaeBuilder.Domain.Data.CvSections.Education;

    /// <summary>
    /// Contact Section document.
    /// </summary>
    public class EducationSectionDocument : CvSectionDocument
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EducationSectionDocument"/> class.
        /// </summary>
        /// <param name="id">The section identifier.</param>
        /// <param name="cvId">The CV identifier.</param>
        /// <param name="educationEstablishments">The collection of education establishments.</param>
        public EducationSectionDocument(
            Guid id,
            Guid cvId,
            IList<EducationEstablishment> educationEstablishments)
            : base(id, cvId)
        {
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
