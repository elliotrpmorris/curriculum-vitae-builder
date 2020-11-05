// <copyright file="Cv.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Domain.Data.Cv
{
    using System;

    using CurriculumVitaeBuilder.Domain.Data.Cv.CvSections;
    using CurriculumVitaeBuilder.Domain.Data.Cv.CvSections.Bio;
    using CurriculumVitaeBuilder.Domain.Data.Cv.CvSections.Contact;
    using CurriculumVitaeBuilder.Domain.Data.Cv.CvSections.Education;
    using CurriculumVitaeBuilder.Domain.Data.Cv.CvSections.SkillsProfile;

    /// <summary>
    /// CV.
    /// </summary>
    public class Cv
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Cv"/> class.
        /// </summary>
        /// <param name="id">The CV identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="bio">Th bio section.</param>
        /// <param name="contact">The contact section.</param>
        /// <param name="education">The education section.</param>
        /// <param name="jobHistory">The job history section.</param>
        /// <param name="skillsProfile">The skills profile section.</param>
        public Cv(
            Guid id,
            Guid userId,
            BioSection? bio,
            ContactSection? contact,
            EducationSection? education,
            JobHistorySection? jobHistory,
            SkillsProfileSection? skillsProfile)
        {
            this.Id = id;
            this.UserId = userId;
            this.Bio = bio;
            this.Contact = contact;
            this.Education = education;
            this.JobHistory = jobHistory;
            this.SkillsProfile = skillsProfile;
        }

        /// <summary>
        /// Gets the CV identifier.
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Gets the user identifier.
        /// </summary>
        public Guid UserId { get; }

        /// <summary>
        /// Gets the bio section.
        /// </summary>
        public BioSection? Bio { get; }

        /// <summary>
        /// Gets the contact section.
        /// </summary>
        public ContactSection? Contact { get; }

        /// <summary>
        /// Gets the education section.
        /// </summary>
        public EducationSection? Education { get; }

        /// <summary>
        /// Gets the job history section.
        /// </summary>
        public JobHistorySection? JobHistory { get; }

        /// <summary>
        /// Gets the skills profile section.
        /// </summary>
        public SkillsProfileSection? SkillsProfile { get; }
    }
}
