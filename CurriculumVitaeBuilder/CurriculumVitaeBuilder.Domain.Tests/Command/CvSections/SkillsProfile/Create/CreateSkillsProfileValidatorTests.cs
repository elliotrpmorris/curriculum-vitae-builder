// <copyright file="CreateSkillsProfileValidatorTests.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Domain.Tests.Command.CvSections.SkillsProfile.Create
{
    using System;
    using System.Collections.Generic;

    using CurriculumVitaeBuilder.Domain.Command.CvSections.SkillsProfile.Create;
    using CurriculumVitaeBuilder.Domain.Data.CvSections.SkillsProfile;

    using FluentValidation.TestHelper;

    using Xunit;

    public class CreateSkillsProfileValidatorTests
    {
        public CreateSkillsProfileValidatorTests()
        {
            this.Validator = new CreateSkillsProfileSectionValidator();
            this.Validator.ConfigureRules();
        }

        private CreateSkillsProfileSectionValidator Validator { get; }

        [Fact]
        public void Validate_InvalidUserId_Fails()
        {
            // Arrange
            var skills = new List<Skill>();

            var command = new CreateSkillsProfileSection(
                Guid.Empty,
                Guid.NewGuid(),
                skills);

            // Act / Assert
            this.Validator.ShouldHaveValidationErrorFor(x => x.UserId, command);
        }

        [Fact]
        public void Validate_InvalidCvId_Fails()
        {
            // Arrange
            var skills = new List<Skill>();

            var command = new CreateSkillsProfileSection(
                Guid.NewGuid(),
                Guid.Empty,
                skills);

            // Act / Assert
            this.Validator.ShouldHaveValidationErrorFor(x => x.CvId, command);
        }

        [Fact]
        public void Validate_InvalidContactDetails_Fails()
        {
            // Arrange
            List<Skill>? skills = null;

            var command = new CreateSkillsProfileSection(
                Guid.NewGuid(),
                Guid.Empty,
                skills!);

            // Act / Assert
            this.Validator.ShouldHaveValidationErrorFor(x => x.Skills, command);
        }

        [Fact]
        public void Validate_ValidParameters_Passes()
        {
            // Arrange
            var skills = new List<Skill>()
            {
                new Skill("test", "test", null),
            };

            var command = new CreateSkillsProfileSection(
                Guid.NewGuid(),
                Guid.NewGuid(),
                skills);

            // Act / Assert
            this.Validator.ShouldNotHaveValidationErrorFor(x => x.CvId, command);
            this.Validator.ShouldNotHaveValidationErrorFor(x => x.UserId, command);
            this.Validator.ShouldNotHaveValidationErrorFor(x => x.Skills, command);
        }
    }
}
