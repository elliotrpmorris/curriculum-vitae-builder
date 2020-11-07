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
        /// <param name="cv">The CV Document. </param>
        /// <returns>The Cv Data Object.</returns>
        public static Cv ToCV(this CvDocument cv)
        {
            if (cv == null)
            {
                throw new ArgumentNullException(nameof(cv));
            }

            return new Cv(
                cv.Id,
                cv.UserId);
        }

        /// <summary>
        /// Convert Document to Data Object to Document.
        /// </summary>
        /// <param name="cv">The CV.</param>
        /// <returns>The Data Object.</returns>
        public static CvDocument ToCvDocument(this Cv cv)
        {
            if (cv == null)
            {
                throw new ArgumentNullException(nameof(cv));
            }

            return new CvDocument(
               cv.Id,
               cv.UserId);
        }
    }
}
