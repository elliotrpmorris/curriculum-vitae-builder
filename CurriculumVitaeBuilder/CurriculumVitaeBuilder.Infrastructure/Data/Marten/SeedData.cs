// <copyright file="SeedData.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Infrastructure.Data.Marten
{
    using System;
    using System.Collections.Generic;

    using CurriculumVitaeBuilder.Infrastructure.Data.Marten.CvSections.Bio;
    using CurriculumVitaeBuilder.Infrastructure.Data.Marten.CvSections.Contact;
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
                "JohnDoe11",
                DateTime.Now),
        };

        /// <summary>
        /// Seed data for user documents.
        /// </summary>
        public static readonly CvDocument[]
            CvDocuments =
        {
            new CvDocument(
                Guid.Parse("fc2bd26b-4dc4-4ad5-85b7-cbe23c2ed0db"),
                Guid.Parse("3f169b60-3d10-4beb-b6f9-3bfe2e4e5526")),
        };

        /// <summary>
        /// Seed data for bio section documents.
        /// </summary>
        public static readonly BioSectionDocument[]
            BioSectionDocuments =
        {
            new BioSectionDocument(
                Guid.Parse("58e68b85-ec68-49d6-841a-e3fb62f42d65"),
                Guid.Parse("fc2bd26b-4dc4-4ad5-85b7-cbe23c2ed0db"),
                "John Doe",
                "Leeds"),
        };

        /// <summary>
        /// Seed data for contact section documents.
        /// </summary>
        public static readonly ContactSectionDocument[]
            ContactSectionDocuments =
        {
            new ContactSectionDocument(
                Guid.Parse("621d3c3c-6b98-4cee-8fe0-6627a8c8c300"),
                Guid.Parse("fc2bd26b-4dc4-4ad5-85b7-cbe23c2ed0db"),
                new Dictionary<string, string>()
                {
                    { "Email", "hello@hello.com" },
                    { "Phone", "07949155434" },
                    { "Website", "https://elliotmorris.dev" },
                }),
        };
    }
}
