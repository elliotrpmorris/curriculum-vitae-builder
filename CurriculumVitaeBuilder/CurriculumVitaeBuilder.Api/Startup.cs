// <copyright file="Startup.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Api
{
    using System;

    using CurriculumVitaeBuilder.Infrastructure.Data.Marten;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="config">The configuration provider.</param>
        public Startup(IConfiguration config)
        {
            if (config == null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            this.ConnectionString = config.GetConnectionString("Postgres");
        }

        private string ConnectionString { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services
               .AddControllers()
               .AddNewtonsoftJson(opts =>
               {
                   opts.SerializerSettings.ContractResolver =
                       new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();

                   opts.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
               });

            services.AddHealthChecks();

            services.AddMartenDataAccess(this.ConnectionString);

            services.RegisterCommandPipeline();

            services.RegisterGraphQL();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/health");
                endpoints.MapControllers();
            });
        }
    }
}
