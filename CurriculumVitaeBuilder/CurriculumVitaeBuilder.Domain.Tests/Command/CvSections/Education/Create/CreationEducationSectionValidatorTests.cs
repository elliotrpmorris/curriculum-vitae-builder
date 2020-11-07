﻿// <copyright file="CreationEducationSectionValidatorTests.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Domain.Tests.Command.CvSections.Education.Create
{
    using System;
    using System.Collections.Generic;

    using CurriculumVitaeBuilder.Domain.Command.CvSections.Education.Create;
    using CurriculumVitaeBuilder.Domain.Data.CvSections.Education;

    using FluentValidation.TestHelper;

    using Xunit;

    public class CreationEducationSectionValidatorTests
    {
        public CreationEducationSectionValidatorTests()
        {
            this.Validator = new CreateEducationSectionValidator();
            this.Validator.ConfigureRules();
        }

        private CreateEducationSectionValidator Validator { get; }

        [Fact]
        public void Validate_InvalidUserId_Fails()
        {
            // Arrange
            var educationEstablishments = new List<EducationEstablishment>();

            var command = new CreateEducationSection(
                Guid.Empty,
                Guid.NewGuid(),
                educationEstablishments);

            // Act / Assert
            this.Validator.ShouldHaveValidationErrorFor(x => x.UserId, command);
        }

        [Fact]
        public void Validate_InvalidCvId_Fails()
        {
            // Arrange
            var educationEstablishments = new List<EducationEstablishment>();

            var command = new CreateEducationSection(
                Guid.NewGuid(),
                Guid.Empty,
                educationEstablishments);

            // Act / Assert
            this.Validator.ShouldHaveValidationErrorFor(x => x.CvId, command);
        }

        [Fact]
        public void Validate_InvalidContactDetails_Fails()
        {
            // Arrange
            List<EducationEstablishment>? educationEstablishments = null;

            var command = new CreateEducationSection(
                Guid.NewGuid(),
                Guid.Empty,
                educationEstablishments!);

            // Act / Assert
            this.Validator.ShouldHaveValidationErrorFor(x => x.EducationEstablishments, command);
        }

        [Fact]
        public void Validate_ValidParameters_Passes()
        {
            // Arrange
            var educationEstablishments = new List<EducationEstablishment>()
            {
                new EducationEstablishment("test", DateTime.Now, DateTime.Now),
            };

            var command = new CreateEducationSection(
                Guid.NewGuid(),
                Guid.NewGuid(),
                educationEstablishments);

            // Act / Assert
            this.Validator.ShouldNotHaveValidationErrorFor(x => x.CvId, command);
            this.Validator.ShouldNotHaveValidationErrorFor(x => x.UserId, command);
            this.Validator.ShouldNotHaveValidationErrorFor(x => x.EducationEstablishments, command);
        }
    }
}
