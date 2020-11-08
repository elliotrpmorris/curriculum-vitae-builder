// <copyright file="RootQuery.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Api.Controllers.Query
{
    using System;
    using System.Collections.Generic;

    using CurriculumVitaeBuilder.Api.Controllers.Query.UserRoot;
    using CurriculumVitaeBuilder.Api.Controllers.Query.UserRoot.Types;
    using CurriculumVitaeBuilder.Domain.Data;
    using CurriculumVitaeBuilder.Domain.Data.User;

    using GraphQL.Types;

    public class RootQuery : ObjectGraphType
    {
        public RootQuery(
            IUserReader userReader,
            ICvReader cvReader)
        {
            this.Field<UserQuery>()
                .Name("user")
                .Description("Displays user inofrmation.")
                .Argument<NonNullGraphType<StringGraphType>>("userId", "The user identifier.")
                .Resolve(context =>
                {
                    return new UserQueryContext(context.GetArgument<Guid>("userId"));
                });

            this.Field<ListGraphType<UserType>, IReadOnlyList<User>>("users")
                .Description("All the users in the system.")
                .ResolveAsync(context => userReader.GetUsersAsync());

            this.Field<ListGraphType<CvType>, IReadOnlyList<Cv>>("cvs")
                .Description("All the CVs in the system.")
                .ResolveAsync(context => cvReader.GetCvsAsync());
        }
    }
}
