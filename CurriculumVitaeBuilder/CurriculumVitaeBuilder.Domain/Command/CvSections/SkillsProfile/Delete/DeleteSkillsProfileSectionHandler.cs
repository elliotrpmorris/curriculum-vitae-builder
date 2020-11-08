// <copyright file="DeleteSkillsProfileSectionHandler.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Domain.Command.CvSections.SkillsProfile.Delete
{
    using System;
    using System.Threading.Tasks;

    using Chest.Core.Command;
    using Chest.Core.Exceptions;

    using CurriculumVitaeBuilder.Domain.Data;
    using CurriculumVitaeBuilder.Domain.Data.CvSections;
    using CurriculumVitaeBuilder.Domain.Data.CvSections.SkillsProfile;

    /// <summary>
    /// Create Section Handler.
    /// </summary>
    public class DeleteSkillsProfileSectionHandler : ICommandHandler<DeleteSkillsProfileSection>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteSkillsProfileSectionHandler"/> class.
        /// </summary>
        /// <param name="cvSectionReader">The CV section reader.</param>
        /// <param name="cvSectionWriter">The CV section writer.</param>
        /// <param name="cvReader">The CV reader.</param>
        public DeleteSkillsProfileSectionHandler(
            ICvSectionReader<SkillsProfileSection> cvSectionReader,
            ICvSectionWriter<SkillsProfileSection> cvSectionWriter,
            ICvReader cvReader)
        {
            this.CvSectionReader = cvSectionReader
                ?? throw new ArgumentNullException(nameof(cvSectionReader));

            this.CvSectionWriter = cvSectionWriter
                ?? throw new ArgumentNullException(nameof(cvSectionWriter));

            this.CvReader = cvReader
                ?? throw new ArgumentNullException(nameof(cvReader));
        }

        private ICvSectionReader<SkillsProfileSection> CvSectionReader { get; }

        private ICvSectionWriter<SkillsProfileSection> CvSectionWriter { get; }

        private ICvReader CvReader { get; }

        /// <inheritdoc/>
        public async Task Handle(DeleteSkillsProfileSection command, CommandMetadata metadata)
        {
            var cvExists = await
                this.CvReader.GetCvExistsAsync(command.CvId);

            if (!cvExists)
            {
                throw new InvalidCommandException(
                  metadata.CommandName,
                  typeof(DeleteSkillsProfileSection).Name,
                  $"CV does not exist for user: {command.UserId}.");
            }

            var section = await
               this.CvSectionReader.GetSectionByCvAsync(command.CvId);

            if (section == null)
            {
                throw new InvalidCommandException(
                  metadata.CommandName,
                  typeof(DeleteSkillsProfileSection).Name,
                  $"CV section doesn't exist.");
            }

            await this.CvSectionWriter.DeleteAsync(section);
        }
    }
}
