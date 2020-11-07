// <copyright file="DeleteEducationEstablishment.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Domain.Command.CvSections.Education.DeleteEducationEstablishment
{
    using System;

    using Chest.Core.Command;

    /// <summary>
    /// Delete Contact Detail command.
    /// </summary>
    [CommandName("CVSECTION/DELETE_EDUCATION_ESTABLISHMENT")]
    public class DeleteEducationEstablishment : ICommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteEducationEstablishment"/> class.
        /// </summary>
        /// <param name="cvId">The CV identifier.</param>
        /// <param name="educationEstablishment">The contact detail name.</param>
        public DeleteEducationEstablishment(
            Guid cvId,
            string educationEstablishment)
        {
            this.CvId = cvId;
            this.EducationEstablishment = educationEstablishment;
        }

        /// <summary>
        /// Gets the CV identifier.
        /// </summary>
        public Guid CvId { get; }

        /// <summary>
        /// Gets the contact detail name.
        /// </summary>
        public string EducationEstablishment { get; }
    }
}
