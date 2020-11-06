﻿// <copyright file="ServiceExtensions.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Infrastructure.Data.Marten
{
    using CurriculumVitaeBuilder.Domain.Data;
    using CurriculumVitaeBuilder.Domain.Data.CvSections;
    using CurriculumVitaeBuilder.Domain.Data.CvSections.Bio;
    using CurriculumVitaeBuilder.Domain.Data.CvSections.Contact;
    using CurriculumVitaeBuilder.Domain.Data.User;
    using CurriculumVitaeBuilder.Infrastructure.Data.Marten.CvSections.Bio;
    using CurriculumVitaeBuilder.Infrastructure.Data.Marten.CvSections.Contact;
    using CurriculumVitaeBuilder.Infrastructure.Data.Marten.User;

    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Service Extensions.
    /// </summary>
    public static class ServiceExtensions
    {
        /// <summary>
        /// Registers Marten Data Access.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <returns>The service collections.</returns>
        public static IServiceCollection AddMartenDataAccess(
            this IServiceCollection services)
        {
            services.AddTransient<MartenDocumentStoreFactory>();

            services.AddScoped<IUserReader, MartenUserStore>();

            services
                .AddScoped<ICvReader, MartenCvStore>();

            services
                .AddScoped<ICvSectionReader<BioSection>, MartenBioSectionStore>()
                .AddScoped<ICvSectionWriter<BioSection>, MartenBioSectionStore>();

            services
                .AddScoped<ICvSectionReader<ContactSection>, MartenContactSectionStore>()
                .AddScoped<ICvSectionWriter<ContactSection>, MartenContactSectionStore>();

            return services;
        }
    }
}
