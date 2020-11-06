// <copyright file="User.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Domain.Data.User
{
    using System;

    /// <summary>
    /// User.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        /// <param name="id">The user identifier.</param>
        /// <param name="userName">The user name.</param>
        /// <param name="createdAt">The created at time.</param>
        public User(
            Guid id,
            string userName,
            DateTime createdAt)
        {
            if (id == default)
            {
                throw new ArgumentException(nameof(id));
            }

            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new ArgumentException(nameof(userName));
            }

            if (createdAt == default)
            {
                throw new ArgumentException(nameof(createdAt));
            }

            this.Id = id;
            this.UserName = userName;
            this.CreatedAt = createdAt;
        }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Gets the user name.
        /// </summary>
        public string UserName { get; }

        /// <summary>
        /// Gets the created at time.
        /// </summary>
        public DateTime CreatedAt { get; }
    }
}
