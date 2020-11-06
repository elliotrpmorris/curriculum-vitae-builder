// <copyright file="MartenUserStore.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Infrastructure.Data.Marten.User
{
    using System;
    using System.Threading.Tasks;

    using CurriculumVitaeBuilder.Domain.Data.User;
    using CurriculumVitaeBuilder.Infrastructure.Data.Marten;

    using global::Marten;

    /// <summary>
    /// Marten user store.
    /// </summary>
    internal sealed class MartenUserStore : IUserReader
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MartenUserStore"/> class.
        /// </summary>
        /// <param name="documentStoreFactory">The Document Store Factory.</param>
        public MartenUserStore(
            MartenDocumentStoreFactory documentStoreFactory)
        {
            this.DocumentStoreFactory = documentStoreFactory
                ?? throw new ArgumentNullException(nameof(documentStoreFactory));

            this.DocumentStore = this.DocumentStoreFactory.GetDocumentStore();
        }

        private MartenDocumentStoreFactory DocumentStoreFactory { get; }

        private DocumentStore DocumentStore { get; }

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
