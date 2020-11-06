// <copyright file="CreateSectionHandler.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Domain.Command.CvSections.Create
{
    using System;
    using System.Threading.Tasks;

    using Chest.Core.Command;
    using Chest.Core.Exceptions;

    using CurriculumVitaeBuilder.Domain.Command.CvSections.BioSection.Create;
    using CurriculumVitaeBuilder.Domain.Data.CvSections;
    using CurriculumVitaeBuilder.Domain.Data.CvSections.Bio;

    /// <summary>
    /// Create Section Handler.
    /// </summary>
    public class CreateBioSectionHandler : ICommandHandler<CreateBioSection>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateBioSectionHandler"/> class.
        /// </summary>
        /// <param name="cvSectionReader">The CV section reader.</param>
        /// <param name="cvSectionWriter">The CV section writer,</param>
        public CreateBioSectionHandler(
            ICvSectionReader<BioSection> cvSectionReader,
            ICvSectionWriter<BioSection> cvSectionWriter)
        {
            this.CvSectionReader = cvSectionReader
                ?? throw new ArgumentNullException(nameof(cvSectionReader));

            this.CvSectionWriter = cvSectionWriter
                ?? throw new ArgumentNullException(nameof(cvSectionWriter));
        }

        private ICvSectionReader<BioSection> CvSectionReader { get; }

        private ICvSectionWriter<BioSection> CvSectionWriter { get; }

        /// <inheritdoc/>
        public async Task Handle(CreateBioSection command, CommandMetadata metadata)
        {
            if (command.UserId == default)
            {
                throw new InvalidCommandException(
                    metadata.CommandName,
                    typeof(CreateBioSection).Name,
                    $"User Id must be set on the command.");
            }

            if (command.CvId == default)
            {
                throw new InvalidCommandException(
                   metadata.CommandName,
                   typeof(CreateBioSection).Name,
                   $"CV Id must be set on the command.");
            }

            var exists = await
                this.CvSectionReader.GetSectionExistsAsync(command.CvId);

            if (exists)
            {
                throw new InvalidCommandException(
                  metadata.CommandName,
                  typeof(CreateBioSection).Name,
                  $"CV section already exists.");
            }

            await this.CvSectionWriter.AddAsync(
                new BioSection(
                    Guid.NewGuid(),
                    command.CvId,
                    command.FullName,
                    command.City));
        }
    }
}
