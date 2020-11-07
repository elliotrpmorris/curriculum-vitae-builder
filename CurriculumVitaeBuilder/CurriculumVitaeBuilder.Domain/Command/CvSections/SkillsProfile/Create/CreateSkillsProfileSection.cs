// <copyright file="CreateSkillsProfileSection.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Domain.Command.CvSections.SkillsProfile.Create
{
    using System;
    using System.Collections.Generic;

    using Chest.Core.Command;

    using CurriculumVitaeBuilder.Domain.Data.CvSections.SkillsProfile;

    /// <summary>
    /// Create Section Command.
    /// </summary>
    [CommandName("CVSECTION/CREATE_SKILLSPROFILE_SECTION")]
    public class CreateSkillsProfileSection : ICommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateSkillsProfileSection"/> class.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="cvId">The cv identifier.</param>
        /// <param name="skills">The collection of skills.</param>
        public CreateSkillsProfileSection(
            Guid userId,
            Guid cvId,
            IList<Skill> skills)
        {
            this.UserId = userId;
            this.CvId = cvId;

            this.Skills = skills
                ?? throw new ArgumentNullException(nameof(skills));
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
        /// Gets the collection of skills.
        /// </summary>
        public IList<Skill> Skills { get; }
    }
}
