// <copyright file="MartenEducationSectionStore.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Infrastructure.Data.Marten.CvSections.Education
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CurriculumVitaeBuilder.Domain.Data.CvSections;
    using CurriculumVitaeBuilder.Domain.Data.CvSections.Education;

    using global::Marten;

    /// <summary>
    /// Marten Contact Section store.
    /// </summary>
    internal sealed class MartenEducationSectionStore
        : ICvSectionReader<EducationSection>, ICvSectionWriter<EducationSection>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MartenEducationSectionStore"/> class.
        /// </summary>
        /// <param name="documentStoreFactory">The Document Store Factory.</param>
        public MartenEducationSectionStore(
            MartenDocumentStoreFactory documentStoreFactory)
        {
            this.DocumentStoreFactory = documentStoreFactory
                ?? throw new ArgumentNullException(nameof(documentStoreFactory));

            this.DocumentStore = this.DocumentStoreFactory.GetDocumentStore();
        }

        private MartenDocumentStoreFactory DocumentStoreFactory { get; }

        private DocumentStore DocumentStore { get; }

        /// <inheritdoc/>
        public Task AddAsync(EducationSection section)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task DeleteAsync(EducationSection section)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public async Task<EducationSection?> GetSectionByCvAsync(Guid cvId)
        {
            using var session = this.DocumentStore.LightweightSession();

            var section = await
                session
                    .Query<EducationSectionDocument>()
                    .FirstOrDefaultAsync(s => s.CvId == cvId);

            return section.ToEducationSection() ?? null;
        }

        /// <inheritdoc/>
        public async Task<IDictionary<Guid, EducationSection>> GetSectionByCvAsync(
            IReadOnlyCollection<Guid> cvIds)
        {
            using var session = this.DocumentStore.LightweightSession();

            var sections = await
                session
                    .Query<EducationSectionDocument>()
                    .Where(s => s.CvId.In(cvIds.ToList()))
                    .ToListAsync();

            if (sections == null)
            {
                return new Dictionary<Guid, EducationSection>();
            }

            return sections.ToDictionary(x => x.CvId, x => x.ToEducationSection());
        }

        /// <inheritdoc/>
        public Task UpdateAsync(EducationSection section)
        {
            throw new NotImplementedException();
        }
    }
}
