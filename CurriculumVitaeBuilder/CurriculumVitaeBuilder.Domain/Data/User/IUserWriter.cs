// <copyright file="IUserWriter.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Domain.Data.User
{
    using System.Threading.Tasks;

    /// <summary>
    /// User Writer.
    /// </summary>
    public interface IUserWriter
    {
        /// <summary>
        /// Adds the user.
        /// </summary>
        /// <param name="user">The User.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public Task AddAsync(User user);

        // This interface could be expanded to cater for updating username and deleting users etc..
    }
}
