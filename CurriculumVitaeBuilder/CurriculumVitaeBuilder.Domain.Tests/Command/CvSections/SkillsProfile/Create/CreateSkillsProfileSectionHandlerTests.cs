// <copyright file="CreateSkillsProfileSectionHandlerTests.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Domain.Tests.Command.CvSections.SkillsProfile.Create
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Chest.Core.Command;
    using Chest.Core.Exceptions;

    using CurriculumVitaeBuilder.Domain.Command.CvSections.SkillsProfile.Create;
    using CurriculumVitaeBuilder.Domain.Data;
    using CurriculumVitaeBuilder.Domain.Data.CvSections;
    using CurriculumVitaeBuilder.Domain.Data.CvSections.SkillsProfile;

    using NSubstitute;

    using Xunit;

    public class CreateSkillsProfileSectionHandlerTests
    {
        [Fact]
        public async void Handle_CvDoesntExist_Throws()
        {
            // Arrange
            var cvSectionReader = Substitute.For<ICvSectionReader<SkillsProfileSection>>();
            var cvSectionWriter = Substitute.For<ICvSectionWriter<SkillsProfileSection>>();
            var cvReader = Substitute.For<ICvReader>();

            var handler = new CreateSkillsProfileSectionHandler(
                cvSectionReader,
                cvSectionWriter,
                cvReader);

            var metadata = new CommandMetadata("CreateSkillsProfileSection", DateTime.UtcNow, Guid.NewGuid().ToString());

            var skills = new List<Skill>();

            var command = new CreateSkillsProfileSection(
                Guid.NewGuid(),
                Guid.NewGuid(),
                skills);

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

            var handler = new CreateSkillsProfileSectionHandler(
                cvSectionReader,
                cvSectionWriter,
                cvReader);

            var metadata = new CommandMetadata("CreateSkillsProfileSection", DateTime.UtcNow, Guid.NewGuid().ToString());

            var skills = new List<Skill>();

            var command = new CreateSkillsProfileSection(
                Guid.NewGuid(),
                Guid.NewGuid(),
                skills);

            cvReader
                .GetCvExistsAsync(command.CvId)
                .Returns(true);

            cvSectionReader
               .GetSectionExistsAsync(command.CvId)
               .Returns(true);

            // Act
            // Assert
            var ex = await Assert.ThrowsAsync<InvalidCommandException>(
                async () => await handler.Handle(command, metadata));

            Assert.Contains(
                ex.ValidationErrors.SelectMany(v => v.Value),
                v => v == "CV section already exists.");
        }

        [Fact]
        public async void Handle_ValidCommand_CreatesSkillsProfileSection()
        {
            // Arrange
            var cvSectionReader = Substitute.For<ICvSectionReader<SkillsProfileSection>>();
            var cvSectionWriter = Substitute.For<ICvSectionWriter<SkillsProfileSection>>();
            var cvReader = Substitute.For<ICvReader>();

            var handler = new CreateSkillsProfileSectionHandler(
                cvSectionReader,
                cvSectionWriter,
                cvReader);

            var metadata = new CommandMetadata("CreateSkillsProfileSection", DateTime.UtcNow, Guid.NewGuid().ToString());

            var skills = new List<Skill>();

            var command = new CreateSkillsProfileSection(
                Guid.NewGuid(),
                Guid.NewGuid(),
                skills);

            cvReader
                .GetCvExistsAsync(command.CvId)
                .Returns(true);

            cvSectionReader
              .GetSectionExistsAsync(command.CvId)
              .Returns(false);

            // Act
            // Assert
            await handler.Handle(command, metadata);

            var expectedArgs =
                Arg.Is<SkillsProfileSection>(u =>
                u.CvId == command.CvId &&
                u.Skills == command.Skills);

            await cvSectionWriter.Received().AddAsync(expectedArgs);
        }
    }
}
