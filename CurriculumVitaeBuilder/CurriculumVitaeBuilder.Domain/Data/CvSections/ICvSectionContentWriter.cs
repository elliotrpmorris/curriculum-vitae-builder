// <copyright file="ICvSectionContentWriter.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Domain.Data.CvSections
{
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// CV Section Content Writer.
    /// </summary>
    /// <typeparam name="T">The section.</typeparam>
    public interface ICvSectionContentWriter<T>
        where T : CvSection
    {
        /// <summary>
        /// Deletes the selected content item.
        /// </summary>
        /// <param name="cvId">The cv identifier.</param>
        /// <param name="name">The name of the content item.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public Task DeleteAsync(
            Guid cvId,
            string name);
    }
}
