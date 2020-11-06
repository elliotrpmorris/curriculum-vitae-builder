// <copyright file="Skill.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Domain.Data.CvSections.SkillsProfile
{
    using System;

    /// <summary>
    /// Skill.
    /// </summary>
    public class Skill
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Skill"/> class.
        /// </summary>
        /// <param name="name">The skill name.</param>
        /// <param name="description">The description of the skill.</param>
        /// <param name="achievedAt">The optional achieved at date.</param>
        public Skill(
            string name,
            string description,
            DateTime? achievedAt)
        {
            if (name == default)
            {
                throw new ArgumentException(nameof(name));
            }

            if (string.IsNullOrWhiteSpace(description))
            {
                throw new ArgumentException(nameof(description));
            }

            this.Name = name;
            this.Description = description;
            this.AchieveAt = achievedAt;
        }

        /// <summary>
        /// Gets the skill name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the description of the skill.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// Gets the optional achieved at date.
        /// </summary>
        public DateTime? AchieveAt { get; }
    }
}
