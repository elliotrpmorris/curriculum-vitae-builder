// <copyright file="EducationEstablishment.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Domain.Data.Cv.CvSections.Education
{
    using System;

    /// <summary>
    /// Education Establishment.
    /// </summary>
    public class EducationEstablishment
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EducationEstablishment"/> class.
        /// </summary>
        /// <param name="name">The establishment name.</param>
        /// <param name="start">The start date at the establishment.</param>
        /// <param name="end">The end date at the establishment.</param>
        public EducationEstablishment(
            string name,
            DateTime start,
            DateTime end)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException(nameof(name));
            }

            if (start == default)
            {
                throw new ArgumentException(nameof(start));
            }

            if (end == default)
            {
                throw new ArgumentException(nameof(end));
            }

            this.Name = name;
            this.Start = start;
            this.End = end;
        }

        /// <summary>
        /// Gets the establishment name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the start date at the establishment.
        /// </summary>
        public DateTime Start { get; }

        /// <summary>
        /// Gets the end date at the establishment.
        /// </summary>
        public DateTime End { get; }
    }
}
