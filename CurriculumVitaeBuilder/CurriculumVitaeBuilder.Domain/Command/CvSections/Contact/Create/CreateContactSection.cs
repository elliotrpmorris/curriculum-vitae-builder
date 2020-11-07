// <copyright file="CreateContactSection.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Domain.Command.CvSections.Contact.Create
{
    using System;
    using System.Collections.Generic;

    using Chest.Core.Command;

    /// <summary>
    /// Create Section Command.
    /// </summary>
    [CommandName("CVSECTION/CREATE_CONTACT_SECTION")]
    public class CreateContactSection : ICommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateContactSection"/> class.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="cvId">The cv identifier.</param>
        /// <param name="contactDetails">The collection of contact details.</param>
        public CreateContactSection(
            Guid userId,
            Guid cvId,
            IDictionary<string, string> contactDetails)
        {
            this.UserId = userId;
            this.CvId = cvId;

            this.ContactDetails = contactDetails
                ?? throw new ArgumentNullException(nameof(contactDetails));
        }

        /// <summary>
        /// Gets the user identifier.
        /// </summary>
        public Guid UserId { get; }

        /// <summary>
        /// Gets the CV identifier.
        /// </summary>
        public Guid CvId { get; }

        /// <summary>
        /// Gets the collection of contact details.
        /// </summary>
        public IDictionary<string, string> ContactDetails { get; }
    }
}
