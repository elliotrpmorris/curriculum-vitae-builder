// <copyright file="ICvSectionReader.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Domain.Data.Cv.CvSections
{
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Generic CV reader.
    /// </summary>
    /// <typeparam name="T">The section.</typeparam>
    public interface ICvSectionReader<T>
        where T : CvSection
    {
        /// <summary>
        /// Gets the section by CV identifier.
        /// </summary>
        /// <param name="cvId">The CV identifier.</param>
        /// <returns>The section.</returns>
        public Task<T> GetSectionByCvAsync(
            Guid cvId);
    }
}
