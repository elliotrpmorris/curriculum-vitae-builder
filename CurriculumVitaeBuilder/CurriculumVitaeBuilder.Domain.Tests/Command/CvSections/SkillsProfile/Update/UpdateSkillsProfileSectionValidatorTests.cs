// <copyright file="UpdateSkillsProfileSectionValidatorTests.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Domain.Tests.Command.CvSections.SkillsProfile.Update
{
    using System;
    using System.Collections.Generic;

    using CurriculumVitaeBuilder.Domain.Command.CvSections.SkillsProfile.Update;
    using CurriculumVitaeBuilder.Domain.Data.CvSections.SkillsProfile;

    using FluentValidation.TestHelper;

    using Xunit;

    public class UpdateSkillsProfileSectionValidatorTests
    {
        public UpdateSkillsProfileSectionValidatorTests()
        {
            this.Validator = new UpdateSkillsProfileSectionValidator();
            this.Validator.ConfigureRules();
        }

        private UpdateSkillsProfileSectionValidator Validator { get; }

        [Fact]
        public void Validate_InvalidUserId_Fails()
        {
            // Arrange
            var command = new UpdateSkillsProfileSection(
                Guid.Empty,
                Guid.NewGuid(),
                new List<Skill>
                {
                    new Skill(
                        "test1",
                        "test2",
                        null),
                });

            // Act / Assert
            this.Validator.ShouldHaveValidationErrorFor(x => x.UserId, command);
        }

        [Fact]
        public void Validate_InvalidCvId_Fails()
        {
            // Arrange
            var command = new UpdateSkillsProfileSection(
                Guid.NewGuid(),
                Guid.Empty,
                new List<Skill>
                {
                    new Skill(
                        "test1",
                        "test2",
                        null),
                });

            // Act / Assert
            this.Validator.ShouldHaveValidationErrorFor(x => x.CvId, command);
        }

        [Fact]
        public void Validate_InvalidSkillsProfileDetails_Fails()
        {
            // Arrange
            List<Skill>? skills = null;

            var command = new UpdateSkillsProfileSection(
                Guid.NewGuid(),
                Guid.NewGuid(),
                skills!);

            // Act / Assert
            this.Validator.ShouldHaveValidationErrorFor(x => x.Skills, command);
        }

        [Fact]
        public void Validate_ValidParameters_Passes()
        {
            // Arrange
            var command = new UpdateSkillsProfileSection(
                Guid.NewGuid(),
                Guid.NewGuid(),
                new List<Skill>
                {
                    new Skill(
                        "test1",
                        "test2",
                        null),
                });

            // Act / Assert
            this.Validator.ShouldNotHaveValidationErrorFor(x => x.UserId, command);
            this.Validator.ShouldNotHaveValidationErrorFor(x => x.CvId, command);
            this.Validator.ShouldNotHaveValidationErrorFor(x => x.Skills, command);
        }
    }
}
