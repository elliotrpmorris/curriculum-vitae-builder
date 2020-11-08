// <copyright file="UpdateContactSectionValidatorTests.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Domain.Tests.Command.CvSections.Contact.Update
{
    using System;
    using System.Collections.Generic;

    using CurriculumVitaeBuilder.Domain.Command.CvSections.Contact.Update;

    using FluentValidation.TestHelper;

    using Xunit;

    public class UpdateContactSectionValidatorTests
    {
        public UpdateContactSectionValidatorTests()
        {
            this.Validator = new UpdateContactSectionValidator();
            this.Validator.ConfigureRules();
        }

        private UpdateContactSectionValidator Validator { get; }

        [Fact]
        public void Validate_InvalidUserId_Fails()
        {
            // Arrange
            var command = new UpdateContactSection(
                Guid.Empty,
                Guid.NewGuid(),
                new Dictionary<string, string>()
                {
                    { "test", "value" },
                });

            // Act / Assert
            this.Validator.ShouldHaveValidationErrorFor(x => x.UserId, command);
        }

        [Fact]
        public void Validate_InvalidCvId_Fails()
        {
            // Arrange
            var command = new UpdateContactSection(
                Guid.NewGuid(),
                Guid.Empty,
                new Dictionary<string, string>()
                {
                    { "test", "value" },
                });

            // Act / Assert
            this.Validator.ShouldHaveValidationErrorFor(x => x.CvId, command);
        }

        [Fact]
        public void Validate_InvalidContactDetails_Fails()
        {
            // Arrange
            Dictionary<string, string>? contactDetails = null;

            var command = new UpdateContactSection(
                Guid.NewGuid(),
                Guid.NewGuid(),
                contactDetails!);

            // Act / Assert
            this.Validator.ShouldHaveValidationErrorFor(x => x.ContactDetails, command);
        }

        [Fact]
        public void Validate_ValidParameters_Passes()
        {
            // Arrange
            var command = new UpdateContactSection(
                Guid.NewGuid(),
                Guid.NewGuid(),
                new Dictionary<string, string>()
                {
                    { "test", "value" },
                });

            // Act / Assert
            this.Validator.ShouldNotHaveValidationErrorFor(x => x.UserId, command);
            this.Validator.ShouldNotHaveValidationErrorFor(x => x.CvId, command);
            this.Validator.ShouldNotHaveValidationErrorFor(x => x.ContactDetails, command);
        }
    }
}
