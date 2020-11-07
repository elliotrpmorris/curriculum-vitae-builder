// <copyright file="CreateJobHistorySection.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Domain.Command.CvSections.JobHistory.Create
{
    using System;
    using System.Collections.Generic;

    using Chest.Core.Command;

    using CurriculumVitaeBuilder.Domain.Data.CvSections.JobHistory;

    /// <summary>
    /// Create Section Command.
    /// </summary>
    [CommandName("CVSECTION/CREATE_JOBHISTORY_SECTION")]
    public class CreateJobHistorySection : ICommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateJobHistorySection"/> class.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="cvId">The cv identifier.</param>
        /// <param name="jobs">The collection of jobs.</param>
        public CreateJobHistorySection(
            Guid userId,
            Guid cvId,
            IList<Job> jobs)
        {
            this.UserId = userId;
            this.CvId = cvId;

            this.Jobs = jobs
               ?? throw new ArgumentNullException(nameof(jobs));
        }

        /// <summary>
        /// Gets the user identifier.
        /// </summary>
        public Guid UserId { get; }

        /// <summary>
        /// Gets the CV identifier.
        /// </summary>
        public Guid CvId { get; }

        /// <summary>
        /// Gets the collection of jobs.
        /// </summary>
        public IList<Job> Jobs { get; }
    }
}
