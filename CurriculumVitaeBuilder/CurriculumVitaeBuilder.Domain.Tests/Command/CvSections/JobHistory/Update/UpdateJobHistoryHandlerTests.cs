// <copyright file="UpdateJobHistoryHandlerTests.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Domain.Tests.Command.CvSections.JobHistory.Update
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Chest.Core.Command;
    using Chest.Core.Exceptions;

    using CurriculumVitaeBuilder.Domain.Command.CvSections.JobHistory.Update;
    using CurriculumVitaeBuilder.Domain.Data;
    using CurriculumVitaeBuilder.Domain.Data.CvSections;
    using CurriculumVitaeBuilder.Domain.Data.CvSections.JobHistory;

    using NSubstitute;
    using NSubstitute.ReturnsExtensions;

    using Xunit;

    public class UpdateJobHistoryHandlerTests
    {
        [Fact]
        public async void Handle_CvDoesntExist_Throws()
        {
            // Arrange
            var cvSectionReader = Substitute.For<ICvSectionReader<JobHistorySection>>();
            var cvSectionWriter = Substitute.For<ICvSectionWriter<JobHistorySection>>();
            var cvReader = Substitute.For<ICvReader>();

            var handler = new UpdateJobHistorySectionHandler(
                cvSectionReader,
                cvSectionWriter,
                cvReader);

            var metadata = new CommandMetadata("UpdateJobHistorySection", DateTime.UtcNow, Guid.NewGuid().ToString());

            var command = new UpdateJobHistorySection(
                Guid.NewGuid(),
                Guid.NewGuid(),
                new List<Job>
                {
                    new Job(
                        "test",
                        DateTime.Now,
                        DateTime.Now,
                        "test title",
                        "test desc"),
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
            var cvSectionReader = Substitute.For<ICvSectionReader<JobHistorySection>>();
            var cvSectionWriter = Substitute.For<ICvSectionWriter<JobHistorySection>>();
            var cvReader = Substitute.For<ICvReader>();

            var handler = new UpdateJobHistorySectionHandler(
                cvSectionReader,
                cvSectionWriter,
                cvReader);

            var metadata = new CommandMetadata("UpdateJobHistorySection", DateTime.UtcNow, Guid.NewGuid().ToString());

            var command = new UpdateJobHistorySection(
                Guid.NewGuid(),
                Guid.NewGuid(),
                new List<Job>
                {
                    new Job(
                        "test",
                        DateTime.Now,
                        DateTime.Now,
                        "test title",
                        "test desc"),
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
        public async void Handle_ValidCommand_UpdatesJobHistorySection()
        {
            // Arrange
            var cvSectionReader = Substitute.For<ICvSectionReader<JobHistorySection>>();
            var cvSectionWriter = Substitute.For<ICvSectionWriter<JobHistorySection>>();
            var cvReader = Substitute.For<ICvReader>();

            var handler = new UpdateJobHistorySectionHandler(
                cvSectionReader,
                cvSectionWriter,
                cvReader);

            var metadata = new CommandMetadata("UpdateJobHistorySection", DateTime.UtcNow, Guid.NewGuid().ToString());

            var command = new UpdateJobHistorySection(
                Guid.NewGuid(),
                Guid.NewGuid(),
                new List<Job>
                {
                    new Job(
                        "test",
                        DateTime.Now,
                        DateTime.Now,
                        "test title",
                        "test desc"),
                });

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
                            "test desc"),
                    }));

            // Act
            // Assert
            await handler.Handle(command, metadata);

            var expectedArgs =
                Arg.Is<JobHistorySection>(u =>
                u.CvId == command.CvId &&
                u.Jobs == command.Jobs);

            await cvSectionWriter.Received().UpdateAsync(expectedArgs);
        }
    }
}
