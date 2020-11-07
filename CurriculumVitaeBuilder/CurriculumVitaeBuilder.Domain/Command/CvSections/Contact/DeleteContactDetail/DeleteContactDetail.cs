// <copyright file="DeleteContactDetail.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Domain.Command.CvSections.Contact.DeleteContactDetail
{
    using System;

    using Chest.Core.Command;

    /// <summary>
    /// Delete Contact Detail Command.
    /// </summary>
    [CommandName("CVSECTION/DELETE_CONTACT_DETAIL")]
    public class DeleteContactDetail : ICommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteContactDetail"/> class.
        /// </summary>
        /// <param name="cvId">The CV identifier.</param>
        /// <param name="contactDetail">The contact detail name.</param>
        public DeleteContactDetail(
            Guid cvId,
            string contactDetail)
        {
            this.CvId = cvId;
            this.ContactDetail = contactDetail;
        }

        /// <summary>
        /// Gets the CV identifier.
        /// </summary>
        public Guid CvId { get; }

        /// <summary>
        /// Gets the contact detail name.
        /// </summary>
        public string ContactDetail { get; }
    }
}
