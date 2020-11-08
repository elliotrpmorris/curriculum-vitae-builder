// <copyright file="DeleteEducationSectionValidatorTests.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Domain.Tests.Command.CvSections.Education.Delete
{
    using System;

    using CurriculumVitaeBuilder.Domain.Command.CvSections.Education.Delete;

    using FluentValidation.TestHelper;

    using Xunit;

    public class DeleteEducationSectionValidatorTests
    {
        public DeleteEducationSectionValidatorTests()
        {
            this.Validator = new DeleteEducationSectionValidator();
            this.Validator.ConfigureRules();
        }

        private DeleteEducationSectionValidator Validator { get; }

        [Fact]
        public void Validate_InvalidUserId_Fails()
        {
            // Arrange
            var command = new DeleteEducationSection(
                Guid.Empty,
                Guid.NewGuid());

            // Act / Assert
            this.Validator.ShouldHaveValidationErrorFor(x => x.UserId, command);
        }

        [Fact]
        public void Validate_InvalidCvId_Fails()
        {
            // Arrange
            var command = new DeleteEducationSection(
                Guid.NewGuid(),
                Guid.Empty);

            // Act / Assert
            this.Validator.ShouldHaveValidationErrorFor(x => x.CvId, command);
        }

        [Fact]
        public void Validate_ValidParameters_Passes()
        {
            // Arrange
            var command = new DeleteEducationSection(
                Guid.NewGuid(),
                Guid.NewGuid());

            // Act / Assert
            this.Validator.ShouldNotHaveValidationErrorFor(x => x.UserId, command);
            this.Validator.ShouldNotHaveValidationErrorFor(x => x.CvId, command);
        }
    }
}
