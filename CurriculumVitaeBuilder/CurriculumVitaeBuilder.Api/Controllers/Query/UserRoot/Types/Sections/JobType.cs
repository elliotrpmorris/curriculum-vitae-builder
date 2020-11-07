// <copyright file="JobType.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Api.Controllers.Query.UserRoot.Types.Sections
{
    using CurriculumVitaeBuilder.Domain.Data.CvSections.JobHistory;

    using GraphQL.Types;

    public class JobType : ObjectGraphType<Job>
    {
        public JobType()
        {
            this.Field(p => p.JobTitle)
                .Name("jobTitle")
                .Description("The job title.");

            this.Field(p => p.Employer)
                .Name("employer")
                .Description("The employer.");

            this.Field<StringGraphType, string>("start")
                .Description("The education establishment start.")
                .Resolve(context => context.Source.Start.ToString("u"));

            this.Field<StringGraphType, string>("end")
                .Description("The education establishment end.")
                .Resolve(context => context.Source.End.ToString("u"));

            this.Field<StringGraphType, string?>("description")
                .Description("The description of the role.")
                .Resolve(context => context.Source.Description);
        }
    }
}
