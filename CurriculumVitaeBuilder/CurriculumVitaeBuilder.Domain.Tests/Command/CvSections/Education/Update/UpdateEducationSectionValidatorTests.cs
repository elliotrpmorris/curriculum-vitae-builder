// <copyright file="UpdateEducationSectionValidatorTests.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Domain.Tests.Command.CvSections.Education.Update
{
    using System;
    using System.Collections.Generic;
    using CurriculumVitaeBuilder.Domain.Command.CvSections.Education.Update;
    using CurriculumVitaeBuilder.Domain.Data.CvSections.Education;
    using FluentValidation;
    using FluentValidation.TestHelper;
    using Xunit;

    public class UpdateEducationSectionValidatorTests
    {
        public UpdateEducationSectionValidatorTests()
        {
            this.Validator = new UpdateEducationSectionValidator();
            this.Validator.ConfigureRules();
        }

        private UpdateEducationSectionValidator Validator { get; }

        [Fact]
        public void Validate_InvalidUserId_Fails()
        {
            // Arrange
            var command = new UpdateEducationSection(
                Guid.Empty,
                Guid.NewGuid(),
                new List<EducationEstablishment>
                {
                    new EducationEstablishment(
                        "test",
                        DateTime.Now,
                        DateTime.Now),
                });

            // Act / Assert
            this.Validator.ShouldHaveValidationErrorFor(x => x.UserId, command);
        }

        [Fact]
        public void Validate_InvalidCvId_Fails()
        {
            // Arrange
            var command = new UpdateEducationSection(
                Guid.NewGuid(),
                Guid.Empty,
                new List<EducationEstablishment>
                {
                    new EducationEstablishment(
                        "test",
                        DateTime.Now,
                        DateTime.Now),
                });

            // Act / Assert
            this.Validator.ShouldHaveValidationErrorFor(x => x.CvId, command);
        }

        [Fact]
        public void Validate_InvalidEducationDetails_Fails()
        {
            // Arrange
            List<EducationEstablishment>? educationEstablishments = null;

            var command = new UpdateEducationSection(
                Guid.NewGuid(),
                Guid.NewGuid(),
                educationEstablishments!);

            // Act / Assert
            this.Validator.ShouldHaveValidationErrorFor(x => x.EducationEstablishments, command);
        }

        [Fact]
        public void Validate_ValidParameters_Passes()
        {
            // Arrange
            var command = new UpdateEducationSection(
                Guid.NewGuid(),
                Guid.NewGuid(),
                new List<EducationEstablishment>
                {
                    new EducationEstablishment(
                        "test",
                        DateTime.Now,
                        DateTime.Now),
                });

            // Act / Assert
            this.Validator.ShouldNotHaveValidationErrorFor(x => x.UserId, command);
            this.Validator.ShouldNotHaveValidationErrorFor(x => x.CvId, command);
            this.Validator.ShouldNotHaveValidationErrorFor(x => x.EducationEstablishments, command);
        }
    }
}
