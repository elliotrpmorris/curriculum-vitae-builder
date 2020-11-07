// <copyright file="CreateEducationSectionHandler.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Domain.Command.CvSections.Education.Create
{
    using System;
    using System.Threading.Tasks;

    using Chest.Core.Command;
    using Chest.Core.Exceptions;

    using CurriculumVitaeBuilder.Domain.Data.CvSections;
    using CurriculumVitaeBuilder.Domain.Data.CvSections.Education;

    /// <summary>
    /// Create Section Handler.
    /// </summary>
    public class CreateEducationSectionHandler : ICommandHandler<CreateEducationSection>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateEducationSectionHandler"/> class.
        /// </summary>
        /// <param name="cvSectionReader">The CV section reader.</param>
        /// <param name="cvSectionWriter">The CV section writer.</param>
        public CreateEducationSectionHandler(
            ICvSectionReader<EducationSection> cvSectionReader,
            ICvSectionWriter<EducationSection> cvSectionWriter)
        {
            this.CvSectionReader = cvSectionReader
                ?? throw new ArgumentNullException(nameof(cvSectionReader));

            this.CvSectionWriter = cvSectionWriter
                ?? throw new ArgumentNullException(nameof(cvSectionWriter));
        }

        private ICvSectionReader<EducationSection> CvSectionReader { get; }

        private ICvSectionWriter<EducationSection> CvSectionWriter { get; }

        /// <inheritdoc/>
        public async Task Handle(CreateEducationSection command, CommandMetadata metadata)
        {
            if (command.UserId == default)
            {
                throw new InvalidCommandException(
                    metadata.CommandName,
                    typeof(CreateEducationSection).Name,
                    $"User Id must be set on the command.");
            }

            if (command.CvId == default)
            {
                throw new InvalidCommandException(
                   metadata.CommandName,
                   typeof(CreateEducationSection).Name,
                   $"CV Id must be set on the command.");
            }

            var exists = await
                this.CvSectionReader.GetSectionExistsAsync(command.CvId);

            if (exists)
            {
                throw new InvalidCommandException(
                  metadata.CommandName,
                  typeof(CreateEducationSection).Name,
                  $"CV section already exists.");
            }

            await this.CvSectionWriter.AddAsync(
                new EducationSection(
                    Guid.NewGuid(),
                    command.CvId,
                    command.EducationEstablishments));
        }
    }
}
