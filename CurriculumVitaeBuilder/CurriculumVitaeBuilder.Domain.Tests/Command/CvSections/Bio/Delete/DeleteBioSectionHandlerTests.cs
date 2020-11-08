// <copyright file="DeleteBioSectionHandlerTests.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Domain.Tests.Command.CvSections.Bio.Delete
{
    using System;
    using System.Linq;
    using Chest.Core.Command;
    using Chest.Core.Exceptions;
    using CurriculumVitaeBuilder.Domain.Command.CvSections.Bio.Delete;
    using CurriculumVitaeBuilder.Domain.Data;
    using CurriculumVitaeBuilder.Domain.Data.CvSections;
    using CurriculumVitaeBuilder.Domain.Data.CvSections.Bio;
    using NSubstitute;
    using NSubstitute.ReturnsExtensions;
    using Xunit;

    public class DeleteBioSectionHandlerTests
    {
        [Fact]
        public async void Handle_CvDoesntExist_Throws()
        {
            // Arrange
            var cvSectionReader = Substitute.For<ICvSectionReader<BioSection>>();
            var cvSectionWriter = Substitute.For<ICvSectionWriter<BioSection>>();
            var cvReader = Substitute.For<ICvReader>();

            var handler = new DeleteBioSectionHandler(
                cvSectionReader,
                cvSectionWriter,
                cvReader);

            var metadata = new CommandMetadata("DeleteBioSection", DateTime.UtcNow, Guid.NewGuid().ToString());

            var command = new DeleteBioSection(
                Guid.NewGuid(),
                Guid.NewGuid());

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

            var handler = new DeleteBioSectionHandler(
                cvSectionReader,
                cvSectionWriter,
                cvReader);

            var metadata = new CommandMetadata("DeleteBioSection", DateTime.UtcNow, Guid.NewGuid().ToString());

            var command = new DeleteBioSection(
                Guid.NewGuid(),
                Guid.NewGuid());

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
        public async void Handle_ValidCommand_DeletesBioSection()
        {
            // Arrange
            var cvSectionReader = Substitute.For<ICvSectionReader<BioSection>>();
            var cvSectionWriter = Substitute.For<ICvSectionWriter<BioSection>>();
            var cvReader = Substitute.For<ICvReader>();

            var handler = new DeleteBioSectionHandler(
                cvSectionReader,
                cvSectionWriter,
                cvReader);

            var metadata = new CommandMetadata("DeleteBioSection", DateTime.UtcNow, Guid.NewGuid().ToString());

            var command = new DeleteBioSection(
                Guid.NewGuid(),
                Guid.NewGuid());

            cvReader
                .GetCvExistsAsync(command.CvId)
                .Returns(true);

            cvSectionReader
                .GetSectionByCvAsync(command.CvId)
                .Returns(new BioSection(
                    Guid.NewGuid(),
                    command.CvId,
                    "test",
                    "test"));

            // Act
            // Assert
            await handler.Handle(command, metadata);

            var expectedArgs =
                Arg.Is<BioSection>(u =>
                u.CvId == command.CvId);

            await cvSectionWriter.Received().DeleteAsync(expectedArgs);
        }
    }
}
