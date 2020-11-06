// <copyright file="ContactSectionType.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Api.Controllers.Query.UserRoot.Types.Sections
{
    using System;

    using CurriculumVitaeBuilder.Domain.Data.CvSections.Contact;

    using GraphQL.Types;

    public class ContactSectionType : ObjectGraphType<ContactSection>
    {
        public ContactSectionType()
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

            this.Field(p => p.ContactDetails)
                .Name("contactDetails")
                .Description("The contact details.");
        }
    }
}
