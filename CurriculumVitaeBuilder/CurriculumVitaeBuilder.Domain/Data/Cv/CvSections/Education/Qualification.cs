// <copyright file="Qualification.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Domain.Data.Cv.CvSections.Education
{
    using System;

    /// <summary>
    /// Qualification.
    /// </summary>
    public class Qualification
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Qualification"/> class.
        /// </summary>
        /// <param name="name">The qualification name.</param>
        /// <param name="grade">The grade.</param>
        public Qualification(
            string name,
            string grade)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException(nameof(name));
            }

            if (string.IsNullOrWhiteSpace(grade))
            {
                throw new ArgumentException(nameof(grade));
            }

            this.Name = name;
            this.Grade = grade;
        }

        /// <summary>
        /// Gets the qualification name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the grade.
        /// </summary>
        public string Grade { get; }
    }
}
