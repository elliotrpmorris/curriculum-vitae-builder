// <copyright file="UserDataLoader.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Api.Controllers.Query.DataLoaders
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using CurriculumVitaeBuilder.Domain.Data.User;

    using GraphQL.DataLoader;

    public class UserDataLoader
    {
        public UserDataLoader(
            IDataLoaderContextAccessor contextAccessor,
            IUserReader userReader)
        {
            this.ContextAccessor = contextAccessor
              ?? throw new ArgumentNullException(nameof(contextAccessor));

            this.UserReader = userReader
                ?? throw new ArgumentNullException(nameof(userReader));
        }

        private IDataLoaderContextAccessor ContextAccessor { get; }

        private IUserReader UserReader { get; }

        public async Task<string> GetUserNameAsync(
           Guid userId)
        {
            if (userId == default)
            {
                throw new ArgumentException(nameof(userId));
            }

            var loaderKey = $"{nameof(this.GetUserNameAsync)}";

            var dataLoader = this.ContextAccessor.Context
                .GetOrAddBatchLoader<Guid, string>(
                    loaderKey,
                    ids => this.UserReader
                        .GetUserNamesAsync(ids.ToList()));

            return await dataLoader.LoadAsync(userId);
        }
    }
}
