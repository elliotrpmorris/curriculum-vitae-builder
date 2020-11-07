// <copyright file="RootQuery.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Api.Controllers.Query
{
    using System;
    using System.Collections.Generic;

    using CurriculumVitaeBuilder.Api.Controllers.Query.UserRoot;
    using CurriculumVitaeBuilder.Api.Controllers.Query.UserRoot.Types;
    using CurriculumVitaeBuilder.Domain.Data.User;

    using GraphQL.Types;

    public class RootQuery : ObjectGraphType
    {
        public RootQuery(
            IUserReader userReader)
        {
            this.Field<UserQuery>()
                .Name("user")
                .Description("The user")
                .Argument<NonNullGraphType<StringGraphType>>("userId", "The user identifier.")
                .Resolve(context =>
                {
                    return new UserQueryContext(context.GetArgument<Guid>("userId"));
                });

            this.Field<ListGraphType<UserType>, IReadOnlyList<User>>("users")
                .Description("The users.")
                .ResolveAsync(context => userReader.GetUsersAsync());
        }
    }
}
