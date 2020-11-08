// <copyright file="DeleteEducationEstablishmentHandlerTests.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Domain.Tests.Command.CvSections.Education
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Chest.Core.Command;
    using Chest.Core.Exceptions;
    using CurriculumVitaeBuilder.Domain.Command.CvSections.Education.Delete;
    using CurriculumVitaeBuilder.Domain.Command.CvSections.Education.DeleteEducationEstablishment;
    using CurriculumVitaeBuilder.Domain.Data;
    using CurriculumVitaeBuilder.Domain.Data.CvSections;
    using CurriculumVitaeBuilder.Domain.Data.CvSections.Education;
    using NSubstitute;
    using NSubstitute.ReturnsExtensions;
    using Xunit;

    public class DeleteEducationEstablishmentHandlerTests
    {
        [Fact]
        public async void Handle_CvDoesntExist_Throws()
        {
            // Arrange
            var cvSectionContentWriter = Substitute.For<ICvSectionContentWriter<EducationSection>>();
            var cvSectionReader = Substitute.For<ICvSectionReader<EducationSection>>();
            var cvReader = Substitute.For<ICvReader>();

            var handler = new DeleteEducationEstablishmentHandler(
                cvSectionContentWriter,
                cvSectionReader,
                cvReader);

            var metadata = new CommandMetadata("DeleteEducationEstablishment", DateTime.UtcNow, Guid.NewGuid().ToString());

            var command = new DeleteEducationEstablishment(
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
            var cvSectionContentWriter = Substitute.For<ICvSectionContentWriter<EducationSection>>();
            var cvSectionReader = Substitute.For<ICvSectionReader<EducationSection>>();
            var cvReader = Substitute.For<ICvReader>();

            var handler = new DeleteEducationEstablishmentHandler(
                cvSectionContentWriter,
                cvSectionReader,
                cvReader);

            var metadata = new CommandMetadata("DeleteEducationEstablishment", DateTime.UtcNow, Guid.NewGuid().ToString());

            var command = new DeleteEducationEstablishment(
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
        public async void Handle_ValidCommand_DeletesEducationSection()
        {
            // Arrange
            var cvSectionContentWriter = Substitute.For<ICvSectionContentWriter<EducationSection>>();
            var cvSectionReader = Substitute.For<ICvSectionReader<EducationSection>>();
            var cvReader = Substitute.For<ICvReader>();

            var handler = new DeleteEducationEstablishmentHandler(
                cvSectionContentWriter,
                cvSectionReader,
                cvReader);

            var metadata = new CommandMetadata("DeleteEducationEstablishment", DateTime.UtcNow, Guid.NewGuid().ToString());

            var command = new DeleteEducationEstablishment(
               Guid.NewGuid(),
               "test");

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

            await cvSectionContentWriter.Received().DeleteAsync(
                command.CvId,
                command.EducationEstablishment);
        }
    }
}
