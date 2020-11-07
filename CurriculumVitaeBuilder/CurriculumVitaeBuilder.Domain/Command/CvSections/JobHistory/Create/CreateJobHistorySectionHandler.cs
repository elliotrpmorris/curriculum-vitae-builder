// <copyright file="CreateJobHistorySectionHandler.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Domain.Command.CvSections.JobHistory.Create
{
    using System;
    using System.Threading.Tasks;

    using Chest.Core.Command;
    using Chest.Core.Exceptions;

    using CurriculumVitaeBuilder.Domain.Data.CvSections;

    /// <summary>
    /// Create Section Handler.
    /// </summary>
    public class CreateJobHistorySectionHandler : ICommandHandler<CreateJobHistorySection>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateJobHistorySectionHandler"/> class.
        /// </summary>
        /// <param name="cvSectionReader">The CV section reader.</param>
        /// <param name="cvSectionWriter">The CV section writer.</param>
        public CreateJobHistorySectionHandler(
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
        public async Task Handle(CreateJobHistorySection command, CommandMetadata metadata)
        {
            if (command.UserId == default)
            {
                throw new InvalidCommandException(
                    metadata.CommandName,
                    typeof(CreateJobHistorySection).Name,
                    $"User Id must be set on the command.");
            }

            if (command.CvId == default)
            {
                throw new InvalidCommandException(
                   metadata.CommandName,
                   typeof(CreateJobHistorySection).Name,
                   $"CV Id must be set on the command.");
            }

            var exists = await
                this.CvSectionReader.GetSectionExistsAsync(command.CvId);

            if (exists)
            {
                throw new InvalidCommandException(
                  metadata.CommandName,
                  typeof(CreateJobHistorySection).Name,
                  $"CV section already exists.");
            }

            await this.CvSectionWriter.AddAsync(
                new JobHistorySection(
                    Guid.NewGuid(),
                    command.CvId,
                    command.Jobs));
        }
    }
}
