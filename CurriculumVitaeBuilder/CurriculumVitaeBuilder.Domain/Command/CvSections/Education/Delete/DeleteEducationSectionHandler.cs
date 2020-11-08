// <copyright file="DeleteEducationSectionHandler.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Domain.Command.CvSections.Education.Delete
{
    using System;
    using System.Threading.Tasks;

    using Chest.Core.Command;
    using Chest.Core.Exceptions;

    using CurriculumVitaeBuilder.Domain.Data;
    using CurriculumVitaeBuilder.Domain.Data.CvSections;
    using CurriculumVitaeBuilder.Domain.Data.CvSections.Education;

    /// <summary>
    /// Create Section Handler.
    /// </summary>
    public class DeleteEducationSectionHandler : ICommandHandler<DeleteEducationSection>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteEducationSectionHandler"/> class.
        /// </summary>
        /// <param name="cvSectionReader">The CV section reader.</param>
        /// <param name="cvSectionWriter">The CV section writer.</param>
        /// <param name="cvReader">The CV reader.</param>
        public DeleteEducationSectionHandler(
            ICvSectionReader<EducationSection> cvSectionReader,
            ICvSectionWriter<EducationSection> cvSectionWriter,
            ICvReader cvReader)
        {
            this.CvSectionReader = cvSectionReader
                ?? throw new ArgumentNullException(nameof(cvSectionReader));

            this.CvSectionWriter = cvSectionWriter
                ?? throw new ArgumentNullException(nameof(cvSectionWriter));

            this.CvReader = cvReader
                ?? throw new ArgumentNullException(nameof(cvReader));
        }

        private ICvSectionReader<EducationSection> CvSectionReader { get; }

        private ICvSectionWriter<EducationSection> CvSectionWriter { get; }

        private ICvReader CvReader { get; }

        /// <inheritdoc/>
        public async Task Handle(DeleteEducationSection command, CommandMetadata metadata)
        {
            var cvExists = await
               this.CvReader.GetCvExistsAsync(command.CvId);

            if (!cvExists)
            {
                throw new InvalidCommandException(
                  metadata.CommandName,
                  typeof(DeleteEducationSection).Name,
                  $"CV does not exist for user: {command.UserId}.");
            }

            var section = await
               this.CvSectionReader.GetSectionByCvAsync(command.CvId);

            if (section == null)
            {
                throw new InvalidCommandException(
                  metadata.CommandName,
                  typeof(DeleteEducationSection).Name,
                  $"CV section doesn't exist.");
            }

            await this.CvSectionWriter.DeleteAsync(section);
        }
    }
}
