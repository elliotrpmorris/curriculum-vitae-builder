// <copyright file="UpdateJobHistorySectionHandler.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Domain.Command.CvSections.JobHistory.Update
{
    using System;
    using System.Threading.Tasks;

    using Chest.Core.Command;
    using Chest.Core.Exceptions;

    using CurriculumVitaeBuilder.Domain.Data.CvSections;

    /// <summary>
    /// Create Section Handler.
    /// </summary>
    public class UpdateJobHistorySectionHandler : ICommandHandler<UpdateJobHistorySection>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateJobHistorySectionHandler"/> class.
        /// </summary>
        /// <param name="cvSectionReader">The CV section reader.</param>
        /// <param name="cvSectionWriter">The CV section writer.</param>
        public UpdateJobHistorySectionHandler(
            ICvSectionReader<JobHistorySection> cvSectionReader,
            ICvSectionWriter<JobHistorySection> cvSectionWriter)
        {
            this.CvSectionReader = cvSectionReader
                ?? throw new ArgumentNullException(nameof(cvSectionReader));

            this.CvSectionWriter = cvSectionWriter
                ?? throw new ArgumentNullException(nameof(cvSectionWriter));
        }

        private ICvSectionReader<JobHistorySection> CvSectionReader { get; }

        private ICvSectionWriter<JobHistorySection> CvSectionWriter { get; }

        /// <inheritdoc/>
        public async Task Handle(UpdateJobHistorySection command, CommandMetadata metadata)
        {
            if (command.UserId == default)
            {
                throw new InvalidCommandException(
                    metadata.CommandName,
                    typeof(UpdateJobHistorySection).Name,
                    $"User Id must be set on the command.");
            }

            if (command.CvId == default)
            {
                throw new InvalidCommandException(
                   metadata.CommandName,
                   typeof(UpdateJobHistorySection).Name,
                   $"CV Id must be set on the command.");
            }

            var section = await
                this.CvSectionReader.GetSectionByCvAsync(command.CvId);

            if (section == null)
            {
                throw new InvalidCommandException(
                  metadata.CommandName,
                  typeof(UpdateJobHistorySection).Name,
                  $"CV section doesn't exists.");
            }

            await this.CvSectionWriter.UpdateAsync(
                new JobHistorySection(
                    section.Id,
                    section.CvId,
                    command.Jobs));
        }
    }
}
