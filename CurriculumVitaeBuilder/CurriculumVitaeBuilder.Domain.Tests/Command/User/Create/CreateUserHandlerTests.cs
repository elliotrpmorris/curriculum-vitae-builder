// <copyright file="CreateUserHandlerTests.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Domain.Tests.Command.User.Create
{
    using System;
    using System.Linq;

    using Chest.Core.Command;
    using Chest.Core.Exceptions;

    using CurriculumVitaeBuilder.Domain.Command.User.Create;
    using CurriculumVitaeBuilder.Domain.Data;
    using CurriculumVitaeBuilder.Domain.Data.User;

    using NSubstitute;

    using Xunit;

    public class CreateUserHandlerTests
    {
        [Fact]
        public async void Handle_UserNameExists_Throws()
        {
            // Arrange
            var userReader = Substitute.For<IUserReader>();
            var userWriter = Substitute.For<IUserWriter>();
            var cvWriter = Substitute.For<ICvWriter>();

            var handler = new CreateUserHandler(
                userReader,
                userWriter,
                cvWriter);

            var metadata = new CommandMetadata("CreateUser", DateTime.UtcNow, Guid.NewGuid().ToString());

            var command = new CreateUser(
                "testUser");

            userReader
                .GetUserExistsAsync(command.UserName)
                .Returns(true);

            // Act
            // Assert
            var ex = await Assert.ThrowsAsync<InvalidCommandException>(
                async () => await handler.Handle(command, metadata));

            Assert.Contains(
                ex.ValidationErrors.SelectMany(v => v.Value),
                v => v == $"User already exists.");
        }

        [Fact]
        public async void Handle_ValidCommand_CreatesUser()
        {
            // Arrange
            var userReader = Substitute.For<IUserReader>();
            var userWriter = Substitute.For<IUserWriter>();
            var cvWriter = Substitute.For<ICvWriter>();

            var handler = new CreateUserHandler(
                userReader,
                userWriter,
                cvWriter);

            var metadata = new CommandMetadata("CreateUser", DateTime.UtcNow, Guid.NewGuid().ToString());

            var command = new CreateUser(
                "testUser");

            userReader
                .GetUserExistsAsync(command.UserName)
                .Returns(false);

            // Act
            // Assert
            await handler.Handle(command, metadata);

            var expectedArgs =
                Arg.Is<User>(u =>
                u.UserName == command.UserName);

            await userWriter.Received().AddAsync(expectedArgs);
        }
    }
}
