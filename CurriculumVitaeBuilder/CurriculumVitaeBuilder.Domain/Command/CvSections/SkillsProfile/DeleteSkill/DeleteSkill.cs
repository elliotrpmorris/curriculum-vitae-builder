// <copyright file="DeleteSkill.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Domain.Command.CvSections.SkillsProfile.DeleteSkill
{
    using System;

    using Chest.Core.Command;

    /// <summary>
    /// Delete Skill.
    /// </summary>
    [CommandName("CVSECTION/DELETE_SKILL")]
    public class DeleteSkill : ICommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteSkill"/> class.
        /// </summary>
        /// <param name="cvId">The CV identifier.</param>
        /// <param name="skill">The skill name.</param>
        public DeleteSkill(
            Guid cvId,
            string skill)
        {
            this.CvId = cvId;
            this.Skill = skill;
        }

        /// <summary>
        /// Gets the CV identifier.
        /// </summary>
        public Guid CvId { get; }

        /// <summary>
        /// Gets the contact detail name.
        /// </summary>
        public string Skill { get; }
    }
}
