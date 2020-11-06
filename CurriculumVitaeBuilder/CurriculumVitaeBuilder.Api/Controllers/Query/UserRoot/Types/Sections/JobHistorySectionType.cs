// <copyright file="JobHistorySectionType.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Api.Controllers.Query.UserRoot.Types.Sections
{
    using System;
    using System.Collections.Generic;

    using CurriculumVitaeBuilder.Domain.Data.CvSections;
    using CurriculumVitaeBuilder.Domain.Data.CvSections.JobHistory;

    using GraphQL.Types;

    public class JobHistorySectionType : ObjectGraphType<JobHistorySection>
    {
        public JobHistorySectionType()
        {
            this.Field<IdGraphType, Guid>("id")
                .Description("The section identifier.")
                .Resolve(context => context.Source.Id);

            this.Field<IdGraphType, Guid>("cvId")
                .Description("The cv identifier.")
                .Resolve(context => context.Source.CvId);

            this.Field(p => p.Title)
                .Name("title")
                .Description("The section title.");

            this.Field<
                ListGraphType<JobType>,
                IList<Job>?>("jobs")
                .Description("The jobs.")
                .Resolve(context => context.Source.Jobs);
        }
    }
}
