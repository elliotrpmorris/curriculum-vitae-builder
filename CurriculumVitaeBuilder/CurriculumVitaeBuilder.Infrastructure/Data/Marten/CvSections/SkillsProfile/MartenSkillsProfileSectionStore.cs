// <copyright file="MartenSkillsProfileSectionStore.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Infrastructure.Data.Marten.CvSections.SkillsProfile
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CurriculumVitaeBuilder.Domain.Data.CvSections;
    using CurriculumVitaeBuilder.Domain.Data.CvSections.SkillsProfile;

    using global::Marten;

    /// <summary>
    /// Marten Contact Section store.
    /// </summary>
    internal sealed class MartenSkillsProfileSectionStore
        : ICvSectionReader<SkillsProfileSection>, ICvSectionWriter<SkillsProfileSection>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MartenSkillsProfileSectionStore"/> class.
        /// </summary>
        /// <param name="documentStoreFactory">The Document Store Factory.</param>
        public MartenSkillsProfileSectionStore(
            MartenDocumentStoreFactory documentStoreFactory)
        {
            this.DocumentStoreFactory = documentStoreFactory
                ?? throw new ArgumentNullException(nameof(documentStoreFactory));

            this.DocumentStore = this.DocumentStoreFactory.GetDocumentStore();
        }

        private MartenDocumentStoreFactory DocumentStoreFactory { get; }

        private DocumentStore DocumentStore { get; }

        /// <inheritdoc/>
        public Task AddAsync(SkillsProfileSection section)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task DeleteAsync(SkillsProfileSection section)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public async Task<SkillsProfileSection?> GetSectionByCvAsync(Guid cvId)
        {
            using var session = this.DocumentStore.LightweightSession();

            var section = await
                session
                    .Query<SkillsProfileSectionDocument>()
                    .FirstOrDefaultAsync(s => s.CvId == cvId);

            return section.ToSkillsProfile() ?? null;
        }

        /// <inheritdoc/>
        public async Task<IDictionary<Guid, SkillsProfileSection>> GetSectionByCvAsync(
            IReadOnlyCollection<Guid> cvIds)
        {
            using var session = this.DocumentStore.LightweightSession();

            var sections = await
                session
                    .Query<SkillsProfileSectionDocument>()
                    .Where(s => s.CvId.In(cvIds.ToList()))
                    .ToListAsync();

            if (sections == null)
            {
                return new Dictionary<Guid, SkillsProfileSection>();
            }

            return sections.ToDictionary(x => x.CvId, x => x.ToSkillsProfile());
        }

        /// <inheritdoc/>
        public Task UpdateAsync(SkillsProfileSection section)
        {
            throw new NotImplementedException();
        }
    }
}
