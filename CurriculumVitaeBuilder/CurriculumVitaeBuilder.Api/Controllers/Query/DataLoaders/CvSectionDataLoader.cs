// <copyright file="CvSectionDataLoader.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Api.Controllers.Query.DataLoaders
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using CurriculumVitaeBuilder.Domain.Data.CvSections;
    using CurriculumVitaeBuilder.Domain.Data.CvSections.Bio;
    using CurriculumVitaeBuilder.Domain.Data.CvSections.Contact;
    using CurriculumVitaeBuilder.Domain.Data.CvSections.Education;
    using CurriculumVitaeBuilder.Domain.Data.CvSections.SkillsProfile;

    using GraphQL.DataLoader;

    public class CvSectionDataLoader
    {
        public CvSectionDataLoader(
           IDataLoaderContextAccessor contextAccessor,
           ICvSectionReader<BioSection> bioSection,
           ICvSectionReader<ContactSection> contactSection,
           ICvSectionReader<EducationSection> educationSection,
           ICvSectionReader<SkillsProfileSection> skillsProfileSection,
           ICvSectionReader<JobHistorySection> jobHistorySection)
        {
            this.ContextAccessor = contextAccessor
               ?? throw new ArgumentNullException(nameof(contextAccessor));

            this.BioSection = bioSection
                ?? throw new ArgumentNullException(nameof(bioSection));

            this.ContactSection = contactSection
               ?? throw new ArgumentNullException(nameof(contactSection));

            this.EducationSection = educationSection
               ?? throw new ArgumentNullException(nameof(educationSection));

            this.SkillsProfileSection = skillsProfileSection
               ?? throw new ArgumentNullException(nameof(skillsProfileSection));

            this.JobHistorySection = jobHistorySection
               ?? throw new ArgumentNullException(nameof(jobHistorySection));
        }

        private IDataLoaderContextAccessor ContextAccessor { get; }

        private ICvSectionReader<BioSection> BioSection { get; }

        private ICvSectionReader<ContactSection> ContactSection { get; }

        private ICvSectionReader<EducationSection> EducationSection { get; }

        private ICvSectionReader<SkillsProfileSection> SkillsProfileSection { get; }

        private ICvSectionReader<JobHistorySection> JobHistorySection { get; }

        public async Task<BioSection?> GetBioSectionByCvAsync(
           Guid cvId)
        {
            if (cvId == default)
            {
                throw new ArgumentException(nameof(cvId));
            }

            var loaderKey = $"{nameof(this.GetBioSectionByCvAsync)}";

            var dataLoader = this.ContextAccessor.Context
                .GetOrAddBatchLoader<Guid, BioSection>(
                    loaderKey,
                    ids => this.BioSection
                        .GetSectionByCvAsync(ids.ToList()));

            return await dataLoader.LoadAsync(cvId);
        }

        public async Task<ContactSection?> GetContactSectionByCvAsync(
           Guid cvId)
        {
            if (cvId == default)
            {
                throw new ArgumentException(nameof(cvId));
            }

            var loaderKey = $"{nameof(this.GetContactSectionByCvAsync)}";

            var dataLoader = this.ContextAccessor.Context
                .GetOrAddBatchLoader<Guid, ContactSection>(
                    loaderKey,
                    ids => this.ContactSection
                        .GetSectionByCvAsync(ids.ToList()));

            return await dataLoader.LoadAsync(cvId);
        }

        public async Task<EducationSection?> GetEducationSectionByCvAsync(
          Guid cvId)
        {
            if (cvId == default)
            {
                throw new ArgumentException(nameof(cvId));
            }

            var loaderKey = $"{nameof(this.GetEducationSectionByCvAsync)}";

            var dataLoader = this.ContextAccessor.Context
                .GetOrAddBatchLoader<Guid, EducationSection>(
                    loaderKey,
                    ids => this.EducationSection
                        .GetSectionByCvAsync(ids.ToList()));

            return await dataLoader.LoadAsync(cvId);
        }

        public async Task<SkillsProfileSection?> GetSkillsProfileSectionByCvAsync(
          Guid cvId)
        {
            if (cvId == default)
            {
                throw new ArgumentException(nameof(cvId));
            }

            var loaderKey = $"{nameof(this.GetSkillsProfileSectionByCvAsync)}";

            var dataLoader = this.ContextAccessor.Context
                .GetOrAddBatchLoader<Guid, SkillsProfileSection>(
                    loaderKey,
                    ids => this.SkillsProfileSection
                        .GetSectionByCvAsync(ids.ToList()));

            return await dataLoader.LoadAsync(cvId);
        }

        public async Task<JobHistorySection?> GetJobHistorySectionByCvAsync(
          Guid cvId)
        {
            if (cvId == default)
            {
                throw new ArgumentException(nameof(cvId));
            }

            var loaderKey = $"{nameof(this.GetJobHistorySectionByCvAsync)}";

            var dataLoader = this.ContextAccessor.Context
                .GetOrAddBatchLoader<Guid, JobHistorySection>(
                    loaderKey,
                    ids => this.JobHistorySection
                        .GetSectionByCvAsync(ids.ToList()));

            return await dataLoader.LoadAsync(cvId);
        }
    }
}
