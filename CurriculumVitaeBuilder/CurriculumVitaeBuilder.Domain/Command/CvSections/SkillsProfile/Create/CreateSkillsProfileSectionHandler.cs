// <copyright file="CreateSkillsProfileSectionHandler.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Domain.Command.CvSections.SkillsProfile.Create
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
    public class CreateSkillsProfileSectionHandler : ICommandHandler<CreateSkillsProfileSection>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateSkillsProfileSectionHandler"/> class.
        /// </summary>
        /// <param name="cvSectionReader">The CV section reader.</param>
        /// <param name="cvSectionWriter">The CV section writer.</param>
        public CreateSkillsProfileSectionHandler(
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
        public async Task Handle(CreateSkillsProfileSection command, CommandMetadata metadata)
        {
            if (command.UserId == default)
            {
                throw new InvalidCommandException(
                    metadata.CommandName,
                    typeof(CreateSkillsProfileSection).Name,
                    $"User Id must be set on the command.");
            }

            if (command.CvId == default)
            {
                throw new InvalidCommandException(
                   metadata.CommandName,
                   typeof(CreateSkillsProfileSection).Name,
                   $"CV Id must be set on the command.");
            }

            var exists = await
                this.CvSectionReader.GetSectionExistsAsync(command.CvId);

            if (exists)
            {
                throw new InvalidCommandException(
                  metadata.CommandName,
                  typeof(CreateSkillsProfileSection).Name,
                  $"CV section already exists.");
            }

            await this.CvSectionWriter.AddAsync(
                new SkillsProfileSection(
                    Guid.NewGuid(),
                    command.CvId,
                    command.Skills));
        }
    }
}
