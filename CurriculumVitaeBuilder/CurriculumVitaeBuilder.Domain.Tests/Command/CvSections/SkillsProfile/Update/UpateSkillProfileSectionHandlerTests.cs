// <copyright file="UpateSkillProfileSectionHandlerTests.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Domain.Tests.Command.CvSections.SkillsProfile.Update
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Chest.Core.Command;
    using Chest.Core.Exceptions;

    using CurriculumVitaeBuilder.Domain.Command.CvSections.SkillsProfile.Update;
    using CurriculumVitaeBuilder.Domain.Data;
    using CurriculumVitaeBuilder.Domain.Data.CvSections;
    using CurriculumVitaeBuilder.Domain.Data.CvSections.SkillsProfile;

    using NSubstitute;
    using NSubstitute.ReturnsExtensions;

    using Xunit;

    public class UpateSkillProfileSectionHandlerTests
    {
        [Fact]
        public async void Handle_CvDoesntExist_Throws()
        {
            // Arrange
            var cvSectionReader = Substitute.For<ICvSectionReader<SkillsProfileSection>>();
            var cvSectionWriter = Substitute.For<ICvSectionWriter<SkillsProfileSection>>();
            var cvReader = Substitute.For<ICvReader>();

            var handler = new UpdateSkillsProfileSectionHandler(
                cvSectionReader,
                cvSectionWriter,
                cvReader);

            var metadata = new CommandMetadata("UpdateSkillsProfileSection", DateTime.UtcNow, Guid.NewGuid().ToString());

            var command = new UpdateSkillsProfileSection(
                Guid.NewGuid(),
                Guid.NewGuid(),
                new List<Skill>
                {
                    new Skill(
                        "test1",
                        "test2",
                        null),
                });

            cvReader
                .GetCvExistsAsync(command.CvId)
                .Returns(false);

            // Act
            // Assert
            var ex = await Assert.ThrowsAsync<InvalidCommandException>(
                async () => await handler.Handle(command, metadata));

            Assert.Contains(
                ex.ValidationErrors.SelectMany(v => v.Value),
                v => v == $"CV does not exist for user: {command.UserId}.");
        }

        [Fact]
        public async void Handle_CvSectionDoesntExist_Throws()
        {
            // Arrange
            var cvSectionReader = Substitute.For<ICvSectionReader<SkillsProfileSection>>();
            var cvSectionWriter = Substitute.For<ICvSectionWriter<SkillsProfileSection>>();
            var cvReader = Substitute.For<ICvReader>();

            var handler = new UpdateSkillsProfileSectionHandler(
                cvSectionReader,
                cvSectionWriter,
                cvReader);

            var metadata = new CommandMetadata("UpdateSkillsProfileSection", DateTime.UtcNow, Guid.NewGuid().ToString());

            var command = new UpdateSkillsProfileSection(
                Guid.NewGuid(),
                Guid.NewGuid(),
                new List<Skill>
                {
                    new Skill(
                        "test1",
                        "test2",
                        null),
                });

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
                v => v == "CV section doesn't exists.");
        }

        [Fact]
        public async void Handle_ValidCommand_UpdatesSkillsProfileSection()
        {
            // Arrange
            var cvSectionReader = Substitute.For<ICvSectionReader<SkillsProfileSection>>();
            var cvSectionWriter = Substitute.For<ICvSectionWriter<SkillsProfileSection>>();
            var cvReader = Substitute.For<ICvReader>();

            var handler = new UpdateSkillsProfileSectionHandler(
                cvSectionReader,
                cvSectionWriter,
                cvReader);

            var metadata = new CommandMetadata("UpdateSkillsProfileSection", DateTime.UtcNow, Guid.NewGuid().ToString());

            var command = new UpdateSkillsProfileSection(
                Guid.NewGuid(),
                Guid.NewGuid(),
                new List<Skill>
                {
                    new Skill(
                        "test1",
                        "test2",
                        null),
                });

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
                            null),
                    }));

            // Act
            // Assert
            await handler.Handle(command, metadata);

            var expectedArgs =
                Arg.Is<SkillsProfileSection>(u =>
                u.CvId == command.CvId &&
                u.Skills == command.Skills);

            await cvSectionWriter.Received().UpdateAsync(expectedArgs);
        }
    }
}
