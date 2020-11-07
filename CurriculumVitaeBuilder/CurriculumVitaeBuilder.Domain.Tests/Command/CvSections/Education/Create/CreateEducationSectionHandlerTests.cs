// <copyright file="CreateEducationSectionHandlerTests.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Domain.Tests.Command.CvSections.Education.Create
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Chest.Core.Command;
    using Chest.Core.Exceptions;

    using CurriculumVitaeBuilder.Domain.Command.CvSections.Education.Create;
    using CurriculumVitaeBuilder.Domain.Data;
    using CurriculumVitaeBuilder.Domain.Data.CvSections;
    using CurriculumVitaeBuilder.Domain.Data.CvSections.Education;

    using NSubstitute;

    using Xunit;

    public class CreateEducationSectionHandlerTests
    {
        [Fact]
        public async void Handle_CvDoesntExist_Throws()
        {
            // Arrange
            var cvSectionReader = Substitute.For<ICvSectionReader<EducationSection>>();
            var cvSectionWriter = Substitute.For<ICvSectionWriter<EducationSection>>();
            var cvReader = Substitute.For<ICvReader>();

            var handler = new CreateEducationSectionHandler(
                cvSectionReader,
                cvSectionWriter,
                cvReader);

            var metadata = new CommandMetadata("CreateEducationSection", DateTime.UtcNow, Guid.NewGuid().ToString());

            var educationEstablishments = new List<EducationEstablishment>();

            var command = new CreateEducationSection(
                Guid.NewGuid(),
                Guid.NewGuid(),
                educationEstablishments);

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

            var handler = new CreateEducationSectionHandler(
                cvSectionReader,
                cvSectionWriter,
                cvReader);

            var metadata = new CommandMetadata("CreateEducationSection", DateTime.UtcNow, Guid.NewGuid().ToString());

            var educationEstablishments = new List<EducationEstablishment>();

            var command = new CreateEducationSection(
                Guid.NewGuid(),
                Guid.NewGuid(),
                educationEstablishments);

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
        public async void Handle_ValidCommand_CreatesEducationSection()
        {
            // Arrange
            var cvSectionReader = Substitute.For<ICvSectionReader<EducationSection>>();
            var cvSectionWriter = Substitute.For<ICvSectionWriter<EducationSection>>();
            var cvReader = Substitute.For<ICvReader>();

            var handler = new CreateEducationSectionHandler(
                cvSectionReader,
                cvSectionWriter,
                cvReader);

            var metadata = new CommandMetadata("CreateEducationSection", DateTime.UtcNow, Guid.NewGuid().ToString());

            var educationEstablishments = new List<EducationEstablishment>();

            var command = new CreateEducationSection(
                Guid.NewGuid(),
                Guid.NewGuid(),
                educationEstablishments);
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
                Arg.Is<EducationSection>(u =>
                u.CvId == command.CvId &&
                u.EducationEstablishments == command.EducationEstablishments);

            await cvSectionWriter.Received().AddAsync(expectedArgs);
        }
    }
}
