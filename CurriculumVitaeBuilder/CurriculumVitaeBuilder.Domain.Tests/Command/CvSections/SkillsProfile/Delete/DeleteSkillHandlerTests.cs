// <copyright file="DeleteSkillHandlerTests.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Domain.Tests.Command.CvSections.SkillsProfile.Delete
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Chest.Core.Command;
    using Chest.Core.Exceptions;

    using CurriculumVitaeBuilder.Domain.Command.CvSections.SkillsProfile.DeleteSkill;
    using CurriculumVitaeBuilder.Domain.Data;
    using CurriculumVitaeBuilder.Domain.Data.CvSections;
    using CurriculumVitaeBuilder.Domain.Data.CvSections.SkillsProfile;

    using NSubstitute;
    using NSubstitute.ReturnsExtensions;

    using Xunit;

    public class DeleteSkillHandlerTests
    {
        [Fact]
        public async void Handle_CvDoesntExist_Throws()
        {
            // Arrange
            var cvSectionContentWriter = Substitute.For<ICvSectionContentWriter<SkillsProfileSection>>();
            var cvSectionReader = Substitute.For<ICvSectionReader<SkillsProfileSection>>();
            var cvReader = Substitute.For<ICvReader>();

            var handler = new DeleteSkillHandler(
                cvSectionContentWriter,
                cvSectionReader,
                cvReader);

            var metadata = new CommandMetadata("DeleteSkill", DateTime.UtcNow, Guid.NewGuid().ToString());

            var command = new DeleteSkill(
                Guid.NewGuid(),
                "test");

            cvReader
                .GetCvExistsAsync(command.CvId)
                .Returns(false);

            // Act
            // Assert
            var ex = await Assert.ThrowsAsync<InvalidCommandException>(
                async () => await handler.Handle(command, metadata));

            Assert.Contains(
                ex.ValidationErrors.SelectMany(v => v.Value),
                v => v == $"CV does not exist with cvId: {command.CvId}.");
        }

        [Fact]
        public async void Handle_CvSectionDoesntExist_Throws()
        {
            // Arrange
            // Arrange
            var cvSectionContentWriter = Substitute.For<ICvSectionContentWriter<SkillsProfileSection>>();
            var cvSectionReader = Substitute.For<ICvSectionReader<SkillsProfileSection>>();
            var cvReader = Substitute.For<ICvReader>();

            var handler = new DeleteSkillHandler(
               cvSectionContentWriter,
               cvSectionReader,
               cvReader);

            var metadata = new CommandMetadata("DeleteSkill", DateTime.UtcNow, Guid.NewGuid().ToString());

            var command = new DeleteSkill(
                Guid.NewGuid(),
                "test");

            cvReader
                .GetCvExistsAsync(command.CvId)
                .Returns(true);

            cvSectionReader
                .GetSectionByCvAsync(command.CvId)
                .ReturnsNull();

            // Act
            // Assert
            var ex = await Assert.ThrowsAsync<InvalidCommandException>(
                async () => await handler.Handle(command, metadata));

            Assert.Contains(
                ex.ValidationErrors.SelectMany(v => v.Value),
                v => v == "CV section doesn't exist.");
        }

        [Fact]
        public async void Handle_ValidCommand_DeletesSkillsProfileEstablishment()
        {
            // Arrange
            var cvSectionContentWriter = Substitute.For<ICvSectionContentWriter<SkillsProfileSection>>();
            var cvSectionReader = Substitute.For<ICvSectionReader<SkillsProfileSection>>();
            var cvReader = Substitute.For<ICvReader>();

            var handler = new DeleteSkillHandler(
               cvSectionContentWriter,
               cvSectionReader,
               cvReader);

            var metadata = new CommandMetadata("DeleteSkill", DateTime.UtcNow, Guid.NewGuid().ToString());

            var command = new DeleteSkill(
                Guid.NewGuid(),
                "test");

            cvReader
                .GetCvExistsAsync(command.CvId)
                .Returns(true);

            cvSectionReader
                .GetSectionByCvAsync(command.CvId)
                .Returns(new SkillsProfileSection(
                    Guid.NewGuid(),
                    command.CvId,
                    new List<Skill>
                    {
                        new Skill(
                            "test1",
                            "test2",
                            DateTime.Now),
                    }));

            // Act
            // Assert
            await handler.Handle(command, metadata);

            await cvSectionContentWriter.Received().DeleteAsync(
                command.CvId,
                command.Skill);
        }
    }
}
