// <copyright file="IJobWriter.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Domain.Data.CvSections.JobHistory
{
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Job Writer.
    /// </summary>
    public interface IJobWriter
    {
        /// <summary>
        /// Deletes the selected content item.
        /// </summary>
        /// <param name="cvId">The cv identifier.</param>
        /// <param name="employer">The name of the employer.</param>
        /// <param name="jobTitle">The name of the job title.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public Task DeleteAsync(
            Guid cvId,
            string employer,
            string jobTitle);
    }
}
