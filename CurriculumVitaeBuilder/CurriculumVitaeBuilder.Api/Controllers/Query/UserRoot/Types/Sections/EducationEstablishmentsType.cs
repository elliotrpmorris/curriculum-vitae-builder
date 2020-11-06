// <copyright file="EducationEstablishmentsType.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Api.Controllers.Query.UserRoot.Types.Sections
{
    using CurriculumVitaeBuilder.Domain.Data.CvSections.Education;

    using GraphQL.Types;

    public class EducationEstablishmentsType : ObjectGraphType<EducationEstablishment>
    {
        public EducationEstablishmentsType()
        {
            this.Field(p => p.Name)
                .Name("name")
                .Description("The education establishment name.");

            this.Field<StringGraphType, string>("start")
                .Description("The education establishment start.")
                .Resolve(context => context.Source.Start.ToString("u"));

            this.Field<StringGraphType, string>("end")
                .Description("The education establishment end.")
                .Resolve(context => context.Source.End.ToString("u"));
        }
    }
}
