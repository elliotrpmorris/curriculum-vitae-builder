// <copyright file="CreateContactSectionValidatorTests.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Domain.Tests.Command.CvSections.Contact.Create
{
    using System;
    using System.Collections.Generic;

    using CurriculumVitaeBuilder.Domain.Command.CvSections.Contact.Create;

    using FluentValidation.TestHelper;

    using Xunit;

    public class CreateContactSectionValidatorTests
    {
        public CreateContactSectionValidatorTests()
        {
            this.Validator = new CreateContactSectionValidator();
            this.Validator.ConfigureRules();
        }

        private CreateContactSectionValidator Validator { get; }

        [Fact]
        public void Validate_InvalidUserId_Fails()
        {
            // Arrange
            var contactDetails = new Dictionary<string, string>();

            var command = new CreateContactSection(
                Guid.Empty,
                Guid.NewGuid(),
                contactDetails);

            // Act / Assert
            this.Validator.ShouldHaveValidationErrorFor(x => x.UserId, command);
        }

        [Fact]
        public void Validate_InvalidCvId_Fails()
        {
            // Arrange
            var contactDetails = new Dictionary<string, string>();

            var command = new CreateContactSection(
                Guid.NewGuid(),
                Guid.Empty,
                contactDetails);

            // Act / Assert
            this.Validator.ShouldHaveValidationErrorFor(x => x.CvId, command);
        }

        [Fact]
        public void Validate_InvalidContactDetails_Fails()
        {
            // Arrange
            Dictionary<string, string>? contactDetails = null;

            var command = new CreateContactSection(
                Guid.NewGuid(),
                Guid.Empty,
                contactDetails!);

            // Act / Assert
            this.Validator.ShouldHaveValidationErrorFor(x => x.ContactDetails, command);
        }

        [Fact]
        public void Validate_ValidParameters_Passes()
        {
            // Arrange
            var contactDetails = new Dictionary<string, string>()
            {
                { "test", "test" },
            };

            var command = new CreateContactSection(
                Guid.NewGuid(),
                Guid.NewGuid(),
                contactDetails);

            // Act / Assert
            this.Validator.ShouldNotHaveValidationErrorFor(x => x.CvId, command);
            this.Validator.ShouldNotHaveValidationErrorFor(x => x.UserId, command);
            this.Validator.ShouldNotHaveValidationErrorFor(x => x.ContactDetails, command);
        }
    }
}
