// <copyright file="SkillsProfileSectionDocument.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Infrastructure.Data.Marten.CvSections.SkillsProfile
{
    using System;
    using System.Collections.Generic;

    using CurriculumVitaeBuilder.Domain.Data.CvSections.SkillsProfile;

    /// <summary>
    /// Contact Section document.
    /// </summary>
    public class SkillsProfileSectionDocument : CvSectionDocument
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SkillsProfileSectionDocument"/> class.
        /// </summary>
        /// <param name="id">The section identifier.</param>
        /// <param name="cvId">The CV identifier.</param>
        /// <param name="skills">The collection of skills.</param>
        public SkillsProfileSectionDocument(
            Guid id,
            Guid cvId,
            IList<Skill> skills)
            : base(id, cvId)
        {
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
