// <copyright file="UserDocument.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Infrastructure.Data.Marten.User
{
    using System;

    /// <summary>
    /// User Document.
    /// </summary>
    public class UserDocument
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserDocument"/> class.
        /// </summary>
        /// <param name="id">The user identifier.</param>
        /// <param name="userName">The user name.</param>
        /// <param name="createdAt">The created at time.</param>
        public UserDocument(
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
