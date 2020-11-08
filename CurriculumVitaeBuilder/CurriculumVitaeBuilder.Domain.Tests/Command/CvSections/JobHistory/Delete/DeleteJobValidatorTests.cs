// <copyright file="DeleteJobValidatorTests.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Domain.Tests.Command.CvSections.JobHistory.Delete
{
    using System;

    using CurriculumVitaeBuilder.Domain.Command.CvSections.JobHistory.DeleteJob;

    using FluentValidation.TestHelper;

    using Xunit;

    public class DeleteJobValidatorTests
    {
        public DeleteJobValidatorTests()
        {
            this.Validator = new DeleteJobValidator();
            this.Validator.ConfigureRules();
        }

        private DeleteJobValidator Validator { get; }

        [Fact]
        public void Validate_InvalidCvId_Fails()
        {
            // Arrange
            var command = new DeleteJob(
                Guid.Empty,
                "test1",
                "test2");

            // Act / Assert
            this.Validator.ShouldHaveValidationErrorFor(x => x.CvId, command);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Validate_InvalidEmployer_Fails(string employer)
        {
            // Arrange
            var command = new DeleteJob(
                Guid.NewGuid(),
                employer,
                "test2");

            // Act / Assert
            this.Validator.ShouldHaveValidationErrorFor(x => x.Employer, command);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Validate_InvalidJobTitle_Fails(string jobTitle)
        {
            // Arrange
            var command = new DeleteJob(
                Guid.NewGuid(),
                "test1",
                jobTitle);

            // Act / Assert
            this.Validator.ShouldHaveValidationErrorFor(x => x.JobTitle, command);
        }

        [Fact]
        public void Validate_ValidParameters_Passes()
        {
            // Arrange
            var command = new DeleteJob(
                Guid.NewGuid(),
                "test1",
                "test2");

            // Act / Assert
            this.Validator.ShouldNotHaveValidationErrorFor(x => x.CvId, command);
            this.Validator.ShouldNotHaveValidationErrorFor(x => x.Employer, command);
            this.Validator.ShouldNotHaveValidationErrorFor(x => x.JobTitle, command);
        }
    }
}
