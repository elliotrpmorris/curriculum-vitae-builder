// <copyright file="UserQuery.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Api.Controllers.Query.UserRoot
{
    using CurriculumVitaeBuilder.Api.Controllers.Query.UserRoot.Types;
    using CurriculumVitaeBuilder.Domain.Data.User;

    using GraphQL.Types;

    public class UserQuery : ObjectGraphType<UserQueryContext>
    {
        public UserQuery(
            IUserReader userReader)
        {
            this.Field<UserType, User?>()
                .Name("info")
                .Description("User information")
                .ResolveAsync(context => userReader.GetUserByIdAsync(
                    context.Source.UserId));
        }
    }
}
