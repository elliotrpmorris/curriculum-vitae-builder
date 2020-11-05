// <copyright file="Cv.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Domain.Data.Cv
{
    using System;
    using System.Collections.Generic;

    using CurriculumVitaeBuilder.Domain.Data.Cv.CvSections;

    /// <summary>
    /// CV.
    /// </summary>
    public class Cv
    {
        public Cv(
            Guid id,
            Guid userId,
            IReadOnlyCollection<CvSection> sections)
        {
            this.Id = id;
            this.UserId = userId;
            this.Sections = sections
                ?? throw new ArgumentNullException(nameof(sections));
        }

        public Guid Id { get; }

        public Guid UserId { get; }

        public IReadOnlyCollection<CvSection> Sections { get; }
    }
}
