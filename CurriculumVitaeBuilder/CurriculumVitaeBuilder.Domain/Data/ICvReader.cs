// <copyright file="ICvReader.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Domain.Data
{
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// CV reader.
    /// </summary>
    public interface ICvReader
    {
        /// <summary>
        /// Gets a CV by identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>The CV.</returns>
        public Task<Cv?> GetCvByUserAsync(Guid userId);
    }
}
