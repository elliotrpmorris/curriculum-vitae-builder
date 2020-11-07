// <copyright file="DeleteJob.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Domain.Command.CvSections.JobHistory.DeleteJob
{
    using System;

    using Chest.Core.Command;

    /// <summary>
    /// Delete Job Command.
    /// </summary>
    [CommandName("CVSECTION/DELETE_JOB")]
    public class DeleteJob : ICommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteJob"/> class.
        /// </summary>
        /// <param name="cvId">The CV identifier.</param>
        /// <param name="contactDetail">The contact detail name.</param>
        /// <param name="employer">The employer.</param>
        /// <param name="jobTitle">The job title.</param>
        public DeleteJob(
            Guid cvId,
            string employer,
            string jobTitle)
        {
            this.CvId = cvId;
            this.Employer = employer;
            this.JobTitle = jobTitle;
        }

        /// <summary>
        /// Gets the CV identifier.
        /// </summary>
        public Guid CvId { get; }

        /// <summary>
        /// Gets the employer.
        /// </summary>
        public string Employer { get; }

        /// <summary>
        /// Gets the job title.
        /// </summary>
        public string JobTitle { get; }
    }
}
