// <copyright file="DeleteSkillsProfileSection.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Domain.Command.CvSections.SkillsProfile.Delete
{
    using System;

    using Chest.Core.Command;

    /// <summary>
    /// Delete Section Command.
    /// </summary>
    [CommandName("CVSECTION/DELETE_SKILLSPROFILE_SECTION")]

    public class DeleteSkillsProfileSection : ICommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteSkillsProfileSection"/> class.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="cvId">The cv identifier.</param>
        public DeleteSkillsProfileSection(
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
