// <copyright file="SkillsProfileSectionExtensions.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Infrastructure.Data.Marten.CvSections.SkillsProfile
{
    using System;

    using CurriculumVitaeBuilder.Domain.Data.CvSections.SkillsProfile;

    /// <summary>
    /// Contact Section extensions.
    /// </summary>
    internal static class SkillsProfileSectionExtensions
    {
        /// <summary>
        /// Convert From Document to Data Object.
        /// </summary>
        /// <param name="section">The Section Document. </param>
        /// <returns>The Data Object.</returns>
        public static SkillsProfileSection ToSkillsProfile(this SkillsProfileSectionDocument section)
        {
            if (section == null)
            {
                throw new ArgumentNullException(nameof(section));
            }

            return new SkillsProfileSection(
                section.Id,
                section.CvId,
                section.SKills);
        }

        /// <summary>
        /// Convert Document to Data Object to Document.
        /// </summary>
        /// <param name="section">The Section.</param>
        /// <returns>The Data Object.</returns>
        public static SkillsProfileSectionDocument ToSkillsProfileSectionDocument(this SkillsProfileSection section)
        {
            if (section == null)
            {
                throw new ArgumentNullException(nameof(section));
            }

            return new SkillsProfileSectionDocument(
                section.Id,
                section.CvId,
                section.Skills);
        }
    }
}
