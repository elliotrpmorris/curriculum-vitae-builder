// <copyright file="DeleteEducationSection.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Domain.Command.CvSections.Education.Delete
{
    using System;

    using Chest.Core.Command;

    /// <summary>
    /// Delete Section Command.
    /// </summary>
    [CommandName("CVSECTION/DELETE_EDUCATION_SECTION")]

    public class DeleteEducationSection : ICommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteEducationSection"/> class.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="cvId">The cv identifier.</param>
        public DeleteEducationSection(
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
