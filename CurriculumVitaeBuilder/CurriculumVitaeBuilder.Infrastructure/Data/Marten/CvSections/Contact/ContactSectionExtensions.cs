// <copyright file="ContactSectionExtensions.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Infrastructure.Data.Marten.CvSections.Contact
{
    using System;

    using CurriculumVitaeBuilder.Domain.Data.CvSections.Contact;

    /// <summary>
    /// Contact Section extensions.
    /// </summary>
    internal static class ContactSectionExtensions
    {
        /// <summary>
        /// Convert From Document to Data Object.
        /// </summary>
        /// <param name="section">The Section Document. </param>
        /// <returns>The Data Object.</returns>
        public static ContactSection ToContactSection(this ContactSectionDocument section)
        {
            if (section == null)
            {
                throw new ArgumentNullException(nameof(section));
            }

            return new ContactSection(
                section.Id,
                section.CvId,
                section.ContactDetails);
        }

        /// <summary>
        /// Convert Document to Data Object to Document.
        /// </summary>
        /// <param name="section">The Data Object.</param>
        /// <returns>The Gateway Data Object.</returns>
        public static ContactSectionDocument ToContactSectionDocument(this ContactSection section)
        {
            if (section == null)
            {
                throw new ArgumentNullException(nameof(section));
            }

            return new ContactSectionDocument(
                section.Id,
                section.CvId,
                section.ContactDetails);
        }
    }
}
