// <copyright file="MartenDocumentStoreFactory.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Infrastructure.Data.Marten
{
    using System;

    using Chest.Core.Logging;

    using CurriculumVitaeBuilder.Infrastructure.Data.Marten.User;

    using global::Marten;

    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// Marten Document Store Factory.
    /// </summary>
    internal class MartenDocumentStoreFactory
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MartenDocumentStoreFactory"/> class.
        /// </summary>
        /// <param name="config">The configuration provider.</param>
        public MartenDocumentStoreFactory(IConfiguration config)
        {
            if (config == null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            this.ConnectionString = config.GetConnectionString("Postgres");
        }

        private string ConnectionString { get; }

        /// <summary>
        /// Gets The Document Store.
        /// </summary>
        /// <returns>The Document Store.</returns>
        public DocumentStore GetDocumentStore()
        {
            var store =
               DocumentStore
                   .For(_ =>
                   {
                       _.Connection(this.ConnectionString);

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

                       // Seed data.
                       _.InitialData.Add(new SeedDataSetup(SeedData.UserDocuments));
                   });

            return store;
        }
    }
}
