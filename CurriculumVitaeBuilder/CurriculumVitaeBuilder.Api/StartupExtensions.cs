// <copyright file="StartupExtensions.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Api
{
    using Chest.Core.Command;

    using CurriculumVitaeBuilder.Api.Controllers.Query;
    using CurriculumVitaeBuilder.Api.Controllers.Query.DataLoaders;
    using CurriculumVitaeBuilder.Api.Controllers.Query.UserRoot;
    using CurriculumVitaeBuilder.Api.Controllers.Query.UserRoot.Types;
    using CurriculumVitaeBuilder.Api.Controllers.Query.UserRoot.Types.Sections;

    using GraphQL;
    using GraphQL.DataLoader;
    using GraphQL.Http;
    using GraphQL.Types;

    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Extensions for startup.
    /// </summary>
    internal static class StartupExtensions
    {
        /// <summary>
        /// Register Command pipeline.
        /// </summary>
        /// <param name="services">The container builder.</param>
        /// <returns>A Container builder.</returns>
        public static IServiceCollection RegisterCommandPipeline(this IServiceCollection services)
        {
            services.AddCommandBus(
                typeof(CurriculumVitaeBuilder.Domain.Command.ICommandHandlerAssembly).Assembly);

            return services;
        }

        /// <summary>
        /// Registers GraphQL.
        /// </summary>
        /// <param name="services">The container builder.</param>
        /// <returns>A Container builder.</returns>
        public static IServiceCollection RegisterGraphQL(this IServiceCollection services)
        {
            services.AddScoped<IDependencyResolver>(s => new FuncDependencyResolver(s.GetRequiredService));

            services
                .AddSingleton<IDocumentWriter, DocumentWriter>();

            services
                .AddSingleton<IDocumentExecuter, DocumentExecuter>();

            services.AddScoped<RootQuery>();

            services
                .AddSingleton<IDataLoaderContextAccessor, DataLoaderContextAccessor>();

            services
                .AddSingleton<DataLoaderDocumentListener>();

            services
                .AddScoped<ISchema, RootSchema>();

            services
                .AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // Types
            services.AddScoped<UserQuery>();

            services.AddScoped<CvSectionDataLoader>();
            services.AddScoped<UserDataLoader>();

            services.AddScoped<UserType>();
            services.AddScoped<CvType>();

            services.AddScoped<BioSectionType>();

            services.AddScoped<ContactSectionType>();

            services.AddScoped<EducationSectionType>();
            services.AddScoped<EducationEstablishmentsType>();

            services.AddScoped<JobHistorySectionType>();
            services.AddScoped<JobType>();

            services.AddScoped<SkillsProfileSectionType>();
            services.AddScoped<SkillType>();

            return services;
        }
    }
}
