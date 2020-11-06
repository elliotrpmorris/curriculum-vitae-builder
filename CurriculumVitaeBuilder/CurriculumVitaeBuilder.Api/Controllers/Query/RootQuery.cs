// <copyright file="RootQuery.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Api.Controllers.Query
{
    using System;

    using CurriculumVitaeBuilder.Api.Controllers.Query.UserRoot;

    using GraphQL.Types;

    public class RootQuery : ObjectGraphType
    {
        public RootQuery()
        {
            this.Field<UserQuery>()
                .Name("user")
                .Argument<NonNullGraphType<StringGraphType>>("userId", "The user identifier.")
                .Resolve(context =>
                {
                    return new UserQueryContext(context.GetArgument<Guid>("userId"));
                });
        }
    }
}
