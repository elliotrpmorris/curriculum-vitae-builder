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
        /// <param name="documentStore">The Document Store Factory.</param>
        public MartenCvStore(
            IDocumentStore documentStore)
        {
            this.DocumentStore = documentStore
                ?? throw new ArgumentNullException(nameof(documentStore));
        }

        private IDocumentStore DocumentStore { get; }

        /// <inheritdoc/>
        public async Task<Cv?> GetCvByUserAsync(Guid userId)
        {
            using var session = this.DocumentStore.LightweightSession();

            var cv = await
                session
                    .Query<CvDocument>()
                    .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cv == null)
            {
                return null;
            }

            return cv.ToCV();
        }
    }
}
