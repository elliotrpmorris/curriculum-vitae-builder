// <copyright file="SkillsProfileSection.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Domain.Data.Cv.CvSections.SkillsProfile
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
            IReadOnlyCollection<Skill> skills)
            : base(id, cvId)
        {
            this.Skils = skills
                ?? throw new ArgumentNullException(nameof(skills));

            this.Title = "Skills Profile";
        }

        /// <summary>
        /// Gets the collection of skills.
        /// </summary>
        public IReadOnlyCollection<Skill> Skils { get; }

        /// <summary>
        /// Gets or sets the Title.
        /// </summary>
        public override string Title { get; set; }
    }
}
