// <copyright file="SeedData.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Infrastructure.Data.Marten
{
    using System;

    using CurriculumVitaeBuilder.Infrastructure.Data.Marten.User;

    /// <summary>
    /// Seed Data for Marten.
    /// </summary>
    internal static class SeedData
    {
        /// <summary>
        /// Seed data for user documents.
        /// </summary>
        public static readonly UserDocument[]
            UserDocuments =
        {
            new UserDocument(
                Guid.Parse("3f169b60-3d10-4beb-b6f9-3bfe2e4e5526"),
                "Test User",
                DateTime.Now),
        };
    }
}
