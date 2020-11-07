// <copyright file="CreateBioSectionHandlerTests.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Domain.Tests.Command.CvSections.Bio.Create
{
    using System;
    using System.Linq;

    using Chest.Core.Command;
    using Chest.Core.Exceptions;

    using CurriculumVitaeBuilder.Domain.Command.CvSections.Bio.Create;
    using CurriculumVitaeBuilder.Domain.Data;
    using CurriculumVitaeBuilder.Domain.Data.CvSections;
    using CurriculumVitaeBuilder.Domain.Data.CvSections.Bio;

    using NSubstitute;

    using Xunit;

    public class CreateBioSectionHandlerTests
    {
        [Fact]
        public async void Handle_CvDoesntExist_Throws()
        {
            // Arrange
            var cvSectionReader = Substitute.For<ICvSectionReader<BioSection>>();
            var cvSectionWriter = Substitute.For<ICvSectionWriter<BioSection>>();
            var cvReader = Substitute.For<ICvReader>();

            var handler = new CreateBioSectionHandler(
                cvSectionReader,
                cvSectionWriter,
                cvReader);

            var metadata = new CommandMetadata("CreateBioSection", DateTime.UtcNow, Guid.NewGuid().ToString());

            var command = new CreateBioSection(
                Guid.NewGuid(),
                Guid.NewGuid(),
                "Test Name",
                "Test City");

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
            var cvSectionReader = Substitute.For<ICvSectionReader<BioSection>>();
            var cvSectionWriter = Substitute.For<ICvSectionWriter<BioSection>>();
            var cvReader = Substitute.For<ICvReader>();

            var handler = new CreateBioSectionHandler(
                cvSectionReader,
                cvSectionWriter,
                cvReader);

            var metadata = new CommandMetadata("CreateBioSection", DateTime.UtcNow, Guid.NewGuid().ToString());

            var command = new CreateBioSection(
                Guid.NewGuid(),
                Guid.NewGuid(),
                "Test Name",
                "Test City");

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
        public async void Handle_ValidCommand_CreatesBioSection()
        {
            // Arrange
            var cvSectionReader = Substitute.For<ICvSectionReader<BioSection>>();
            var cvSectionWriter = Substitute.For<ICvSectionWriter<BioSection>>();
            var cvReader = Substitute.For<ICvReader>();

            var handler = new CreateBioSectionHandler(
                cvSectionReader,
                cvSectionWriter,
                cvReader);

            var metadata = new CommandMetadata("CreateBioSection", DateTime.UtcNow, Guid.NewGuid().ToString());

            var command = new CreateBioSection(
                Guid.NewGuid(),
                Guid.NewGuid(),
                "Test Name",
                "Test City");

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
                Arg.Is<BioSection>(u =>
                u.CvId == command.CvId &&
                u.FullName == command.FullName &&
                u.City == command.City);

            await cvSectionWriter.Received().AddAsync(expectedArgs);
        }
    }
}
