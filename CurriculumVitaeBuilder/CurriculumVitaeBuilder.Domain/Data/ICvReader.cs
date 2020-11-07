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
        /// Gets a CV by user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>The CV.</returns>
        public Task<Cv?> GetCvByUserAsync(Guid userId);

        /// <summary>
        /// Gets a CV by CV identifier.
        /// </summary>
        /// <param name="cvId">The user identifier.</param>
        /// <returns>Whether the Cv exists or not.</returns>
        public Task<bool> GetCvExistsAsync(Guid cvId);
    }
}
