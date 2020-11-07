// <copyright file="JobHistorySectionDocument.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Infrastructure.Data.Marten.CvSections.JobHistory
{
    using System;
    using System.Collections.Generic;

    using CurriculumVitaeBuilder.Domain.Data.CvSections.JobHistory;

    /// <summary>
    /// Contact Section document.
    /// </summary>
    public class JobHistorySectionDocument : CvSectionDocument
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JobHistorySectionDocument"/> class.
        /// </summary>
        /// <param name="id">The section identifier.</param>
        /// <param name="cvId">The CV identifier.</param>
        /// <param name="jobs">The collection of jobs.</param>
        public JobHistorySectionDocument(
            Guid id,
            Guid cvId,
            IList<Job> jobs)
            : base(id, cvId)
        {
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
