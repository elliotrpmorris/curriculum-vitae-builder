// <copyright file="ICvWriter.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Domain.Data
{
    using System.Threading.Tasks;

    /// <summary>
    /// CV writer.
    /// </summary>
    public interface ICvWriter
    {
        /// <summary>
        /// Adds the CV.
        /// </summary>
        /// <param name="cv">The CV.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public Task AddAsync(Cv cv);

        // This interface could be expanded to cater for deleting cv's or allowing for multi
        // cvs per user.
    }
}
