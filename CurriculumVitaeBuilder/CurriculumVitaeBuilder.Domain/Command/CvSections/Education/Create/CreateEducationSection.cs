// <copyright file="CreateEducationSection.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Domain.Command.CvSections.Education.Create
{
    using System;
    using System.Collections.Generic;

    using Chest.Core.Command;

    using CurriculumVitaeBuilder.Domain.Data.CvSections.Education;

    /// <summary>
    /// Create Section Command.
    /// </summary>
    [CommandName("CVSECTION/CREATE_EDUCATION_SECTION")]
    public class CreateEducationSection : ICommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateEducationSection"/> class.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="cvId">The cv identifier.</param>
        /// <param name="educationEstablishments">The collection of education establishments.</param>
        public CreateEducationSection(
            Guid userId,
            Guid cvId,
            IList<EducationEstablishment> educationEstablishments)
        {
            this.UserId = userId;
            this.CvId = cvId;

            this.EducationEstablishments = educationEstablishments;
        }

        /// <summary>
        /// Gets the user identifier.
        /// </summary>
        public Guid UserId { get; }

        /// <summary>
        /// Gets the CV identifier.
        /// </summary>
        public Guid CvId { get; }

        /// <summary>
        /// Gets the collection of education establishments.
        /// </summary>
        public IList<EducationEstablishment> EducationEstablishments { get; }
    }
}
