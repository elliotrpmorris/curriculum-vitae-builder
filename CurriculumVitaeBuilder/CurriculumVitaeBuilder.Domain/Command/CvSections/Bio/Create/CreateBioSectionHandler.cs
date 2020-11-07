// <copyright file="CreateBioSectionHandler.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Domain.Command.CvSections.Bio.Create
{
    using System;
    using System.Threading.Tasks;

    using Chest.Core.Command;
    using Chest.Core.Exceptions;

    using CurriculumVitaeBuilder.Domain.Data;
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
        /// <param name="cvSectionWriter">The CV section writer.</param>
        /// <param name="cvReader">The CV reader.</param>
        public CreateBioSectionHandler(
            ICvSectionReader<BioSection> cvSectionReader,
            ICvSectionWriter<BioSection> cvSectionWriter,
            ICvReader cvReader)
        {
            this.CvSectionReader = cvSectionReader
                ?? throw new ArgumentNullException(nameof(cvSectionReader));

            this.CvSectionWriter = cvSectionWriter
                ?? throw new ArgumentNullException(nameof(cvSectionWriter));

            this.CvReader = cvReader
                ?? throw new ArgumentNullException(nameof(cvReader));
        }

        private ICvSectionReader<BioSection> CvSectionReader { get; }

        private ICvSectionWriter<BioSection> CvSectionWriter { get; }

        private ICvReader CvReader { get; }

        /// <inheritdoc/>
        public async Task Handle(CreateBioSection command, CommandMetadata metadata)
        {
            var cvExists = await
                this.CvReader.GetCvExistsAsync(command.CvId);

            if (!cvExists)
            {
                throw new InvalidCommandException(
                  metadata.CommandName,
                  typeof(CreateBioSection).Name,
                  $"CV does not exist for user: {command.UserId}.");
            }

            var sectionExists = await
                this.CvSectionReader.GetSectionExistsAsync(command.CvId);

            if (sectionExists)
            {
                throw new InvalidCommandException(
                  metadata.CommandName,
                  typeof(CreateBioSection).Name,
                  "CV section already exists.");
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
