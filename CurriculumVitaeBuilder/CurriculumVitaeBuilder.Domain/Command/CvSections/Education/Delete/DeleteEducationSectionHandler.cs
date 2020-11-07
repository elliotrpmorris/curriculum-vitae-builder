﻿// <copyright file="DeleteEducationSectionHandler.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Domain.Command.CvSections.Education.Delete
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
    public class DeleteEducationSectionHandler : ICommandHandler<DeleteEducationSection>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteEducationSectionHandler"/> class.
        /// </summary>
        /// <param name="cvSectionReader">The CV section reader.</param>
        /// <param name="cvSectionWriter">The CV section writer.</param>
        public DeleteEducationSectionHandler(
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
        public async Task Handle(DeleteEducationSection command, CommandMetadata metadata)
        {
            if (command.UserId == default)
            {
                throw new InvalidCommandException(
                    metadata.CommandName,
                    typeof(DeleteEducationSection).Name,
                    $"User Id must be set on the command.");
            }

            if (command.CvId == default)
            {
                throw new InvalidCommandException(
                   metadata.CommandName,
                   typeof(DeleteEducationSection).Name,
                   $"CV Id must be set on the command.");
            }

            var section = await
               this.CvSectionReader.GetSectionByCvAsync(command.CvId);

            if (section == null)
            {
                throw new InvalidCommandException(
                  metadata.CommandName,
                  typeof(DeleteEducationSection).Name,
                  $"CV section doesn't exists.");
            }

            await this.CvSectionWriter.DeleteAsync(section);
        }
    }
}
