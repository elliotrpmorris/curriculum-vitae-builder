// <copyright file="MartenSkillsProfileSectionStore.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Infrastructure.Data.Marten.CvSections.SkillsProfile
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Chest.Core.Logging;

    using CurriculumVitaeBuilder.Domain.Data.CvSections;
    using CurriculumVitaeBuilder.Domain.Data.CvSections.SkillsProfile;

    using global::Marten;

    /// <summary>
    /// Marten Contact Section store.
    /// </summary>
    internal sealed class MartenSkillsProfileSectionStore :
        ICvSectionReader<SkillsProfileSection>,
        ICvSectionWriter<SkillsProfileSection>,
        ICvSectionContentWriter<SkillsProfileSection>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MartenSkillsProfileSectionStore"/> class.
        /// </summary>
        /// <param name="documentStore">The Document Store Factory.</param>
        public MartenSkillsProfileSectionStore(
            IDocumentStore documentStore)
        {
            this.DocumentStore = documentStore
                ?? throw new ArgumentNullException(nameof(documentStore));
        }

        private IDocumentStore DocumentStore { get; }

        /// <inheritdoc/>
        public async Task AddAsync(SkillsProfileSection section)
        {
            using var session = this.DocumentStore.LightweightSession();

            var exists = await
                session.Query<SkillsProfileSectionDocument>().AnyAsync(s => s.CvId == section.CvId);

            if (exists)
            {
                Logger.LogInformation($"{section.Title} Section Already exists for {section.CvId}");

                return;
            }

            session.Store(section.ToSkillsProfileSectionDocument());

            await session.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task DeleteAsync(SkillsProfileSection section)
        {
            using var session = this.DocumentStore.LightweightSession();

            session.Delete(section.ToSkillsProfileSectionDocument());

            await session.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task<SkillsProfileSection?> GetSectionByCvAsync(Guid cvId)
        {
            using var session = this.DocumentStore.LightweightSession();

            var section = await
                session
                    .Query<SkillsProfileSectionDocument>()
                    .FirstOrDefaultAsync(s => s.CvId == cvId);

            if (section == null)
            {
                return null;
            }

            return section.ToSkillsProfile();
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
        public async Task UpdateAsync(SkillsProfileSection section)
        {
            using var session = this.DocumentStore.LightweightSession();

            var sectionToUpdate = await
                 session
                     .Query<SkillsProfileSectionDocument>()
                     .FirstOrDefaultAsync(s => s.CvId == section.CvId);

            if (sectionToUpdate == null)
            {
                Logger.LogInformation($"{section.Title} Section Doesn't exist for {section.CvId}");

                return;
            }

            foreach (var skill in section.Skills)
            {
                // Check if Skill exists.
                var existingEstablishmentIndex =
                    sectionToUpdate
                        .Skills
                        .ToList()
                        .FindIndex(i => i.Name.ToLower().Equals(skill.Name.ToLower()));

                if (existingEstablishmentIndex == -1)
                {
                    // Add if doesn't exist.
                    sectionToUpdate.Skills.Add(skill);
                }
                else
                {
                    // Set to new updated properties if exists.
                    sectionToUpdate
                        .Skills[existingEstablishmentIndex] =
                            new Skill(
                                skill.Name,
                                skill.Description,
                                skill.AchievedAt);
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
                    .Query<SkillsProfileSectionDocument>()
                    .AnyAsync(s => s.CvId == cvId);

            return exists;
        }

        /// <inheritdoc/>
        public async Task DeleteAsync(Guid cvId, string name)
        {
            using var session = this.DocumentStore.LightweightSession();

            var section = await
               session
                   .Query<SkillsProfileSectionDocument>()
                   .FirstOrDefaultAsync(s =>
                        s.CvId == cvId);

            if (section == null)
            {
                Logger.LogInformation($"Section Doesn't contain contact detail: {name}");

                return;
            }

            var skilltoRemove = section
                .Skills
                .FirstOrDefault(e => e.Name.ToLower() == name.ToLower());

            section.Skills.Remove(skilltoRemove);

            session.Update(section);

            await session.SaveChangesAsync();
        }
    }
}
