// <copyright file="SkillsProfileSection.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Domain.Data.CvSections.SkillsProfile
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Skills Profile section.
    /// </summary>
    public class SkillsProfileSection : CvSection
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SkillsProfileSection"/> class.
        /// </summary>
        /// <param name="id">The section identifier.</param>
        /// <param name="cvId">The CV identifier.</param>
        /// <param name="skills">The collection of skills.</param>
        public SkillsProfileSection(
            Guid id,
            Guid cvId,
            IList<Skill> skills)
            : base(id, cvId)
        {
            if (id == default)
            {
                throw new ArgumentException(nameof(id));
            }

            if (cvId == default)
            {
                throw new ArgumentException(nameof(cvId));
            }

            this.Skills = skills
                ?? throw new ArgumentNullException(nameof(skills));

            this.Title = "Skills Profile";
        }

        /// <summary>
        /// Gets the collection of skills.
        /// </summary>
        public IList<Skill> Skills { get; }

        /// <summary>
        /// Gets or sets the Title.
        /// </summary>
        public override string Title { get; set; }
    }
}
