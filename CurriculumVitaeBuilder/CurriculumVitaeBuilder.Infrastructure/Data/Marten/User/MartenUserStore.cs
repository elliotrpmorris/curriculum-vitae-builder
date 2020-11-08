// <copyright file="MartenUserStore.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Infrastructure.Data.Marten.User
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Chest.Core.Logging;

    using CurriculumVitaeBuilder.Domain.Data.User;

    using global::Marten;

    /// <summary>
    /// Marten user store.
    /// </summary>
    internal sealed class MartenUserStore :
        IUserReader,
        IUserWriter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MartenUserStore"/> class.
        /// </summary>
        /// <param name="documentStore">The Document Store Factory.</param>
        public MartenUserStore(
            IDocumentStore documentStore)
        {
            this.DocumentStore = documentStore
                ?? throw new ArgumentNullException(nameof(documentStore));
        }

        private IDocumentStore DocumentStore { get; }

        /// <inheritdoc/>
        public async Task AddAsync(User user)
        {
            using var session = this.DocumentStore.LightweightSession();

            var exists = await
                session.Query<UserDocument>().AnyAsync(s => s.UserName == user.UserName);

            if (exists)
            {
                Logger.LogInformation($"User Already exists for {user.UserName}");

                return;
            }

            session.Store(user.ToUserDocument());

            await session.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task<IReadOnlyList<User>> GetUsersAsync()
        {
            using var session = this.DocumentStore.LightweightSession();

            var users = await
                session
                    .Query<UserDocument>()
                    .ToUser()
                    .ToListAsync();

            return users;
        }

        /// <inheritdoc/>
        public async Task<User?> GetUserByIdAsync(Guid userId)
        {
            using var session = this.DocumentStore.LightweightSession();

            var user = await
                session
                    .Query<UserDocument>()
                    .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                return null;
            }

            return user.ToUser();
        }

        /// <inheritdoc/>
        public async Task<bool> GetUserExistsAsync(string userName)
        {
            using var session = this.DocumentStore.LightweightSession();

            var exists = await
                session
                    .Query<UserDocument>()
                    .AnyAsync(s => s.UserName == userName);

            return exists;
        }

        /// <inheritdoc/>
        public async Task<IDictionary<Guid, string>> GetUserNamesAsync(
            IReadOnlyCollection<Guid> userIds)
        {
            using var session = this.DocumentStore.LightweightSession();

            var sections = await
                session
                    .Query<UserDocument>()
                    .Where(s => s.Id.In(userIds.ToList()))
                    .ToListAsync();

            if (sections == null)
            {
                return new Dictionary<Guid, string>();
            }

            return sections.ToDictionary(x => x.Id, x => x.UserName);
        }
    }
}
