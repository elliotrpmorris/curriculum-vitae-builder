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
        public User(
            Guid id,
            string fullName,
            DateTime dateOfBirth,
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
            this.CreatedAt = createdAt;
        }

        public Guid Id { get; }

        public DateTime CreatedAt { get; }
    }
}
