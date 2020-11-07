// <copyright file="CreateJobHistorySectionValidatorTests.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Domain.Tests.Command.CvSections.JobHistory.Create
{
    using System;
    using System.Collections.Generic;

    using CurriculumVitaeBuilder.Domain.Command.CvSections.JobHistory.Create;
    using CurriculumVitaeBuilder.Domain.Data.CvSections.JobHistory;

    using FluentValidation.TestHelper;

    using Xunit;

    public class CreateJobHistorySectionValidatorTests
    {
        public CreateJobHistorySectionValidatorTests()
        {
            this.Validator = new CreateJobHistorySectionValidator();
            this.Validator.ConfigureRules();
        }

        private CreateJobHistorySectionValidator Validator { get; }

        [Fact]
        public void Validate_InvalidUserId_Fails()
        {
            // Arrange
            var jobs = new List<Job>();

            var command = new CreateJobHistorySection(
                Guid.Empty,
                Guid.NewGuid(),
                jobs);

            // Act / Assert
            this.Validator.ShouldHaveValidationErrorFor(x => x.UserId, command);
        }

        [Fact]
        public void Validate_InvalidCvId_Fails()
        {
            // Arrange
            var jobs = new List<Job>();

            var command = new CreateJobHistorySection(
                Guid.NewGuid(),
                Guid.Empty,
                jobs);

            // Act / Assert
            this.Validator.ShouldHaveValidationErrorFor(x => x.CvId, command);
        }

        [Fact]
        public void Validate_InvalidContactDetails_Fails()
        {
            // Arrange
            List<Job>? jobs = null;

            var command = new CreateJobHistorySection(
                Guid.NewGuid(),
                Guid.Empty,
                jobs!);

            // Act / Assert
            this.Validator.ShouldHaveValidationErrorFor(x => x.Jobs, command);
        }

        [Fact]
        public void Validate_ValidParameters_Passes()
        {
            // Arrange
            var jobs = new List<Job>()
            {
                new Job("test", DateTime.Now, DateTime.Now, "test", "test"),
            };

            var command = new CreateJobHistorySection(
                Guid.NewGuid(),
                Guid.NewGuid(),
                jobs);

            // Act / Assert
            this.Validator.ShouldNotHaveValidationErrorFor(x => x.CvId, command);
            this.Validator.ShouldNotHaveValidationErrorFor(x => x.UserId, command);
            this.Validator.ShouldNotHaveValidationErrorFor(x => x.Jobs, command);
        }
    }
}
