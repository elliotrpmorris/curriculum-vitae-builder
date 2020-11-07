// <copyright file="JobHistorySectionExtensions.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Infrastructure.Data.Marten.CvSections.JobHistory
{
    using System;

    using CurriculumVitaeBuilder.Domain.Data.CvSections;

    /// <summary>
    /// Contact Section extensions.
    /// </summary>
    internal static class JobHistorySectionExtensions
    {
        /// <summary>
        /// Convert From Document to Data Object.
        /// </summary>
        /// <param name="section">The Section Document. </param>
        /// <returns>The Data Object.</returns>
        public static JobHistorySection ToJobHistorySection(this JobHistorySectionDocument section)
        {
            if (section == null)
            {
                throw new ArgumentNullException(nameof(section));
            }

            return new JobHistorySection(
                section.Id,
                section.CvId,
                section.Jobs);
        }

        /// <summary>
        /// Convert Document to Data Object to Document.
        /// </summary>
        /// <param name="section">The Section.</param>
        /// <returns>The Data Object.</returns>
        public static JobHistorySectionDocument ToJobHistorySectionDocument(this JobHistorySection section)
        {
            if (section == null)
            {
                throw new ArgumentNullException(nameof(section));
            }

            return new JobHistorySectionDocument(
                section.Id,
                section.CvId,
                section.Jobs);
        }
    }
}
