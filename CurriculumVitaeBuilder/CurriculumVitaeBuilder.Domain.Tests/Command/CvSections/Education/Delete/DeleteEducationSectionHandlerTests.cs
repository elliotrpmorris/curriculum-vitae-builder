// <copyright file="DeleteEducationSectionHandlerTests.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Domain.Tests.Command.CvSections.Education.Delete
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Chest.Core.Command;
    using Chest.Core.Exceptions;

    using CurriculumVitaeBuilder.Domain.Command.CvSections.Education.Delete;
    using CurriculumVitaeBuilder.Domain.Data;
    using CurriculumVitaeBuilder.Domain.Data.CvSections;
    using CurriculumVitaeBuilder.Domain.Data.CvSections.Education;

    using NSubstitute;
    using NSubstitute.ReturnsExtensions;

    using Xunit;

    public class DeleteEducationSectionHandlerTests
    {
        [Fact]
        public async void Handle_CvDoesntExist_Throws()
        {
            // Arrange
            var cvSectionReader = Substitute.For<ICvSectionReader<EducationSection>>();
            var cvSectionWriter = Substitute.For<ICvSectionWriter<EducationSection>>();
            var cvReader = Substitute.For<ICvReader>();

            var handler = new DeleteEducationSectionHandler(
                cvSectionReader,
                cvSectionWriter,
                cvReader);

            var metadata = new CommandMetadata("DeleteEducationSection", DateTime.UtcNow, Guid.NewGuid().ToString());

            var command = new DeleteEducationSection(
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
            var cvSectionReader = Substitute.For<ICvSectionReader<EducationSection>>();
            var cvSectionWriter = Substitute.For<ICvSectionWriter<EducationSection>>();
            var cvReader = Substitute.For<ICvReader>();

            var handler = new DeleteEducationSectionHandler(
                cvSectionReader,
                cvSectionWriter,
                cvReader);

            var metadata = new CommandMetadata("DeleteEducationSection", DateTime.UtcNow, Guid.NewGuid().ToString());

            var command = new DeleteEducationSection(
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
        public async void Handle_ValidCommand_DeletesEducationSection()
        {
            // Arrange
            var cvSectionReader = Substitute.For<ICvSectionReader<EducationSection>>();
            var cvSectionWriter = Substitute.For<ICvSectionWriter<EducationSection>>();
            var cvReader = Substitute.For<ICvReader>();

            var handler = new DeleteEducationSectionHandler(
                cvSectionReader,
                cvSectionWriter,
                cvReader);

            var metadata = new CommandMetadata("DeleteEducationSection", DateTime.UtcNow, Guid.NewGuid().ToString());

            var command = new DeleteEducationSection(
                Guid.NewGuid(),
                Guid.NewGuid());

            cvReader
                .GetCvExistsAsync(command.CvId)
                .Returns(true);

            cvSectionReader
                .GetSectionByCvAsync(command.CvId)
                .Returns(new EducationSection(
                    Guid.NewGuid(),
                    command.CvId,
                    new List<EducationEstablishment>
                    {
                        new EducationEstablishment(
                            "test",
                            DateTime.Now,
                            DateTime.Now),
                    }));

            // Act
            // Assert
            await handler.Handle(command, metadata);

            var expectedArgs =
                Arg.Is<EducationSection>(u =>
                u.CvId == command.CvId);

            await cvSectionWriter.Received().DeleteAsync(expectedArgs);
        }
    }
}
