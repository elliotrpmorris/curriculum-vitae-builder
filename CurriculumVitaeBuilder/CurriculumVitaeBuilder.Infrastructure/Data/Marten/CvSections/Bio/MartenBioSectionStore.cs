// <copyright file="MartenBioSectionStore.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Infrastructure.Data.Marten.CvSections.Bio
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CurriculumVitaeBuilder.Domain.Data.CvSections;
    using CurriculumVitaeBuilder.Domain.Data.CvSections.Bio;

    using global::Marten;

    /// <summary>
    /// Marten Bio Section store.
    /// </summary>
    internal sealed class MartenBioSectionStore
        : ICvSectionReader<BioSection>, ICvSectionWriter<BioSection>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MartenBioSectionStore"/> class.
        /// </summary>
        /// <param name="documentStoreFactory">The Document Store Factory.</param>
        public MartenBioSectionStore(
            MartenDocumentStoreFactory documentStoreFactory)
        {
            this.DocumentStoreFactory = documentStoreFactory
                ?? throw new ArgumentNullException(nameof(documentStoreFactory));

            this.DocumentStore = this.DocumentStoreFactory.GetDocumentStore();
        }

        private MartenDocumentStoreFactory DocumentStoreFactory { get; }

        private DocumentStore DocumentStore { get; }

        /// <inheritdoc/>
        public Task AddAsync(BioSection section)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task DeleteAsync(BioSection section)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public async Task<BioSection?> GetSectionByCvAsync(Guid cvId)
        {
            using var session = this.DocumentStore.LightweightSession();

            var section = await
                session
                    .Query<BioSectionDocument>()
                    .FirstOrDefaultAsync(s => s.CvId == cvId);

            return section.ToBioSection() ?? null;
        }

        /// <inheritdoc/>
        public async Task<IDictionary<Guid, BioSection>> GetSectionByCvAsync(
            IReadOnlyCollection<Guid> cvIds)
        {
            using var session = this.DocumentStore.LightweightSession();

            var bioSections = await
                session
                    .Query<BioSectionDocument>()
                    .ToListAsync();

            if (bioSections == null)
            {
                return new Dictionary<Guid, BioSection>();
            }

            return bioSections.ToDictionary(x => x.CvId, x => x.ToBioSection());
        }

        /// <inheritdoc/>
        public Task UpdateAsync(BioSection section)
        {
            throw new NotImplementedException();
        }
    }
}
