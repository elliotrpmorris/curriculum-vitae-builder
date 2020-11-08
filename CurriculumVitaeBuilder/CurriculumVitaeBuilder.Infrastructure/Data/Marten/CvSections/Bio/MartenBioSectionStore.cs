// <copyright file="MartenBioSectionStore.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Infrastructure.Data.Marten.CvSections.Bio
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Chest.Core.Logging;

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
        /// <param name="documentStore">The Document Store Factory.</param>
        public MartenBioSectionStore(
            IDocumentStore documentStore)
        {
            this.DocumentStore = documentStore
                ?? throw new ArgumentNullException(nameof(documentStore));
        }

        private IDocumentStore DocumentStore { get; }

        /// <inheritdoc/>
        public async Task AddAsync(BioSection section)
        {
            using var session = this.DocumentStore.LightweightSession();

            var exists = await
                session.Query<BioSectionDocument>().AnyAsync(s => s.CvId == section.CvId);

            if (exists)
            {
                Logger.LogInformation($"{section.Title} Section Already exists for {section.CvId}");

                return;
            }

            session.Store(section.ToBioSectionDocument());

            await session.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task DeleteAsync(BioSection section)
        {
            using var session = this.DocumentStore.LightweightSession();

            session.Delete(section.ToBioSectionDocument());

            await session.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task<BioSection?> GetSectionByCvAsync(Guid cvId)
        {
            using var session = this.DocumentStore.LightweightSession();

            var section = await
                session
                    .Query<BioSectionDocument>()
                    .FirstOrDefaultAsync(s => s.CvId == cvId);

            if (section == null)
            {
                return null;
            }

            return section.ToBioSection();
        }

        /// <inheritdoc/>
        public async Task<IDictionary<Guid, BioSection>> GetSectionByCvAsync(
            IReadOnlyCollection<Guid> cvIds)
        {
            using var session = this.DocumentStore.LightweightSession();

            var sections = await
                session
                    .Query<BioSectionDocument>()
                    .Where(s => s.CvId.In(cvIds.ToList()))
                    .ToListAsync();

            if (sections == null)
            {
                return new Dictionary<Guid, BioSection>();
            }

            return sections.ToDictionary(x => x.CvId, x => x.ToBioSection());
        }

        /// <inheritdoc/>
        public async Task<bool> GetSectionExistsAsync(Guid cvId)
        {
            using var session = this.DocumentStore.LightweightSession();

            var exists = await
                session
                    .Query<BioSectionDocument>()
                    .AnyAsync(s => s.CvId == cvId);

            return exists;
        }

        /// <inheritdoc/>
        public async Task UpdateAsync(BioSection section)
        {
            using var session = this.DocumentStore.LightweightSession();

            var exists = await
                session
                    .Query<BioSectionDocument>()
                    .AnyAsync(s => s.CvId == section.CvId);

            if (!exists)
            {
                Logger.LogInformation($"{section.Title} Section Doesn't exist for {section.CvId}");

                return;
            }

            var x = section.ToBioSectionDocument();
            session.Update(x);

            await session.SaveChangesAsync();
        }
    }
}
