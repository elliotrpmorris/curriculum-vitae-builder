// <copyright file="DeleteSkillHandler.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Domain.Command.CvSections.SkillsProfile.DeleteSkill
{
    using System;
    using System.Threading.Tasks;

    using Chest.Core.Command;
    using Chest.Core.Exceptions;

    using CurriculumVitaeBuilder.Domain.Data.CvSections;
    using CurriculumVitaeBuilder.Domain.Data.CvSections.SkillsProfile;

    /// <summary>
    /// Delete Skill Handler.
    /// </summary>
    public class DeleteSkillHandler : ICommandHandler<DeleteSkill>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteSkillHandler"/> class.
        /// </summary>
        /// <param name="cvSectionContentWriter">The CV section content writer.</param>
        /// <param name="cvSectionReader">The CV section reader.</param>
        public DeleteSkillHandler(
            ICvSectionContentWriter<SkillsProfileSection> cvSectionContentWriter,
            ICvSectionReader<SkillsProfileSection> cvSectionReader)
        {
            this.CvSectionContentWriter = cvSectionContentWriter
                ?? throw new ArgumentNullException(nameof(cvSectionContentWriter));

            this.CvSectionReader = cvSectionReader
                ?? throw new ArgumentNullException(nameof(cvSectionReader));
        }

        private ICvSectionContentWriter<SkillsProfileSection> CvSectionContentWriter { get; }

        private ICvSectionReader<SkillsProfileSection> CvSectionReader { get; }

        /// <inheritdoc/>
        public async Task Handle(DeleteSkill command, CommandMetadata metadata)
        {
            if (command.CvId == default)
            {
                throw new InvalidCommandException(
                   metadata.CommandName,
                   typeof(DeleteSkill).Name,
                   $"CV Id must be set on the command.");
            }

            var section = await
              this.CvSectionReader.GetSectionByCvAsync(command.CvId);

            if (section == null)
            {
                throw new InvalidCommandException(
                  metadata.CommandName,
                  typeof(DeleteSkill).Name,
                  $"CV section doesn't exist.");
            }

            await this.CvSectionContentWriter.DeleteAsync(
                command.CvId,
                command.Skill);
        }
    }
}
