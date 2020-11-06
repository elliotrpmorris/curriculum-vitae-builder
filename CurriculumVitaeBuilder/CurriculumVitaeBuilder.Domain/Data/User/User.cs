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
        /// <param name="fullName">The full name.</param>
        /// <param name="createdAt">The created at time.</param>
        public User(
            Guid id,
            string fullName,
            DateTime createdAt)
        {
            if (id == default)
            {
                throw new ArgumentException(nameof(id));
            }

            if (createdAt == default)
            {
                throw new ArgumentException(nameof(createdAt));
            }

            this.Id = id;
            this.FullName = fullName;
            this.CreatedAt = createdAt;
        }

        public Guid Id { get; }

        public string FullName { get; }

        public DateTime CreatedAt { get; }
    }
}
