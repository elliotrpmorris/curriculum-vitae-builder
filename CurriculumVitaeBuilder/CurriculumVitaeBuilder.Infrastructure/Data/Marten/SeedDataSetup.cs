// <copyright file="SeedDataSetup.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Infrastructure.Data.Marten
{
    using global::Marten;
    using global::Marten.Schema;

    /// <summary>
    /// Initial Seed Data For Marten.
    /// </summary>
    internal class SeedDataSetup : IInitialData
    {
        private readonly object[] data;

        /// <summary>
        /// Initializes a new instance of the <see cref="SeedDataSetup"/> class.
        /// </summary>
        /// <param name="data">The seed data.</param>
        public SeedDataSetup(params object[] data)
        {
            this.data = data;
        }

        /// <summary>
        /// Populates the DB with seed data.
        /// </summary>
        /// <param name="store">The document store.</param>
        public void Populate(IDocumentStore store)
        {
            using var session = store.LightweightSession();

            session.Store(this.data);
            session.SaveChanges();
        }
    }
}
