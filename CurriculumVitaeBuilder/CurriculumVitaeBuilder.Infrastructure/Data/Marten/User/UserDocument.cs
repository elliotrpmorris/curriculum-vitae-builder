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
        public UserDocument(
            Guid id,
            string fullName,
            DateTime createdAt)
        {
            this.Id = id;
            this.FullName = fullName;
            this.CreatedAt = createdAt;
        }

        public Guid Id { get; }

        public string FullName { get; }

        public DateTime CreatedAt { get; }
    }
}
