// <copyright file="DeleteJobHandlerTests.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Domain.Tests.Command.CvSections.JobHistory.Delete
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Chest.Core.Command;
    using Chest.Core.Exceptions;

    using CurriculumVitaeBuilder.Domain.Command.CvSections.JobHistory.DeleteJob;
    using CurriculumVitaeBuilder.Domain.Data;
    using CurriculumVitaeBuilder.Domain.Data.CvSections;
    using CurriculumVitaeBuilder.Domain.Data.CvSections.JobHistory;

    using NSubstitute;
    using NSubstitute.ReturnsExtensions;

    using Xunit;

    public class DeleteJobHandlerTests
    {
        [Fact]
        public async void Handle_CvDoesntExist_Throws()
        {
            // Arrange
            var jobWriter = Substitute.For<IJobWriter>();
            var cvSectionReader = Substitute.For<ICvSectionReader<JobHistorySection>>();
            var cvReader = Substitute.For<ICvReader>();

            var handler = new DeleteJobHandler(
                jobWriter,
                cvSectionReader,
                cvReader);

            var metadata = new CommandMetadata("DeleteJob", DateTime.UtcNow, Guid.NewGuid().ToString());

            var command = new DeleteJob(
                Guid.NewGuid(),
                "test1",
                "test2");

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
            var jobWriter = Substitute.For<IJobWriter>();
            var cvSectionReader = Substitute.For<ICvSectionReader<JobHistorySection>>();
            var cvReader = Substitute.For<ICvReader>();

            var handler = new DeleteJobHandler(
                jobWriter,
                cvSectionReader,
                cvReader);

            var metadata = new CommandMetadata("DeleteJob", DateTime.UtcNow, Guid.NewGuid().ToString());

            var command = new DeleteJob(
                Guid.NewGuid(),
                "test1",
                "test2");

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
        public async void Handle_ValidCommand_DeletesJob()
        {
            // Arrange
            var jobWriter = Substitute.For<IJobWriter>();
            var cvSectionReader = Substitute.For<ICvSectionReader<JobHistorySection>>();
            var cvReader = Substitute.For<ICvReader>();

            var handler = new DeleteJobHandler(
                jobWriter,
                cvSectionReader,
                cvReader);

            var metadata = new CommandMetadata("DeleteJob", DateTime.UtcNow, Guid.NewGuid().ToString());

            var command = new DeleteJob(
                Guid.NewGuid(),
                "test1",
                "test2");

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
                            "test1",
                            DateTime.Now,
                            DateTime.Now,
                            "test2",
                            "test desc"),
                    }));

            // Act
            // Assert
            await handler.Handle(command, metadata);

            await jobWriter.Received().DeleteAsync(
                command.CvId,
                command.Employer,
                command.JobTitle);
        }
    }
}
