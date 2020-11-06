// <copyright file="CvType.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Api.Controllers.Query.UserRoot.Types
{
    using System;

    using CurriculumVitaeBuilder.Api.Controllers.Query.DataLoaders;
    using CurriculumVitaeBuilder.Api.Controllers.Query.UserRoot.Types.Sections;
    using CurriculumVitaeBuilder.Domain.Data;
    using CurriculumVitaeBuilder.Domain.Data.CvSections.Bio;
    using CurriculumVitaeBuilder.Domain.Data.CvSections.Contact;

    using GraphQL.Types;

    public class CvType : ObjectGraphType<Cv>
    {
        public CvType(
            CvSectionDataLoader cvSectionDataLoader)
        {
            this.Field<IdGraphType, Guid>("id")
                .Description("The cv identifier.")
                .Resolve(context => context.Source.Id);

            this.Field<IdGraphType, Guid>("userId")
                .Description("The user identifier.")
                .Resolve(context => context.Source.UserId);

            this.Field<BioSectionType, BioSection?>("bio")
                .Description("The bio section of the cv.")
                .ResolveAsync(context => cvSectionDataLoader.GetBioSectionByCvAsync(
                    context.Source.Id));

            this.Field<ContactSectionType, ContactSection?>("contact")
               .Description("The contact section of the cv.")
               .ResolveAsync(context => cvSectionDataLoader.GetContactSectionByCvAsync(
                   context.Source.Id));
        }
    }
}
