// <copyright file="SkillsProfileSectionType.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Api.Controllers.Query.UserRoot.Types.Sections
{
    using System;
    using System.Collections.Generic;

    using CurriculumVitaeBuilder.Domain.Data.CvSections.SkillsProfile;

    using GraphQL.Types;

    public class SkillsProfileSectionType : ObjectGraphType<SkillsProfileSection>
    {
        public SkillsProfileSectionType()
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
                ListGraphType<SkillType>,
                IList<Skill>?>("skills")
                .Description("The skills.")
                .Resolve(context => context.Source.Skills);
        }
    }
}
