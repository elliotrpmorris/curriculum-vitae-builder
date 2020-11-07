// <copyright file="EducationSectionType.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Api.Controllers.Query.UserRoot.Types.Sections
{
    using System;
    using System.Collections.Generic;

    using CurriculumVitaeBuilder.Domain.Data.CvSections.Education;

    using GraphQL.Types;

    public class EducationSectionType : ObjectGraphType<EducationSection>
    {
        public EducationSectionType()
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
                ListGraphType<EducationEstablishmentsType>,
                IList<EducationEstablishment>?>("educationEstablishments")
                .Description("The education establishments.")
                .Resolve(context => context.Source.EducationEstablishments);
        }
    }
}
