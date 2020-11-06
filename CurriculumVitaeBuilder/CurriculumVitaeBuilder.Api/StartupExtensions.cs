// <copyright file="StartupExtensions.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Api
{
    using CurriculumVitaeBuilder.Api.Controllers.Query;
    using CurriculumVitaeBuilder.Api.Controllers.Query.UserRoot;
    using CurriculumVitaeBuilder.Api.Controllers.Query.UserRoot.Types;

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
            services.AddScoped<UserType>();
            services.AddScoped<UserQuery>();

            return services;
        }
    }
}
