// <copyright file="DeleteJobHistorySectionHandlerTests.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Domain.Tests.Command.CvSections.JobHistory.Delete
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Chest.Core.Command;
    using Chest.Core.Exceptions;

    using CurriculumVitaeBuilder.Domain.Command.CvSections.JobHistory.Delete;
    using CurriculumVitaeBuilder.Domain.Data;
    using CurriculumVitaeBuilder.Domain.Data.CvSections;
    using CurriculumVitaeBuilder.Domain.Data.CvSections.JobHistory;

    using NSubstitute;
    using NSubstitute.ReturnsExtensions;

    using Xunit;

    public class DeleteJobHistorySectionHandlerTests
    {
        [Fact]
        public async void Handle_CvDoesntExist_Throws()
        {
            // Arrange
            var cvSectionReader = Substitute.For<ICvSectionReader<JobHistorySection>>();
            var cvSectionWriter = Substitute.For<ICvSectionWriter<JobHistorySection>>();
            var cvReader = Substitute.For<ICvReader>();

            var handler = new DeleteJobHistorySectionHandler(
                cvSectionReader,
                cvSectionWriter,
                cvReader);

            var metadata = new CommandMetadata("DeleteJobHistorySection", DateTime.UtcNow, Guid.NewGuid().ToString());

            var command = new DeleteJobHistorySection(
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
            var cvSectionReader = Substitute.For<ICvSectionReader<JobHistorySection>>();
            var cvSectionWriter = Substitute.For<ICvSectionWriter<JobHistorySection>>();
            var cvReader = Substitute.For<ICvReader>();

            var handler = new DeleteJobHistorySectionHandler(
                cvSectionReader,
                cvSectionWriter,
                cvReader);

            var metadata = new CommandMetadata("DeleteJobHistorySection", DateTime.UtcNow, Guid.NewGuid().ToString());

            var command = new DeleteJobHistorySection(
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
        public async void Handle_ValidCommand_DeletesJobHistorySection()
        {
            // Arrange
            var cvSectionReader = Substitute.For<ICvSectionReader<JobHistorySection>>();
            var cvSectionWriter = Substitute.For<ICvSectionWriter<JobHistorySection>>();
            var cvReader = Substitute.For<ICvReader>();

            var handler = new DeleteJobHistorySectionHandler(
                cvSectionReader,
                cvSectionWriter,
                cvReader);

            var metadata = new CommandMetadata("DeleteJobHistorySection", DateTime.UtcNow, Guid.NewGuid().ToString());

            var command = new DeleteJobHistorySection(
                Guid.NewGuid(),
                Guid.NewGuid());

            cvReader
                .GetCvExistsAsync(command.CvId)
                .Returns(true);

            cvSectionReader
                .GetSectionByCvAsync(command.CvId)
                .Returns(new JobHistorySection(
                    Guid.NewGuid(),
                    command.CvId,
                    new List<Job>
                    {
                        new Job(
                            "test",
                            DateTime.Now,
                            DateTime.Now,
                            "test title",
                            "test description"),
                    }));

            // Act
            // Assert
            await handler.Handle(command, metadata);

            var expectedArgs =
                Arg.Is<JobHistorySection>(u =>
                u.CvId == command.CvId);

            await cvSectionWriter.Received().DeleteAsync(expectedArgs);
        }
    }
}
