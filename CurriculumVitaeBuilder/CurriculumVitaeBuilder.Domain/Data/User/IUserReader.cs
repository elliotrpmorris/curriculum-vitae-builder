// <copyright file="IUserReader.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Domain.Data.User
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// User reader.
    /// </summary>
    public interface IUserReader
    {
        /// <summary>
        /// Gets all users in system.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>The collection of users.</returns>
        public Task<IReadOnlyList<User>> GetUsersAsync();

        /// <summary>
        /// Gets a user by identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>The user.</returns>
        public Task<User?> GetUserByIdAsync(Guid userId);

        /// <summary>
        /// Gets a user by identifier.
        /// </summary>
        /// <param name="userName">The username.</param>
        /// <returns>Whether the user exists or not.</returns>
        public Task<bool> GetUserExistsAsync(string userName);
    }
}
