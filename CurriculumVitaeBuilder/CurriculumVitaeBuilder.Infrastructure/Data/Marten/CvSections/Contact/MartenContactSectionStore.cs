// <copyright file="MartenContactSectionStore.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Infrastructure.Data.Marten.CvSections.Contact
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

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
        /// <param name="documentStoreFactory">The Document Store Factory.</param>
        public MartenContactSectionStore(
            MartenDocumentStoreFactory documentStoreFactory)
        {
            this.DocumentStoreFactory = documentStoreFactory
                ?? throw new ArgumentNullException(nameof(documentStoreFactory));

            this.DocumentStore = this.DocumentStoreFactory.GetDocumentStore();
        }

        private MartenDocumentStoreFactory DocumentStoreFactory { get; }

        private DocumentStore DocumentStore { get; }

        /// <inheritdoc/>
        public Task AddAsync(ContactSection section)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task DeleteAsync(ContactSection section)
        {
            throw new NotImplementedException();
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
        public Task UpdateAsync(ContactSection section)
        {
            throw new NotImplementedException();
        }
    }
}
