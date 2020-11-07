// <copyright file="DeleteJobHistorySectionHandler.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Domain.Command.CvSections.JobHistory.Delete
{
    using System;
    using System.Threading.Tasks;

    using Chest.Core.Command;
    using Chest.Core.Exceptions;

    using CurriculumVitaeBuilder.Domain.Data.CvSections;

    /// <summary>
    /// Create Section Handler.
    /// </summary>
    public class DeleteJobHistorySectionHandler : ICommandHandler<DeleteJobHistorySection>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteJobHistorySectionHandler"/> class.
        /// </summary>
        /// <param name="cvSectionReader">The CV section reader.</param>
        /// <param name="cvSectionWriter">The CV section writer.</param>
        public DeleteJobHistorySectionHandler(
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
        public async Task Handle(DeleteJobHistorySection command, CommandMetadata metadata)
        {
            if (command.UserId == default)
            {
                throw new InvalidCommandException(
                    metadata.CommandName,
                    typeof(DeleteJobHistorySection).Name,
                    $"User Id must be set on the command.");
            }

            if (command.CvId == default)
            {
                throw new InvalidCommandException(
                   metadata.CommandName,
                   typeof(DeleteJobHistorySection).Name,
                   $"CV Id must be set on the command.");
            }

            var section = await
               this.CvSectionReader.GetSectionByCvAsync(command.CvId);

            if (section == null)
            {
                throw new InvalidCommandException(
                  metadata.CommandName,
                  typeof(DeleteJobHistorySection).Name,
                  $"CV section doesn't exists.");
            }

            await this.CvSectionWriter.DeleteAsync(section);
        }
    }
}
