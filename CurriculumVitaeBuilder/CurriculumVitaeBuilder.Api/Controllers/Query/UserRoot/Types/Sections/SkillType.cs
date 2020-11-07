// <copyright file="SkillType.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Api.Controllers.Query.UserRoot.Types.Sections
{
    using CurriculumVitaeBuilder.Domain.Data.CvSections.SkillsProfile;

    using GraphQL.Types;

    public class SkillType : ObjectGraphType<Skill>
    {
        public SkillType()
        {
            this.Field(p => p.Name)
                .Name("name")
                .Description("The skill name.");

            this.Field(p => p.Description)
                .Name("description")
                .Description("The skill description.");

            this.Field<StringGraphType, string?>("achievdAt")
                .Description("The time the skill was gained.")
                .Resolve(context => context.Source.AchievedAt?.ToString("u"));
        }
    }
}
