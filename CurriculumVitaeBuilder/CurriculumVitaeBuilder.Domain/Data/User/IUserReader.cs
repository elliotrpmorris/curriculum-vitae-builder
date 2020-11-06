// <copyright file="IUserReader.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Domain.Data.User
{
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// User reader.
    /// </summary>
    public interface IUserReader
    {
        public Task<User?> GetUserByIdAsync(Guid userId);
    }
}
