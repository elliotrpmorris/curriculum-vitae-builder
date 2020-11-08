// <copyright file="DeleteContactSectionValidatorTests.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Domain.Tests.Command.CvSections.Contact.Delete
{
    using System;
    using CurriculumVitaeBuilder.Domain.Command.CvSections.Contact.Delete;
    using FluentValidation.TestHelper;
    using Xunit;

    public class DeleteContactSectionValidatorTests
    {
        public DeleteContactSectionValidatorTests()
        {
            this.Validator = new DeleteContactSectionValidator();
            this.Validator.ConfigureRules();
        }

        private DeleteContactSectionValidator Validator { get; }

        [Fact]
        public void Validate_InvalidUserId_Fails()
        {
            // Arrange
            var command = new DeleteContactSection(
                Guid.Empty,
                Guid.NewGuid());

            // Act / Assert
            this.Validator.ShouldHaveValidationErrorFor(x => x.UserId, command);
        }

        [Fact]
        public void Validate_InvalidCvId_Fails()
        {
            // Arrange
            var command = new DeleteContactSection(
                Guid.NewGuid(),
                Guid.Empty);

            // Act / Assert
            this.Validator.ShouldHaveValidationErrorFor(x => x.CvId, command);
        }

        [Fact]
        public void Validate_ValidParameters_Passes()
        {
            // Arrange
            var command = new DeleteContactSection(
                Guid.NewGuid(),
                Guid.NewGuid());

            // Act / Assert
            this.Validator.ShouldNotHaveValidationErrorFor(x => x.UserId, command);
            this.Validator.ShouldNotHaveValidationErrorFor(x => x.CvId, command);
        }
    }
}