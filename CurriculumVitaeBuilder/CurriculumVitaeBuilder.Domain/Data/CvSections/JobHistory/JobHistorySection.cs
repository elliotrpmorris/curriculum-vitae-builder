// <copyright file="JobHistorySection.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Domain.Data.CvSections
{
    using System;
    using System.Collections.Generic;

    using CurriculumVitaeBuilder.Domain.Data.CvSections.JobHistory;

    /// <summary>
    /// Job History section.
    /// </summary>
    public class JobHistorySection : CvSection
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JobHistorySection"/> class.
        /// </summary>
        /// <param name="id">The section identifier.</param>
        /// <param name="cvId">The CV identifier.</param>
        /// <param name="jobs">The collection of jobs.</param>
        public JobHistorySection(
            Guid id,
            Guid cvId,
            IList<Job> jobs)
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

            this.Jobs = jobs
                ?? throw new ArgumentNullException(nameof(jobs));

            this.Title = "Job History";
        }

        /// <summary>
        /// Gets the collection of jobs.
        /// </summary>
        public IList<Job> Jobs { get; }

        /// <summary>
        /// Gets or sets the Title.
        /// </summary>
        public override string Title { get; set; }
    }
}
