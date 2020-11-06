// <copyright file="MartenJobHistorySectionStore.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Infrastructure.Data.Marten.CvSections.JobHistory
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Chest.Core.Logging;

    using CurriculumVitaeBuilder.Domain.Data.CvSections;

    using global::Marten;

    /// <summary>
    /// Marten Contact Section store.
    /// </summary>
    internal sealed class MartenJobHistorySectionStore
        : ICvSectionReader<JobHistorySection>, ICvSectionWriter<JobHistorySection>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MartenJobHistorySectionStore"/> class.
        /// </summary>
        /// <param name="documentStore">The Document Store Factory.</param>
        public MartenJobHistorySectionStore(
            IDocumentStore documentStore)
        {
            this.DocumentStore = documentStore
                ?? throw new ArgumentNullException(nameof(documentStore));
        }

        private IDocumentStore DocumentStore { get; }

        /// <inheritdoc/>
        public async Task AddAsync(JobHistorySection section)
        {
            using var session = this.DocumentStore.LightweightSession();

            var exists = await
                session.Query<JobHistorySectionDocument>().AnyAsync(s => s.CvId == section.CvId);

            if (exists)
            {
                Logger.LogInformation($"{section.Title} Section Already exists for {section.CvId}");

                return;
            }

            session.Store(section.ToJobHistorySectionDocument());

            await session.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task DeleteAsync(JobHistorySection section)
        {
            using var session = this.DocumentStore.LightweightSession();

            session.Delete(section.ToJobHistorySectionDocument());

            await session.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task<JobHistorySection?> GetSectionByCvAsync(Guid cvId)
        {
            using var session = this.DocumentStore.LightweightSession();

            var section = await
                session
                    .Query<JobHistorySectionDocument>()
                    .FirstOrDefaultAsync(s => s.CvId == cvId);

            return section.ToJobHistorySection() ?? null;
        }

        /// <inheritdoc/>
        public async Task<IDictionary<Guid, JobHistorySection>> GetSectionByCvAsync(
            IReadOnlyCollection<Guid> cvIds)
        {
            using var session = this.DocumentStore.LightweightSession();

            var sections = await
                session
                    .Query<JobHistorySectionDocument>()
                    .Where(s => s.CvId.In(cvIds.ToList()))
                    .ToListAsync();

            if (sections == null)
            {
                return new Dictionary<Guid, JobHistorySection>();
            }

            return sections.ToDictionary(x => x.CvId, x => x.ToJobHistorySection());
        }

        /// <inheritdoc/>
        public async Task UpdateAsync(JobHistorySection section)
        {
            using var session = this.DocumentStore.LightweightSession();

            var exists = await
                session.Query<JobHistorySectionDocument>().AnyAsync(s => s.CvId == section.CvId);

            if (exists)
            {
                Logger.LogInformation($"{section.Title} Section Already exists for {section.CvId}");

                return;
            }

            session.Update(section.ToJobHistorySectionDocument());

            await session.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task<bool> GetSectionExistsAsync(Guid cvId)
        {
            using var session = this.DocumentStore.LightweightSession();

            var exists = await
                session
                    .Query<JobHistorySection>()
                    .AnyAsync(s => s.CvId == cvId);

            return exists;
        }
    }
}
