// <copyright file="ContactSection.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Domain.Data.CvSections.Contact
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Contact Section.
    /// </summary>
    public class ContactSection : CvSection
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ContactSection"/> class.
        /// </summary>
        /// <param name="id">The section identifier.</param>
        /// <param name="cvId">The CV identifier.</param>
        /// <param name="contactDetails">The collection of contact details.</param>
        public ContactSection(
            Guid id,
            Guid cvId,
            IDictionary<string, string> contactDetails)
            : base(id, cvId)
        {
            this.ContactDetails = contactDetails
                ?? throw new ArgumentNullException(nameof(contactDetails));

            this.Title = "Contact";
        }

        /// <summary>
        /// Gets the collection of contact details.
        /// </summary>
        public IDictionary<string, string> ContactDetails { get; }

        /// <summary>
        /// Gets or sets the Title.
        /// </summary>
        public override string Title { get; set; }
    }
}
