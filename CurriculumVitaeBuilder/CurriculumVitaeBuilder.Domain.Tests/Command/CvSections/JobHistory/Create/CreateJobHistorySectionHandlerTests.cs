// <copyright file="CreateJobHistorySectionHandlerTests.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Domain.Tests.Command.JobHistory.Create
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Chest.Core.Command;
    using Chest.Core.Exceptions;

    using CurriculumVitaeBuilder.Domain.Command.CvSections.JobHistory.Create;
    using CurriculumVitaeBuilder.Domain.Data;
    using CurriculumVitaeBuilder.Domain.Data.CvSections;
    using CurriculumVitaeBuilder.Domain.Data.CvSections.JobHistory;

    using NSubstitute;

    using Xunit;

    public class CreateJobHistorySectionHandlerTests
    {
        [Fact]
        public async void Handle_CvDoesntExist_Throws()
        {
            // Arrange
            var cvSectionReader = Substitute.For<ICvSectionReader<JobHistorySection>>();
            var cvSectionWriter = Substitute.For<ICvSectionWriter<JobHistorySection>>();
            var cvReader = Substitute.For<ICvReader>();

            var handler = new CreateJobHistorySectionHandler(
                cvSectionReader,
                cvSectionWriter,
                cvReader);

            var metadata = new CommandMetadata("CreateJobHistorySection", DateTime.UtcNow, Guid.NewGuid().ToString());

            var jobs = new List<Job>();

            var command = new CreateJobHistorySection(
                Guid.NewGuid(),
                Guid.NewGuid(),
                jobs);

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

            var handler = new CreateJobHistorySectionHandler(
                cvSectionReader,
                cvSectionWriter,
                cvReader);

            var metadata = new CommandMetadata("CreateJobHistorySection", DateTime.UtcNow, Guid.NewGuid().ToString());

            var jobs = new List<Job>();

            var command = new CreateJobHistorySection(
                Guid.NewGuid(),
                Guid.NewGuid(),
                jobs);

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
        public async void Handle_ValidCommand_CreatesJobHistorySection()
        {
            // Arrange
            var cvSectionReader = Substitute.For<ICvSectionReader<JobHistorySection>>();
            var cvSectionWriter = Substitute.For<ICvSectionWriter<JobHistorySection>>();
            var cvReader = Substitute.For<ICvReader>();

            var handler = new CreateJobHistorySectionHandler(
                cvSectionReader,
                cvSectionWriter,
                cvReader);

            var metadata = new CommandMetadata("CreateJobHistorySection", DateTime.UtcNow, Guid.NewGuid().ToString());

            var jobs = new List<Job>();

            var command = new CreateJobHistorySection(
                Guid.NewGuid(),
                Guid.NewGuid(),
                jobs);

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
                Arg.Is<JobHistorySection>(u =>
                u.CvId == command.CvId &&
                u.Jobs == command.Jobs);

            await cvSectionWriter.Received().AddAsync(expectedArgs);
        }
    }
}