// <copyright file="UserExtensions.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Infrastructure.Data.Marten.User
{
    using System;

    using CurriculumVitaeBuilder.Domain.Data.User;

    /// <summary>
    /// User extensions.
    /// </summary>
    internal static class UserExtensions
    {
        /// <summary>
        /// Convert From Document to Data Object.
        /// </summary>
        /// <param name="user">The User Document. </param>
        /// <returns>The User Data Object.</returns>
        public static User ToUser(this UserDocument user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return new User(
                user.Id,
                user.UserName,
                user.CreatedAt);
        }
    }
}
