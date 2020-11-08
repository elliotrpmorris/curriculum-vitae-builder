// <copyright file="UpdateJobHistoryValidatorTests.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Domain.Tests.Command.CvSections.JobHistory.Update
{
    using System;
    using System.Collections.Generic;

    using CurriculumVitaeBuilder.Domain.Command.CvSections.JobHistory.Update;
    using CurriculumVitaeBuilder.Domain.Data.CvSections.JobHistory;

    using FluentValidation.TestHelper;

    using Xunit;

    public class UpdateJobHistoryValidatorTests
    {
        public UpdateJobHistoryValidatorTests()
        {
            this.Validator = new UpdateJobHistorySectionValidator();
            this.Validator.ConfigureRules();
        }

        private UpdateJobHistorySectionValidator Validator { get; }

        [Fact]
        public void Validate_InvalidUserId_Fails()
        {
            // Arrange
            var command = new UpdateJobHistorySection(
                Guid.Empty,
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

            // Act / Assert
            this.Validator.ShouldHaveValidationErrorFor(x => x.UserId, command);
        }

        [Fact]
        public void Validate_InvalidCvId_Fails()
        {
            // Arrange
            var command = new UpdateJobHistorySection(
                Guid.NewGuid(),
                Guid.Empty,
                new List<Job>
                {
                    new Job(
                        "test",
                        DateTime.Now,
                        DateTime.Now,
                        "test title",
                        "test desc"),
                });

            // Act / Assert
            this.Validator.ShouldHaveValidationErrorFor(x => x.CvId, command);
        }

        [Fact]
        public void Validate_InvalidJobHistoryDetails_Fails()
        {
            // Arrange
            List<Job>? jobs = null;

            var command = new UpdateJobHistorySection(
                Guid.NewGuid(),
                Guid.NewGuid(),
                jobs!);

            // Act / Assert
            this.Validator.ShouldHaveValidationErrorFor(x => x.Jobs, command);
        }

        [Fact]
        public void Validate_ValidParameters_Passes()
        {
            // Arrange
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

            // Act / Assert
            this.Validator.ShouldNotHaveValidationErrorFor(x => x.UserId, command);
            this.Validator.ShouldNotHaveValidationErrorFor(x => x.CvId, command);
            this.Validator.ShouldNotHaveValidationErrorFor(x => x.Jobs, command);
        }
    }
}
