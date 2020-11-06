// <copyright file="BioSectionExtensions.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Infrastructure.Data.Marten.CvSections.Bio
{
    using System;

    using CurriculumVitaeBuilder.Domain.Data.CvSections.Bio;

    /// <summary>
    /// Bio Section extensions.
    /// </summary>
    internal static class BioSectionExtensions
    {
        /// <summary>
        /// Convert From Document to Data Object.
        /// </summary>
        /// <param name="section">The Section Document. </param>
        /// <returns>The Data Object.</returns>
        public static BioSection ToBioSection(this BioSectionDocument section)
        {
            if (section == null)
            {
                throw new ArgumentNullException(nameof(section));
            }

            return new BioSection(
                section.Id,
                section.CvId,
                section.FullName,
                section.City);
        }

        /// <summary>
        /// Convert Document to Data Object to Document.
        /// </summary>
        /// <param name="section">The Data Object.</param>
        /// <returns>The Gateway Data Object.</returns>
        public static BioSectionDocument ToBioSectionDocument(this BioSection section)
        {
            if (section == null)
            {
                throw new ArgumentNullException(nameof(section));
            }

            return new BioSectionDocument(
                section.Id,
                section.CvId,
                section.FullName,
                section.City);
        }
    }
}
