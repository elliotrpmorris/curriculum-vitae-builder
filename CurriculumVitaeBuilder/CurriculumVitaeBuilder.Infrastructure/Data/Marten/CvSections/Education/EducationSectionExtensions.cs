// <copyright file="EducationSectionExtensions.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Infrastructure.Data.Marten.CvSections.Education
{
    using System;

    using CurriculumVitaeBuilder.Domain.Data.CvSections.Education;

    /// <summary>
    /// Contact Section extensions.
    /// </summary>
    internal static class EducationSectionExtensions
    {
        /// <summary>
        /// Convert From Document to Data Object.
        /// </summary>
        /// <param name="section">The Section Document. </param>
        /// <returns>The Data Object.</returns>
        public static EducationSection ToEducationSection(this EducationSectionDocument section)
        {
            if (section == null)
            {
                throw new ArgumentNullException(nameof(section));
            }

            return new EducationSection(
                section.Id,
                section.CvId,
                section.EducationEstablishments);
        }

        /// <summary>
        /// Convert Document to Data Object to Document.
        /// </summary>
        /// <param name="section">The Section.</param>
        /// <returns>The Data Object.</returns>
        public static EducationSectionDocument ToEducationSectionDocument(this EducationSection section)
        {
            if (section == null)
            {
                throw new ArgumentNullException(nameof(section));
            }

            return new EducationSectionDocument(
                section.Id,
                section.CvId,
                section.EducationEstablishments);
        }
    }
}
