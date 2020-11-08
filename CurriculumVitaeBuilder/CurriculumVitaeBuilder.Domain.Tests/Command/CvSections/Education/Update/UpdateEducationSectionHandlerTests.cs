// <copyright file="UpdateEducationSectionHandlerTests.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Domain.Tests.Command.CvSections.Education.Update
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Chest.Core.Command;
    using Chest.Core.Exceptions;

    using CurriculumVitaeBuilder.Domain.Command.CvSections.Bio.Create;
    using CurriculumVitaeBuilder.Domain.Command.CvSections.Bio.Update;
    using CurriculumVitaeBuilder.Domain.Command.CvSections.Education.Update;
    using CurriculumVitaeBuilder.Domain.Data;
    using CurriculumVitaeBuilder.Domain.Data.CvSections;
    using CurriculumVitaeBuilder.Domain.Data.CvSections.Bio;
    using CurriculumVitaeBuilder.Domain.Data.CvSections.Education;
    using NSubstitute;
    using NSubstitute.ReturnsExtensions;
    using Xunit;

    public class UpdateEducationSectionHandlerTests
    {
        [Fact]
        public async void Handle_CvDoesntExist_Throws()
        {
            // Arrange
            var cvSectionReader = Substitute.For<ICvSectionReader<EducationSection>>();
            var cvSectionWriter = Substitute.For<ICvSectionWriter<EducationSection>>();
            var cvReader = Substitute.For<ICvReader>();

            var handler = new UpdateEducationSectionHandler(
                cvSectionReader,
                cvSectionWriter,
                cvReader);

            var metadata = new CommandMetadata("UpdateEducationSection", DateTime.UtcNow, Guid.NewGuid().ToString());

            var command = new UpdateEducationSection(
                Guid.NewGuid(),
                Guid.NewGuid(),
                new List<EducationEstablishment>
                {
                    new EducationEstablishment(
                        "test",
                        DateTime.Now,
                        DateTime.Now),
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
            var cvSectionReader = Substitute.For<ICvSectionReader<EducationSection>>();
            var cvSectionWriter = Substitute.For<ICvSectionWriter<EducationSection>>();
            var cvReader = Substitute.For<ICvReader>();

            var handler = new UpdateEducationSectionHandler(
                cvSectionReader,
                cvSectionWriter,
                cvReader);

            var metadata = new CommandMetadata("UpdateEducationSection", DateTime.UtcNow, Guid.NewGuid().ToString());

            var command = new UpdateEducationSection(
                Guid.NewGuid(),
                Guid.NewGuid(),
                new List<EducationEstablishment>
                {
                    new EducationEstablishment(
                        "test",
                        DateTime.Now,
                        DateTime.Now),
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
        public async void Handle_ValidCommand_UpdatesEducationSection()
        {
            // Arrange
            var cvSectionReader = Substitute.For<ICvSectionReader<EducationSection>>();
            var cvSectionWriter = Substitute.For<ICvSectionWriter<EducationSection>>();
            var cvReader = Substitute.For<ICvReader>();

            var handler = new UpdateEducationSectionHandler(
                cvSectionReader,
                cvSectionWriter,
                cvReader);

            var metadata = new CommandMetadata("UpdateEducationSection", DateTime.UtcNow, Guid.NewGuid().ToString());

            var command = new UpdateEducationSection(
                Guid.NewGuid(),
                Guid.NewGuid(),
                new List<EducationEstablishment>
                {
                    new EducationEstablishment(
                        "test",
                        DateTime.Now,
                        DateTime.Now),
                });

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
                u.CvId == command.CvId &&
                u.EducationEstablishments == command.EducationEstablishments);

            await cvSectionWriter.Received().UpdateAsync(expectedArgs);
        }
    }
}
