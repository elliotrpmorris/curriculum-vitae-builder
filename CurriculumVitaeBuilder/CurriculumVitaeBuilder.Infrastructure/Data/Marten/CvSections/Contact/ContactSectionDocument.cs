// <copyright file="ContactSectionDocument.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Infrastructure.Data.Marten.CvSections.Contact
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Contact Section document.
    /// </summary>
    public class ContactSectionDocument : CvSectionDocument
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ContactSectionDocument"/> class.
        /// </summary>
        /// <param name="id">The section identifier.</param>
        /// <param name="cvId">The CV identifier.</param>
        /// <param name="contactDetails">The collection of contact details.</param>
        public ContactSectionDocument(
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
