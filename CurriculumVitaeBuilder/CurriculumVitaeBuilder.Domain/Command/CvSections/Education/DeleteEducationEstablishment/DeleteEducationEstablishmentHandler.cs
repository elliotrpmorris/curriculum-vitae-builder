// <copyright file="DeleteEducationEstablishmentHandler.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Domain.Command.CvSections.Education.DeleteEducationEstablishment
{
    using System;
    using System.Threading.Tasks;

    using Chest.Core.Command;
    using Chest.Core.Exceptions;

    using CurriculumVitaeBuilder.Domain.Data.CvSections;
    using CurriculumVitaeBuilder.Domain.Data.CvSections.Education;

    /// <summary>
    /// Delete Education EstablishmentHandler.
    /// </summary>
    public class DeleteEducationEstablishmentHandler : ICommandHandler<DeleteEducationEstablishment>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteEducationEstablishmentHandler"/> class.
        /// </summary>
        /// <param name="cvSectionContentWriter">The CV section content writer.</param>
        /// <param name="cvSectionReader">The CV section reader.</param>
        public DeleteEducationEstablishmentHandler(
            ICvSectionContentWriter<EducationSection> cvSectionContentWriter,
            ICvSectionReader<EducationSection> cvSectionReader)
        {
            this.CvSectionContentWriter = cvSectionContentWriter
                ?? throw new ArgumentNullException(nameof(cvSectionContentWriter));

            this.CvSectionReader = cvSectionReader
                ?? throw new ArgumentNullException(nameof(cvSectionReader));
        }

        private ICvSectionContentWriter<EducationSection> CvSectionContentWriter { get; }

        private ICvSectionReader<EducationSection> CvSectionReader { get; }

        /// <inheritdoc/>
        public async Task Handle(DeleteEducationEstablishment command, CommandMetadata metadata)
        {
            if (command.CvId == default)
            {
                throw new InvalidCommandException(
                   metadata.CommandName,
                   typeof(DeleteEducationEstablishment).Name,
                   $"CV Id must be set on the command.");
            }

            var section = await
              this.CvSectionReader.GetSectionByCvAsync(command.CvId);

            if (section == null)
            {
                throw new InvalidCommandException(
                  metadata.CommandName,
                  typeof(DeleteEducationEstablishment).Name,
                  $"CV section doesn't exist.");
            }

            await this.CvSectionContentWriter.DeleteAsync(
                command.CvId,
                command.EducationEstablishment);
        }
    }
}
