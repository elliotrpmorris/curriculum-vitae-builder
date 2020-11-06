// <copyright file="UserQuery.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Api.Controllers.Query.UserRoot
{
    using CurriculumVitaeBuilder.Api.Controllers.Query.UserRoot.Types;
    using CurriculumVitaeBuilder.Domain.Data;
    using CurriculumVitaeBuilder.Domain.Data.User;

    using GraphQL.Types;

    public class UserQuery : ObjectGraphType<UserQueryContext>
    {
        public UserQuery(
            IUserReader userReader,
            ICvReader cvReader)
        {
            this.Field<UserType, User?>()
                .Name("info")
                .Description("User information")
                .ResolveAsync(context => userReader.GetUserByIdAsync(
                    context.Source.UserId));

            this.Field<CvType, Cv?>()
               .Name("cv")
               .Description("User information")
               .ResolveAsync(context => cvReader.GetCvByUserAsync(
                   context.Source.UserId));

            //this.Field<CvQuery>()
            //    .Name("cv")
            //    .Argument<NonNullGraphType<StringGraphType>>("cvId", "The cv identifier.")
            //    .Resolve(context =>
            //    {
            //        return new CvQueryContext(
            //            context.Source.UserId,
            //            context.GetArgument<Guid>("userId"));
            //    });
        }
    }
}
