// <copyright file="JobHistorySection.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Domain.Data.Cv.CvSections
{
    using System;
    using System.Collections.Generic;

    using CurriculumVitaeBuilder.Domain.Data.Cv.CvSections.JobHistory;

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
            IReadOnlyCollection<Job> jobs)
            : base(id, cvId)
        {
            this.Job = jobs
                ?? throw new ArgumentNullException(nameof(jobs));

            this.Title = "Job History";
        }

        /// <summary>
        /// Gets the collection of jobs.
        /// </summary>
        public IReadOnlyCollection<Job> Job { get; }

        /// <summary>
        /// Gets or sets the Title.
        /// </summary>
        public override string Title { get; set; }
    }
}
