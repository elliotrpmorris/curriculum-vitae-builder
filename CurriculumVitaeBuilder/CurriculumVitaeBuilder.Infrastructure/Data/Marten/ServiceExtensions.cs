// <copyright file="ServiceExtensions.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Infrastructure.Data.Marten
{
    using Chest.Core.Logging;

    using CurriculumVitaeBuilder.Domain.Data;
    using CurriculumVitaeBuilder.Domain.Data.CvSections;
    using CurriculumVitaeBuilder.Domain.Data.CvSections.Bio;
    using CurriculumVitaeBuilder.Domain.Data.CvSections.Contact;
    using CurriculumVitaeBuilder.Domain.Data.CvSections.Education;
    using CurriculumVitaeBuilder.Domain.Data.CvSections.JobHistory;
    using CurriculumVitaeBuilder.Domain.Data.CvSections.SkillsProfile;
    using CurriculumVitaeBuilder.Domain.Data.User;
    using CurriculumVitaeBuilder.Infrastructure.Data.Marten.CvSections;
    using CurriculumVitaeBuilder.Infrastructure.Data.Marten.CvSections.Bio;
    using CurriculumVitaeBuilder.Infrastructure.Data.Marten.CvSections.Contact;
    using CurriculumVitaeBuilder.Infrastructure.Data.Marten.CvSections.Education;
    using CurriculumVitaeBuilder.Infrastructure.Data.Marten.CvSections.JobHistory;
    using CurriculumVitaeBuilder.Infrastructure.Data.Marten.CvSections.SkillsProfile;
    using CurriculumVitaeBuilder.Infrastructure.Data.Marten.User;

    using global::Marten;

    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Service Extensions.
    /// </summary>
    public static class ServiceExtensions
    {
        /// <summary>
        /// Registers Marten Data Access.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="connectionString">The connection string.</param>
        /// <returns>The service collections.</returns>
        public static IServiceCollection AddMartenDataAccess(
            this IServiceCollection services,
            string connectionString)
        {
            services.AddSingleton<IDocumentStore>(
                DocumentStore
                   .For(_ =>
                   {
                       _.Connection(connectionString);

                       _.CreateDatabasesForTenants(c =>
                       {
                           // This will create the DB if not there.
                           c.ForTenant()
                               .CheckAgainstPgDatabase()
                               .WithOwner("postgres")
                               .WithEncoding("UTF-8")
                               .ConnectionLimit(-1)
                               .OnDatabaseCreated(_ =>
                                {
                                    Logger.LogInformation("Database created");
                                });
                       });

                       _.DdlRules.TableCreation = CreationStyle.CreateIfNotExists;

                       // Schema setup.
                       _.Schema.For<UserDocument>()
                            .Identity(i => i.Id)
                            .DocumentAlias("users");

                       _.Schema.For<CvDocument>()
                           .Identity(i => i.Id)
                           .DocumentAlias("cv");

                       _.Schema.For<CvSectionDocument>()
                           .Identity(i => i.Id)
                           .DocumentAlias("cv_sections")

                           // Section Types.
                           .AddSubClass(typeof(BioSectionDocument))

                           .AddSubClass(typeof(ContactSectionDocument))
                           .AddSubClass(typeof(EducationSectionDocument))
                           .AddSubClass(typeof(JobHistorySectionDocument))
                           .AddSubClass(typeof(SkillsProfileSectionDocument));

                       // Seed data.
                       // Please note these are used for demo purpose only.
                       _.InitialData.Add(new SeedDataSetup(SeedData.UserDocuments));
                       _.InitialData.Add(new SeedDataSetup(SeedData.CvDocuments));
                       _.InitialData.Add(new SeedDataSetup(SeedData.BioSectionDocuments));
                       _.InitialData.Add(new SeedDataSetup(SeedData.EducationSectionDocuments));
                       _.InitialData.Add(new SeedDataSetup(SeedData.SkillsProfileSectionDocuments));
                   }));

            services
                .AddTransient<IUserReader, MartenUserStore>()
                .AddTransient<IUserWriter, MartenUserStore>();

            services
                .AddTransient<ICvReader, MartenCvStore>()
                .AddTransient<ICvWriter, MartenCvStore>();

            services
                .AddTransient<ICvSectionReader<BioSection>, MartenBioSectionStore>()
                .AddTransient<ICvSectionWriter<BioSection>, MartenBioSectionStore>();

            services
                .AddTransient<ICvSectionReader<ContactSection>, MartenContactSectionStore>()
                .AddTransient<ICvSectionWriter<ContactSection>, MartenContactSectionStore>()
                .AddTransient<ICvSectionContentWriter<ContactSection>, MartenContactSectionStore>();

            services
                .AddTransient<ICvSectionReader<EducationSection>, MartenEducationSectionStore>()
                .AddTransient<ICvSectionWriter<EducationSection>, MartenEducationSectionStore>()
                .AddTransient<ICvSectionContentWriter<EducationSection>, MartenEducationSectionStore>();

            services
                .AddTransient<ICvSectionReader<JobHistorySection>, MartenJobHistorySectionStore>()
                .AddTransient<ICvSectionWriter<JobHistorySection>, MartenJobHistorySectionStore>()
                .AddTransient<IJobWriter, MartenJobHistorySectionStore>();

            services
                .AddTransient<ICvSectionReader<SkillsProfileSection>, MartenSkillsProfileSectionStore>()
                .AddTransient<ICvSectionWriter<SkillsProfileSection>, MartenSkillsProfileSectionStore>()
                .AddTransient<ICvSectionContentWriter<SkillsProfileSection>, MartenSkillsProfileSectionStore>();

            return services;
        }
    }
}
