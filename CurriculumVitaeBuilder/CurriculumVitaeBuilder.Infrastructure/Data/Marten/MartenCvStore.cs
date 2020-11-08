// <copyright file="MartenCvStore.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Infrastructure.Data.Marten
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Chest.Core.Logging;

    using CurriculumVitaeBuilder.Domain.Data;

    using global::Marten;

    /// <summary>
    /// Marten CV store.
    /// </summary>
    internal sealed class MartenCvStore :
        ICvReader,
        ICvWriter
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
        public async Task AddAsync(Cv cv)
        {
            using var session = this.DocumentStore.LightweightSession();

            var exists = await
                session.Query<CvDocument>().AnyAsync(s => s.UserId == cv.UserId);

            if (exists)
            {
                Logger.LogInformation($"CV Already exists for {cv.UserId}");

                return;
            }

            session.Store(cv.ToCvDocument());

            await session.SaveChangesAsync();
        }

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

        /// <inheritdoc/>
        public async Task<bool> GetCvExistsAsync(Guid cvId)
        {
            using var session = this.DocumentStore.LightweightSession();

            var exists = await
                session
                    .Query<CvDocument>()
                    .AnyAsync(s => s.Id == cvId);

            return exists;
        }

        /// <inheritdoc/>
        public async Task<IReadOnlyList<Cv>> GetCvsAsync()
        {
            using var session = this.DocumentStore.LightweightSession();

            var users = await
                session
                    .Query<CvDocument>()
                    .ToCv()
                    .ToListAsync();

            return users;
        }
    }
}
