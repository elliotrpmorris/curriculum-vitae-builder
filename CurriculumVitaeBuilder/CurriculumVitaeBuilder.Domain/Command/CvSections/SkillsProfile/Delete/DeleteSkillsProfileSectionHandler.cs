// <copyright file="DeleteSkillsProfileSectionHandler.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Domain.Command.CvSections.SkillsProfile.Delete
{
    using System;
    using System.Threading.Tasks;

    using Chest.Core.Command;
    using Chest.Core.Exceptions;

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
        public DeleteSkillsProfileSectionHandler(
            ICvSectionReader<SkillsProfileSection> cvSectionReader,
            ICvSectionWriter<SkillsProfileSection> cvSectionWriter)
        {
            this.CvSectionReader = cvSectionReader
                ?? throw new ArgumentNullException(nameof(cvSectionReader));

            this.CvSectionWriter = cvSectionWriter
                ?? throw new ArgumentNullException(nameof(cvSectionWriter));
        }

        private ICvSectionReader<SkillsProfileSection> CvSectionReader { get; }

        private ICvSectionWriter<SkillsProfileSection> CvSectionWriter { get; }

        /// <inheritdoc/>
        public async Task Handle(DeleteSkillsProfileSection command, CommandMetadata metadata)
        {
            if (command.UserId == default)
            {
                throw new InvalidCommandException(
                    metadata.CommandName,
                    typeof(DeleteSkillsProfileSection).Name,
                    $"User Id must be set on the command.");
            }

            if (command.CvId == default)
            {
                throw new InvalidCommandException(
                   metadata.CommandName,
                   typeof(DeleteSkillsProfileSection).Name,
                   $"CV Id must be set on the command.");
            }

            var section = await
               this.CvSectionReader.GetSectionByCvAsync(command.CvId);

            if (section == null)
            {
                throw new InvalidCommandException(
                  metadata.CommandName,
                  typeof(DeleteSkillsProfileSection).Name,
                  $"CV section doesn't exists.");
            }

            await this.CvSectionWriter.DeleteAsync(section);
        }
    }
}
