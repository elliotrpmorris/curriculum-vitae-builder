// <copyright file="CvExtensions.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Infrastructure.Data.Marten
{
    using System;

    using CurriculumVitaeBuilder.Domain.Data;

    /// <summary>
    /// CV extensions.
    /// </summary>
    internal static class CvExtensions
    {
        /// <summary>
        /// Convert From Document to Data Object.
        /// </summary>
        /// <param name="cv">The Gateway Document. </param>
        /// <returns>The Gateway Data Object.</returns>
        public static Cv ToBioSection(this CvDocument cv)
        {
            if (cv == null)
            {
                throw new ArgumentNullException(nameof(cv));
            }

            return new Cv(
                cv.Id,
                cv.UserId);
        }
    }
}
