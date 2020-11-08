// <copyright file="DeleteEducationEstablishmentHandler.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Domain.Command.CvSections.Education.DeleteEducationEstablishment
{
    using System;
    using System.Threading.Tasks;

    using Chest.Core.Command;
    using Chest.Core.Exceptions;

    using CurriculumVitaeBuilder.Domain.Data;
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
        /// <param name="cvReader">The CV reader.</param>
        public DeleteEducationEstablishmentHandler(
            ICvSectionContentWriter<EducationSection> cvSectionContentWriter,
            ICvSectionReader<EducationSection> cvSectionReader,
            ICvReader cvReader)
        {
            this.CvSectionContentWriter = cvSectionContentWriter
                ?? throw new ArgumentNullException(nameof(cvSectionContentWriter));

            this.CvSectionReader = cvSectionReader
                ?? throw new ArgumentNullException(nameof(cvSectionReader));

            this.CvReader = cvReader
                ?? throw new ArgumentNullException(nameof(cvReader));
        }

        private ICvSectionContentWriter<EducationSection> CvSectionContentWriter { get; }

        private ICvSectionReader<EducationSection> CvSectionReader { get; }

        private ICvReader CvReader { get; }

        /// <inheritdoc/>
        public async Task Handle(DeleteEducationEstablishment command, CommandMetadata metadata)
        {
            var cvExists = await
               this.CvReader.GetCvExistsAsync(command.CvId);

            if (!cvExists)
            {
                throw new InvalidCommandException(
                  metadata.CommandName,
                  typeof(DeleteEducationEstablishment).Name,
                  $"CV does not exist with cvId: {command.CvId}.");
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
