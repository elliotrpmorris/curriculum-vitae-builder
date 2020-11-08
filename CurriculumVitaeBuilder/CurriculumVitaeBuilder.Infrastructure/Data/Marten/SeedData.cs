// <copyright file="SeedData.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Infrastructure.Data.Marten
{
    using System;
    using System.Collections.Generic;

    using CurriculumVitaeBuilder.Domain.Data.CvSections.Education;
    using CurriculumVitaeBuilder.Domain.Data.CvSections.JobHistory;
    using CurriculumVitaeBuilder.Domain.Data.CvSections.SkillsProfile;
    using CurriculumVitaeBuilder.Infrastructure.Data.Marten.CvSections.Bio;
    using CurriculumVitaeBuilder.Infrastructure.Data.Marten.CvSections.Contact;
    using CurriculumVitaeBuilder.Infrastructure.Data.Marten.CvSections.Education;
    using CurriculumVitaeBuilder.Infrastructure.Data.Marten.CvSections.JobHistory;
    using CurriculumVitaeBuilder.Infrastructure.Data.Marten.CvSections.SkillsProfile;
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

            new UserDocument(
                Guid.Parse("47b9b5d6-9ece-4284-a675-468979c4452f"),
                "David",
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

            new CvDocument(
                Guid.Parse("9b9cb1ad-a567-4889-95d7-0ecaa8bfef5d"),
                Guid.Parse("47b9b5d6-9ece-4284-a675-468979c4452f")),
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
                    { "email", "hello@hello.com" },
                    { "phone", "07949155434" },
                    { "website", "https://elliotmorris.dev" },
                }),
        };

        /// <summary>
        /// Seed data for education section documents.
        /// </summary>
        public static readonly EducationSectionDocument[]
            EducationSectionDocuments =
        {
            new EducationSectionDocument(
                Guid.Parse("dc1f0e7b-94c0-4572-ab7f-092cf36eb377"),
                Guid.Parse("fc2bd26b-4dc4-4ad5-85b7-cbe23c2ed0db"),
                new List<EducationEstablishment>()
                {
                    new EducationEstablishment(
                        "The Joe Bloggs school",
                        DateTime.Now.AddYears(-3).Date,
                        DateTime.Now.AddYears(-1).Date),
                }),
        };

        /// <summary>
        /// Seed data for job history section documents.
        /// </summary>
        public static readonly JobHistorySectionDocument[]
            JobHistorySectionDocuments =
        {
            new JobHistorySectionDocument(
                Guid.Parse("972f401b-ceb8-4ac0-baf0-0d2f11044093"),
                Guid.Parse("fc2bd26b-4dc4-4ad5-85b7-cbe23c2ed0db"),
                new List<Job>()
                {
                    new Job(
                        "BJSS",
                        DateTime.Now.AddHours(-3).Date,
                        DateTime.Now.Date,
                        "Software Engineer",
                        "Write good clean beautiful code"),
                }),
        };

        /// <summary>
        /// Seed data for skils profile section documents.
        /// </summary>
        public static readonly SkillsProfileSectionDocument[]
            SkillsProfileSectionDocuments =
        {
            new SkillsProfileSectionDocument(
                Guid.Parse("d15f3334-f587-447d-b59a-c548ba7df2d0"),
                Guid.Parse("fc2bd26b-4dc4-4ad5-85b7-cbe23c2ed0db"),
                new List<Skill>()
                {
                    new Skill(
                        "C#",
                        "4 years experience in using C#",
                        DateTime.Now.AddYears(-4).Date),

                    new Skill(
                        "JavaScript",
                        "4 years experience in using JavaScript",
                        DateTime.Now.AddYears(-4).Date),

                    new Skill(
                        "Go",
                        "Less than a years experience using GO",
                        null),
                }),
        };
    }
}
