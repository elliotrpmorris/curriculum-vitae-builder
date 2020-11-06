// <copyright file="MartenUserStore.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Infrastructure.Data.Marten.User
{
    using System;
    using System.Threading.Tasks;

    using CurriculumVitaeBuilder.Domain.Data.User;

    using global::Marten;

    /// <summary>
    /// Marten user store.
    /// </summary>
    internal sealed class MartenUserStore : IUserReader
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
        public async Task<User?> GetUserByIdAsync(Guid userId)
        {
            using var session = this.DocumentStore.LightweightSession();

            var user = await
                session
                    .Query<UserDocument>()
                    .FirstOrDefaultAsync(u => u.Id == userId);

            return user.ToUser() ?? null;
        }
    }
}
