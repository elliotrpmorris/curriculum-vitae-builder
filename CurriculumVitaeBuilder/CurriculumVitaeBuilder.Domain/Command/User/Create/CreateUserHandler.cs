// <copyright file="CreateUserHandler.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Domain.Command.User.Create
{
    using System;
    using System.Threading.Tasks;

    using Chest.Core.Command;
    using Chest.Core.Exceptions;

    using CurriculumVitaeBuilder.Domain.Data;
    using CurriculumVitaeBuilder.Domain.Data.User;

    /// <summary>
    /// Create user handler.
    /// </summary>
    public class CreateUserHandler : ICommandHandler<CreateUser>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateUserHandler"/> class.
        /// </summary>
        /// <param name="userReader">The user reader.</param>
        /// <param name="userWriter">The user writer.</param>
        /// <param name="cvWriter">The cv writer.</param>
        public CreateUserHandler(
            IUserReader userReader,
            IUserWriter userWriter,
            ICvWriter cvWriter)
        {
            this.UserReader = userReader
                ?? throw new System.ArgumentNullException(nameof(userReader));

            this.UserWriter = userWriter
                ?? throw new System.ArgumentNullException(nameof(userWriter));

            this.CvWriter = cvWriter
                ?? throw new System.ArgumentNullException(nameof(cvWriter));
        }

        private IUserReader UserReader { get; }

        private IUserWriter UserWriter { get; }

        private ICvWriter CvWriter { get; }

        /// <inheritdoc/>
        public async Task Handle(CreateUser command, CommandMetadata metadata)
        {
            if (command.UserName == default)
            {
                throw new InvalidCommandException(
                    metadata.CommandName,
                    typeof(CreateUser).Name,
                    $"UserName must be set on the command.");
            }

            var exists = await
               this.UserReader.GetUserExistsAsync(command.UserName);

            if (exists)
            {
                throw new InvalidCommandException(
                  metadata.CommandName,
                  typeof(CommandMetadata).Name,
                  $"User already exists.");
            }

            var userId = Guid.NewGuid();
            var cvId = Guid.NewGuid();

            await this.UserWriter.AddAsync(
               new User(
                   userId,
                   command.UserName,
                   DateTime.Now));

            await this.CvWriter.AddAsync(
               new Cv(
                   cvId,
                   userId));
        }
    }
}
