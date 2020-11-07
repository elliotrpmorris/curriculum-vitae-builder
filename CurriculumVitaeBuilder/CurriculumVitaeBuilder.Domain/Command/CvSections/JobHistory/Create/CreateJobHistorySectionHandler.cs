// <copyright file="CreateJobHistorySectionHandler.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Domain.Command.CvSections.JobHistory.Create
{
    using System;
    using System.Threading.Tasks;

    using Chest.Core.Command;
    using Chest.Core.Exceptions;

    using CurriculumVitaeBuilder.Domain.Data;
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
        /// <param name="cvReader">The CV reader.</param>
        public CreateJobHistorySectionHandler(
            ICvSectionReader<JobHistorySection> cvSectionReader,
            ICvSectionWriter<JobHistorySection> cvSectionWriter,
            ICvReader cvReader)
        {
            this.CvSectionReader = cvSectionReader
                ?? throw new ArgumentNullException(nameof(cvSectionReader));

            this.CvSectionWriter = cvSectionWriter
                ?? throw new ArgumentNullException(nameof(cvSectionWriter));

            this.CvReader = cvReader
               ?? throw new ArgumentNullException(nameof(cvReader));
        }

        private ICvSectionReader<JobHistorySection> CvSectionReader { get; }

        private ICvSectionWriter<JobHistorySection> CvSectionWriter { get; }

        private ICvReader CvReader { get; }

        /// <inheritdoc/>
        public async Task Handle(CreateJobHistorySection command, CommandMetadata metadata)
        {
            var cvExists = await
                this.CvReader.GetCvExistsAsync(command.CvId);

            if (!cvExists)
            {
                throw new InvalidCommandException(
                  metadata.CommandName,
                  typeof(CreateJobHistorySection).Name,
                  $"CV does not exist for user: {command.UserId}.");
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
