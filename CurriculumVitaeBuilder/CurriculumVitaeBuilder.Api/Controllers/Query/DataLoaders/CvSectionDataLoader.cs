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

    using GraphQL.DataLoader;

    public class CvSectionDataLoader
    {
        public CvSectionDataLoader(
           IDataLoaderContextAccessor contextAccessor,
           ICvSectionReader<BioSection> bioSection,
           ICvSectionReader<ContactSection> contactSection)
        {
            this.ContextAccessor = contextAccessor
               ?? throw new ArgumentNullException(nameof(contextAccessor));

            this.BioSection = bioSection
                ?? throw new ArgumentNullException(nameof(bioSection));

            this.ContactSection = contactSection
               ?? throw new ArgumentNullException(nameof(contactSection));
        }

        private IDataLoaderContextAccessor ContextAccessor { get; }

        private ICvSectionReader<BioSection> BioSection { get; }

        private ICvSectionReader<ContactSection> ContactSection { get; }

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
    }
}
