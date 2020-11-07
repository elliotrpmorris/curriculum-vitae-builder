// <copyright file="MartenEducationSectionStore.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Infrastructure.Data.Marten.CvSections.Education
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Chest.Core.Logging;

    using CurriculumVitaeBuilder.Domain.Data.CvSections;
    using CurriculumVitaeBuilder.Domain.Data.CvSections.Education;

    using global::Marten;

    /// <summary>
    /// Marten Contact Section store.
    /// </summary>
    internal sealed class MartenEducationSectionStore :
        ICvSectionReader<EducationSection>,
        ICvSectionWriter<EducationSection>,
        ICvSectionContentWriter<EducationSection>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MartenEducationSectionStore"/> class.
        /// </summary>
        /// <param name="documentStore">The Document Store Factory.</param>
        public MartenEducationSectionStore(
            IDocumentStore documentStore)
        {
            this.DocumentStore = documentStore
                ?? throw new ArgumentNullException(nameof(documentStore));
        }

        private IDocumentStore DocumentStore { get; }

        /// <inheritdoc/>
        public async Task AddAsync(EducationSection section)
        {
            using var session = this.DocumentStore.LightweightSession();

            var exists = await
                session.Query<EducationSectionDocument>().AnyAsync(s => s.CvId == section.CvId);

            if (exists)
            {
                Logger.LogInformation($"{section.Title} Section Already exists for {section.CvId}");

                return;
            }

            session.Store(section.ToEducationSectionDocument());

            await session.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task DeleteAsync(EducationSection section)
        {
            using var session = this.DocumentStore.LightweightSession();

            session.Delete(section.ToEducationSectionDocument());

            await session.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task<EducationSection?> GetSectionByCvAsync(Guid cvId)
        {
            using var session = this.DocumentStore.LightweightSession();

            var section = await
                session
                    .Query<EducationSectionDocument>()
                    .FirstOrDefaultAsync(s => s.CvId == cvId);

            if (section == null)
            {
                return null;
            }

            return section.ToEducationSection();
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
        public async Task UpdateAsync(EducationSection section)
        {
            using var session = this.DocumentStore.LightweightSession();

            var sectionToUpdate = await
                session
                    .Query<EducationSectionDocument>()
                    .FirstOrDefaultAsync(s => s.CvId == section.CvId);

            if (sectionToUpdate == null)
            {
                Logger.LogInformation($"{section.Title} Section Doesn't exist for {section.CvId}");

                return;
            }

            foreach (var establishment in section.EducationEstablishments)
            {
                // Check if Education Establishment exists.
                var existingEstablishmentIndex =
                    sectionToUpdate
                        .EducationEstablishments
                        .ToList()
                        .FindIndex(i => i.Name.Equals(establishment.Name));

                if (existingEstablishmentIndex == -1)
                {
                    // Add if doesn't exist.
                    sectionToUpdate.EducationEstablishments.Add(establishment);
                }
                else
                {
                    // Set to new updated properties if exists.
                    sectionToUpdate
                        .EducationEstablishments[existingEstablishmentIndex] =
                            new EducationEstablishment(
                                establishment.Name,
                                establishment.Start,
                                establishment.End);
                }
            }

            session.Update(sectionToUpdate);

            await session.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task<bool> GetSectionExistsAsync(Guid cvId)
        {
            using var session = this.DocumentStore.LightweightSession();

            var exists = await
                session
                    .Query<EducationSectionDocument>()
                    .AnyAsync(s => s.CvId == cvId);

            return exists;
        }

        /// <inheritdoc/>
        public async Task DeleteAsync(Guid cvId, string name)
        {
            using var session = this.DocumentStore.LightweightSession();

            var section = await
               session
                   .Query<EducationSectionDocument>()
                   .FirstOrDefaultAsync(s =>
                        s.CvId == cvId);

            if (section == null)
            {
                Logger.LogInformation($"Section Doesn't contain contact detail: {name}");

                return;
            }

            var establishmentToRemove = section
                .EducationEstablishments
                .FirstOrDefault(e => e.Name == name);

            section.EducationEstablishments.Remove(establishmentToRemove);

            session.Update(section);

            await session.SaveChangesAsync();
        }
    }
}
