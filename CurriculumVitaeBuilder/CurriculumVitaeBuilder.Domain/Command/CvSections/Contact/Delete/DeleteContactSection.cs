﻿// <copyright file="DeleteContactSection.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Domain.Command.CvSections.Contact.Delete
{
    using System;

    using Chest.Core.Command;

    /// <summary>
    /// Delete Section Command.
    /// </summary>
    [CommandName("CVSECTION/DELETE_CONTACT_SECTION")]

    public class DeleteContactSection : ICommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteContactSection"/> class.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="cvId">The cv identifier.</param>
        public DeleteContactSection(
            Guid userId,
            Guid cvId)
        {
            this.UserId = userId;
            this.CvId = cvId;
        }

        /// <summary>
        /// Gets the user identifier.
        /// </summary>
        public Guid UserId { get; }

        /// <summary>
        /// Gets the CV identifier.
        /// </summary>
        public Guid CvId { get; }
    }
}
