// <copyright file="MartenCvStore.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Infrastructure.Data.Marten
{
    using System;
    using System.Threading.Tasks;

    using CurriculumVitaeBuilder.Domain.Data;

    using global::Marten;

    /// <summary>
    /// Marten CV store.
    /// </summary>
    internal sealed class MartenCvStore : ICvReader
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MartenCvStore"/> class.
        /// </summary>
        /// <param name="documentStoreFactory">The Document Store Factory.</param>
        public MartenCvStore(
            MartenDocumentStoreFactory documentStoreFactory)
        {
            this.DocumentStoreFactory = documentStoreFactory
                ?? throw new ArgumentNullException(nameof(documentStoreFactory));

            this.DocumentStore = this.DocumentStoreFactory.GetDocumentStore();
        }

        private MartenDocumentStoreFactory DocumentStoreFactory { get; }

        private DocumentStore DocumentStore { get; }

        /// <inheritdoc/>
        public async Task<Cv?> GetCvByUserAsync(Guid userId)
        {
            using var session = this.DocumentStore.LightweightSession();

            var cv = await
                session
                    .Query<CvDocument>()
                    .FirstOrDefaultAsync(c => c.UserId == userId);

            return cv.ToBioSection() ?? null;
        }
    }
}
