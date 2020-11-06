// <copyright file="MartenContactSectionStore.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Infrastructure.Data.Marten.CvSections.Contact
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Chest.Core.Logging;

    using CurriculumVitaeBuilder.Domain.Data.CvSections;
    using CurriculumVitaeBuilder.Domain.Data.CvSections.Contact;

    using global::Marten;

    /// <summary>
    /// Marten Contact Section store.
    /// </summary>
    internal sealed class MartenContactSectionStore
        : ICvSectionReader<ContactSection>, ICvSectionWriter<ContactSection>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MartenContactSectionStore"/> class.
        /// </summary>
        /// <param name="documentStore">The Document Store Factory.</param>
        public MartenContactSectionStore(
            IDocumentStore documentStore)
        {
            this.DocumentStore = documentStore
                ?? throw new ArgumentNullException(nameof(documentStore));
        }

        private IDocumentStore DocumentStore { get; }

        /// <inheritdoc/>
        public async Task AddAsync(ContactSection section)
        {
            using var session = this.DocumentStore.LightweightSession();

            var exists = await
                session.Query<ContactSectionDocument>().AnyAsync(s => s.CvId == section.CvId);

            if (exists)
            {
                Logger.LogInformation($"{section.Title} Section Already exists for {section.CvId}");

                return;
            }

            session.Store(section.ToContactSectionDocument());

            await session.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task DeleteAsync(ContactSection section)
        {
            using var session = this.DocumentStore.LightweightSession();

            session.Delete(section.ToContactSectionDocument());

            await session.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task<ContactSection?> GetSectionByCvAsync(Guid cvId)
        {
            using var session = this.DocumentStore.LightweightSession();

            var section = await
                session
                    .Query<ContactSectionDocument>()
                    .FirstOrDefaultAsync(s => s.CvId == cvId);

            return section.ToContactSection() ?? null;
        }

        /// <inheritdoc/>
        public async Task<IDictionary<Guid, ContactSection>> GetSectionByCvAsync(
            IReadOnlyCollection<Guid> cvIds)
        {
            using var session = this.DocumentStore.LightweightSession();

            var sections = await
                session
                    .Query<ContactSectionDocument>()
                    .Where(s => s.CvId.In(cvIds.ToList()))
                    .ToListAsync();

            if (sections == null)
            {
                return new Dictionary<Guid, ContactSection>();
            }

            return sections.ToDictionary(x => x.CvId, x => x.ToContactSection());
        }

        /// <inheritdoc/>
        public async Task UpdateAsync(ContactSection section)
        {
            using var session = this.DocumentStore.LightweightSession();

            var exists = await
                session.Query<ContactSectionDocument>().AnyAsync(s => s.CvId == section.CvId);

            if (exists)
            {
                Logger.LogInformation($"{section.Title} Section Already exists for {section.CvId}");

                return;
            }

            session.Update(section.ToContactSectionDocument());

            await session.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task<bool> GetSectionExistsAsync(Guid cvId)
        {
            using var session = this.DocumentStore.LightweightSession();

            var exists = await
                session
                    .Query<ContactSectionDocument>()
                    .AnyAsync(s => s.CvId == cvId);

            return exists;
        }
    }
}
