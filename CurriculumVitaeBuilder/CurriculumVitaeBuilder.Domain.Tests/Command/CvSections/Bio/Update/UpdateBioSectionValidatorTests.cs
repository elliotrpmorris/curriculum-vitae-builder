// <copyright file="UpdateBioSectionValidatorTests.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Domain.Tests.Command.CvSections.Bio.Update
{
    using System;
    using CurriculumVitaeBuilder.Domain.Command.CvSections.Bio.Update;
    using FluentValidation.TestHelper;
    using Xunit;

    public class UpdateBioSectionValidatorTests
    {
        public UpdateBioSectionValidatorTests()
        {
            this.Validator = new UpdateBioSectionValidator();
            this.Validator.ConfigureRules();
        }

        private UpdateBioSectionValidator Validator { get; }

        [Fact]
        public void Validate_InvalidUserId_Fails()
        {
            // Arrange
            var command = new UpdateBioSection(
                Guid.Empty,
                Guid.NewGuid(),
                "test",
                "test city");

            // Act / Assert
            this.Validator.ShouldHaveValidationErrorFor(x => x.UserId, command);
        }

        [Fact]
        public void Validate_InvalidCvId_Fails()
        {
            // Arrange
            var command = new UpdateBioSection(
                Guid.NewGuid(),
                Guid.Empty,
                "test",
                "test city");

            // Act / Assert
            this.Validator.ShouldHaveValidationErrorFor(x => x.CvId, command);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Validate_InvalidFullName_Fails(string fullName)
        {
            // Arrange
            var command = new UpdateBioSection(
                Guid.NewGuid(),
                Guid.NewGuid(),
                fullName,
                "test city");

            // Act / Assert
            this.Validator.ShouldHaveValidationErrorFor(x => x.FullName, command);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Validate_InvalidCity_Fails(string city)
        {
            // Arrange
            var command = new UpdateBioSection(
                Guid.NewGuid(),
                Guid.NewGuid(),
                "test",
                city);

            // Act / Assert
            this.Validator.ShouldHaveValidationErrorFor(x => x.City, command);
        }

        [Fact]
        public void Validate_ValidParameters_Passes()
        {
            // Arrange
            var command = new UpdateBioSection(
                Guid.NewGuid(),
                Guid.NewGuid(),
                "test",
                "test city");

            // Act / Assert
            this.Validator.ShouldNotHaveValidationErrorFor(x => x.UserId, command);
            this.Validator.ShouldNotHaveValidationErrorFor(x => x.CvId, command);
            this.Validator.ShouldNotHaveValidationErrorFor(x => x.FullName, command);
            this.Validator.ShouldNotHaveValidationErrorFor(x => x.City, command);
        }
    }
}
