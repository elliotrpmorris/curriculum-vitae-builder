// <copyright file="Job.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Domain.Data.CvSections.JobHistory
{
    using System;

    /// <summary>
    /// Job.
    /// </summary>
    public class Job
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Job"/> class.
        /// </summary>
        /// <param name="employer">The employer name.</param>
        /// <param name="start">The start date at the job.</param>
        /// <param name="end">The end date at the job.</param>
        /// <param name="jobTitle">The job title.</param>
        /// <param name="description">The description of the role.</param>
        public Job(
            string employer,
            DateTime start,
            DateTime end,
            string jobTitle,
            string description)
        {
            if (string.IsNullOrWhiteSpace(employer))
            {
                throw new ArgumentException(nameof(employer));
            }

            if (start == default)
            {
                throw new ArgumentException(nameof(start));
            }

            if (end == default)
            {
                throw new ArgumentException(nameof(end));
            }

            if (start > end)
            {
                throw new ArgumentException("Start cant be greater than end");
            }

            if (string.IsNullOrWhiteSpace(jobTitle))
            {
                throw new ArgumentException(nameof(jobTitle));
            }

            if (string.IsNullOrWhiteSpace(description))
            {
                throw new ArgumentException(nameof(description));
            }

            this.Employer = employer;
            this.Start = start;
            this.End = end;
            this.JobTitle = jobTitle;

            this.Description = description
                ?? throw new ArgumentNullException(nameof(description));
        }

        /// <summary>
        /// Gets the employer name.
        /// </summary>
        public string Employer { get; }

        /// <summary>
        /// Gets the start date at the job.
        /// </summary>
        public DateTime Start { get; }

        /// <summary>
        /// Gets the end date at the job.
        /// </summary>
        public DateTime End { get; }

        /// <summary>
        /// Gets the job title.
        /// </summary>
        public string JobTitle { get; }

        /// <summary>
        /// Gets the description of the role.
        /// </summary>
        public string Description { get; }
    }
}
