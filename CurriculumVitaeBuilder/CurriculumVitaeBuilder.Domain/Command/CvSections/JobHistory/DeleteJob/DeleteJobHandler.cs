// <copyright file="DeleteJobHandler.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Domain.Command.CvSections.JobHistory.DeleteJob
{
    using System;
    using System.Threading.Tasks;

    using Chest.Core.Command;
    using Chest.Core.Exceptions;

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
        public DeleteJobHandler(
            IJobWriter jobWriter,
            ICvSectionReader<JobHistorySection> cvSectionReader)
        {
            this.JobWriter = jobWriter
                ?? throw new ArgumentNullException(nameof(jobWriter));

            this.CvSectionReader = cvSectionReader
                ?? throw new ArgumentNullException(nameof(cvSectionReader));
        }

        private IJobWriter JobWriter { get; }

        private ICvSectionReader<JobHistorySection> CvSectionReader { get; }

        /// <inheritdoc/>
        public async Task Handle(DeleteJob command, CommandMetadata metadata)
        {
            if (command.CvId == default)
            {
                throw new InvalidCommandException(
                   metadata.CommandName,
                   typeof(DeleteJob).Name,
                   $"CV Id must be set on the command.");
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
