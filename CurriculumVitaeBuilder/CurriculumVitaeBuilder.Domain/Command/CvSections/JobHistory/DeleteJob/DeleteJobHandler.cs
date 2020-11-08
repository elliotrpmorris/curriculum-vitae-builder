// <copyright file="DeleteJobHandler.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Domain.Command.CvSections.JobHistory.DeleteJob
{
    using System;
    using System.Threading.Tasks;

    using Chest.Core.Command;
    using Chest.Core.Exceptions;

    using CurriculumVitaeBuilder.Domain.Data;
    using CurriculumVitaeBuilder.Domain.Data.CvSections;
    using CurriculumVitaeBuilder.Domain.Data.CvSections.JobHistory;

    /// <summary>
    /// Delete Job Handler.
    /// </summary>
    public class DeleteJobHandler : ICommandHandler<DeleteJob>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteJobHandler"/> class.
        /// </summary>
        /// <param name="jobWriter">The Job writer.</param>
        /// <param name="cvSectionReader">The CV section reader.</param>
        /// <param name="cvReader">The CV reader.</param>
        public DeleteJobHandler(
            IJobWriter jobWriter,
            ICvSectionReader<JobHistorySection> cvSectionReader,
            ICvReader cvReader)
        {
            this.JobWriter = jobWriter
                ?? throw new ArgumentNullException(nameof(jobWriter));

            this.CvSectionReader = cvSectionReader
                ?? throw new ArgumentNullException(nameof(cvSectionReader));

            this.CvReader = cvReader
                ?? throw new ArgumentNullException(nameof(cvReader));
        }

        private IJobWriter JobWriter { get; }

        private ICvSectionReader<JobHistorySection> CvSectionReader { get; }

        private ICvReader CvReader { get; }

        /// <inheritdoc/>
        public async Task Handle(DeleteJob command, CommandMetadata metadata)
        {
            var cvExists = await
              this.CvReader.GetCvExistsAsync(command.CvId);

            if (!cvExists)
            {
                throw new InvalidCommandException(
                  metadata.CommandName,
                  typeof(DeleteJob).Name,
                  $"CV does not exist with cvId: {command.CvId}.");
            }

            var section = await
              this.CvSectionReader.GetSectionByCvAsync(command.CvId);

            if (section == null)
            {
                throw new InvalidCommandException(
                  metadata.CommandName,
                  typeof(DeleteJob).Name,
                  $"CV section doesn't exist.");
            }

            await this.JobWriter.DeleteAsync(
                command.CvId,
                command.Employer,
                command.JobTitle);
        }
    }
}
