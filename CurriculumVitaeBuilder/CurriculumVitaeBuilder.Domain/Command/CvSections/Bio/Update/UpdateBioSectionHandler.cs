// <copyright file="UpdateBioSectionHandler.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Domain.Command.CvSections.Bio.Update
{
    using System;
    using System.Threading.Tasks;

    using Chest.Core.Command;
    using Chest.Core.Exceptions;

    using CurriculumVitaeBuilder.Domain.Data.CvSections;
    using CurriculumVitaeBuilder.Domain.Data.CvSections.Bio;

    /// <summary>
    /// Create Section Handler.
    /// </summary>
    public class UpdateBioSectionHandler : ICommandHandler<UpdateBioSection>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateBioSectionHandler"/> class.
        /// </summary>
        /// <param name="cvSectionReader">The CV section reader.</param>
        /// <param name="cvSectionWriter">The CV section writer.</param>
        public UpdateBioSectionHandler(
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
        public async Task Handle(UpdateBioSection command, CommandMetadata metadata)
        {
            if (command.UserId == default)
            {
                throw new InvalidCommandException(
                    metadata.CommandName,
                    typeof(UpdateBioSection).Name,
                    $"User Id must be set on the command.");
            }

            if (command.CvId == default)
            {
                throw new InvalidCommandException(
                   metadata.CommandName,
                   typeof(UpdateBioSection).Name,
                   $"CV Id must be set on the command.");
            }

            var section = await
                this.CvSectionReader.GetSectionByCvAsync(command.CvId);

            if (section == null)
            {
                throw new InvalidCommandException(
                  metadata.CommandName,
                  typeof(UpdateBioSection).Name,
                  $"CV section doesn't exists.");
            }

            await this.CvSectionWriter.UpdateAsync(
                new BioSection(
                    section.Id,
                    section.CvId,
                    command.FullName,
                    command.City));
        }
    }
}
