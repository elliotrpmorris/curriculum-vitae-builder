// <copyright file="DeleteEducationEstablishmentValidatorTests.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Domain.Tests.Command.CvSections.Education.Delete
{
    using System;

    using CurriculumVitaeBuilder.Domain.Command.CvSections.Education.DeleteEducationEstablishment;

    using FluentValidation.TestHelper;

    using Xunit;

    public class DeleteEducationEstablishmentValidatorTests
    {
        public DeleteEducationEstablishmentValidatorTests()
        {
            this.Validator = new DeleteEducationEstablishmentValidator();
            this.Validator.ConfigureRules();
        }

        private DeleteEducationEstablishmentValidator Validator { get; }

        [Fact]
        public void Validate_InvalidCvId_Fails()
        {
            // Arrange
            var command = new DeleteEducationEstablishment(
                Guid.Empty,
                "test");

            // Act / Assert
            this.Validator.ShouldHaveValidationErrorFor(x => x.CvId, command);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Validate_InvalidEducationEstablishment_Fails(string educationEstablishment)
        {
            // Arrange
            var command = new DeleteEducationEstablishment(
                Guid.Empty,
                educationEstablishment);

            // Act / Assert
            this.Validator.ShouldHaveValidationErrorFor(x => x.EducationEstablishment, command);
        }

        [Fact]
        public void Validate_ValidParameters_Passes()
        {
            // Arrange
            var command = new DeleteEducationEstablishment(
                Guid.NewGuid(),
                "test");

            // Act / Assert
            this.Validator.ShouldNotHaveValidationErrorFor(x => x.CvId, command);
            this.Validator.ShouldNotHaveValidationErrorFor(x => x.EducationEstablishment, command);
        }
    }
}
