// <copyright file="CreateUser.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Domain.Command.User.Create
{
    using Chest.Core.Command;

    /// <summary>
    /// Create User Command.
    /// </summary>
    [CommandName("USER/CREATE")]

    public class CreateUser : ICommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateUser"/> class.
        /// </summary>
        /// <param name="userName">The username.</param>
        public CreateUser(
            string userName)
        {
            this.UserName = userName;
        }

        /// <summary>
        /// Gets the username.
        /// </summary>
        public string UserName { get; }
    }
}
