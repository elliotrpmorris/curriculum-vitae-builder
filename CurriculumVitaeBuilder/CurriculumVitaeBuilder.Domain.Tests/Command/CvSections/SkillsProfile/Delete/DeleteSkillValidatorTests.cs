// <copyright file="DeleteSkillValidatorTests.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Domain.Tests.Command.CvSections.SkillsProfile.Delete
{
    using System;

    using CurriculumVitaeBuilder.Domain.Command.CvSections.SkillsProfile.DeleteSkill;

    using FluentValidation.TestHelper;

    using Xunit;

    public class DeleteSkillValidatorTests
    {
        public DeleteSkillValidatorTests()
        {
            this.Validator = new DeleteSkillValidator();
            this.Validator.ConfigureRules();
        }

        private DeleteSkillValidator Validator { get; }

        [Fact]
        public void Validate_InvalidCvId_Fails()
        {
            // Arrange
            var command = new DeleteSkill(
                Guid.Empty,
                "test");

            // Act / Assert
            this.Validator.ShouldHaveValidationErrorFor(x => x.CvId, command);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Validate_InvalidEmployer_Fails(string skill)
        {
            // Arrange
            var command = new DeleteSkill(
                Guid.NewGuid(),
                skill);

            // Act / Assert
            this.Validator.ShouldHaveValidationErrorFor(x => x.Skill, command);
        }

        [Fact]
        public void Validate_ValidParameters_Passes()
        {
            // Arrange
            var command = new DeleteSkill(
                Guid.NewGuid(),
                "test");

            // Act / Assert
            this.Validator.ShouldNotHaveValidationErrorFor(x => x.CvId, command);
            this.Validator.ShouldNotHaveValidationErrorFor(x => x.Skill, command);
        }
    }
}
