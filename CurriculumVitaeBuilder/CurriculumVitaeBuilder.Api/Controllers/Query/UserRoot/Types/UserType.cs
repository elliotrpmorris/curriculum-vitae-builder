// <copyright file="UserType.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Api.Controllers.Query.UserRoot.Types
{
    using System;

    using CurriculumVitaeBuilder.Domain.Data.User;

    using GraphQL.Types;

    public class UserType : ObjectGraphType<User>
    {
        public UserType()
        {
            this.Field<IdGraphType, Guid>("id")
                .Description("The user identifier.")
                .Resolve(context => context.Source.Id);

            this.Field(p => p.UserName)
                .Name("userName")
                .Description("The user name.");

            this.Field<StringGraphType, string?>("createdAt")
                .Description("The time the user was created.")
                .Resolve(context => context.Source.CreatedAt.ToString("u"));
        }
    }
}
