// <copyright file="ICvSectionWriter.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Domain.Data.Cv.CvSections
{
    using System.Threading.Tasks;

    /// <summary>
    /// Generic CV section writer.
    /// </summary>
    /// <typeparam name="T">The section.</typeparam>
    public interface ICvSectionWriter<T>
        where T : CvSection
    {
        /// <summary>
        /// Updates the section.
        /// </summary>
        /// <param name="section">The section.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public Task UpdateAsync(T section);

        /// <summary>
        /// Adds the section.
        /// </summary>
        /// <param name="section">The section.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public Task AddAsync(T section);

        /// <summary>
        /// Deletes the section.
        /// </summary>
        /// <param name="section">The section.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public Task DeleteAsync(T section);
    }
}
