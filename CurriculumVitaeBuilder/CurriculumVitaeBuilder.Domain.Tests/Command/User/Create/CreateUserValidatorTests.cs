// <copyright file="CreateUserValidatorTests.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Domain.Tests.Command.User.Create
{
    using CurriculumVitaeBuilder.Domain.Command.User.Create;

    using FluentValidation.TestHelper;

    using Xunit;

    public class CreateUserValidatorTests
    {
        public CreateUserValidatorTests()
        {
            this.Validator = new CreateUserValidator();
            this.Validator.ConfigureRules();
        }

        private CreateUserValidator Validator { get; }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Validate_InvalidUserName_Fails(string userName)
        {
            // Arrange
            var command = new CreateUser(userName);

            // Act / Assert
            this.Validator.ShouldHaveValidationErrorFor(x => x.UserName, command);
        }

        [Fact]
        public void Validate_ValidParameters_Passes()
        {
            // Arrange
            var command = new CreateUser("testName");

            // Act / Assert
            this.Validator.ShouldNotHaveValidationErrorFor(x => x.UserName, command);
        }
    }
}
