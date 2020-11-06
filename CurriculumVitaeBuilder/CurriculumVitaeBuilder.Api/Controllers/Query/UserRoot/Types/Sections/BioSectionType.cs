// <copyright file="BioSectionType.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Api.Controllers.Query.UserRoot.Types.Sections
{
    using System;

    using CurriculumVitaeBuilder.Domain.Data.CvSections.Bio;

    using GraphQL.Types;

    public class BioSectionType : ObjectGraphType<BioSection>
    {
        public BioSectionType()
        {
            this.Field<IdGraphType, Guid>("id")
                .Description("The section identifier.")
                .Resolve(context => context.Source.Id);

            this.Field<IdGraphType, Guid>("cvId")
                .Description("The cv identifier.")
                .Resolve(context => context.Source.CvId);

            this.Field(p => p.City)
                .Name("city")
                .Description("The city.");

            this.Field(p => p.FullName)
               .Name("fullName")
               .Description("The full name.");
        }
    }
}
