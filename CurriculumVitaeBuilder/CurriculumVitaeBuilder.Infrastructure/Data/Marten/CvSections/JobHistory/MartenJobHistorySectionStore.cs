// <copyright file="MartenJobHistorySectionStore.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Infrastructure.Data.Marten.CvSections.JobHistory
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

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
        /// <param name="documentStoreFactory">The Document Store Factory.</param>
        public MartenJobHistorySectionStore(
            MartenDocumentStoreFactory documentStoreFactory)
        {
            this.DocumentStoreFactory = documentStoreFactory
                ?? throw new ArgumentNullException(nameof(documentStoreFactory));

            this.DocumentStore = this.DocumentStoreFactory.GetDocumentStore();
        }

        private MartenDocumentStoreFactory DocumentStoreFactory { get; }

        private DocumentStore DocumentStore { get; }

        /// <inheritdoc/>
        public Task AddAsync(JobHistorySection section)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task DeleteAsync(JobHistorySection section)
        {
            throw new NotImplementedException();
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
        public Task UpdateAsync(JobHistorySection section)
        {
            throw new NotImplementedException();
        }
    }
}
