// <copyright file="DeleteJobHistorySectionValidatorTests.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Domain.Tests.Command.CvSections.JobHistory.Delete
{
    using System;

    using CurriculumVitaeBuilder.Domain.Command.CvSections.JobHistory.Delete;

    using FluentValidation.TestHelper;

    using Xunit;

    public class DeleteJobHistorySectionValidatorTests
    {
        public DeleteJobHistorySectionValidatorTests()
        {
            this.Validator = new DeleteJobHistorySectionValidator();
            this.Validator.ConfigureRules();
        }

        private DeleteJobHistorySectionValidator Validator { get; }

        [Fact]
        public void Validate_InvalidUserId_Fails()
        {
            // Arrange
            var command = new DeleteJobHistorySection(
                Guid.Empty,
                Guid.NewGuid());

            // Act / Assert
            this.Validator.ShouldHaveValidationErrorFor(x => x.UserId, command);
        }

        [Fact]
        public void Validate_InvalidCvId_Fails()
        {
            // Arrange
            var command = new DeleteJobHistorySection(
                Guid.NewGuid(),
                Guid.Empty);

            // Act / Assert
            this.Validator.ShouldHaveValidationErrorFor(x => x.CvId, command);
        }

        [Fact]
        public void Validate_ValidParameters_Passes()
        {
            // Arrange
            var command = new DeleteJobHistorySection(
                Guid.NewGuid(),
                Guid.NewGuid());

            // Act / Assert
            this.Validator.ShouldNotHaveValidationErrorFor(x => x.UserId, command);
            this.Validator.ShouldNotHaveValidationErrorFor(x => x.CvId, command);
        }
    }
}
