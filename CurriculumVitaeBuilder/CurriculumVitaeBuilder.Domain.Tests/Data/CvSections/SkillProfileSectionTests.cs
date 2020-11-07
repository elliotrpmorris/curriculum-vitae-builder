// <copyright file="SkillProfileSectionTests.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Domain.Tests.Data.CvSections
{
    using System;
    using System.Collections.Generic;

    using CurriculumVitaeBuilder.Domain.Data.CvSections.SkillsProfile;

    using Xunit;

    public class SkillProfileSectionTests
    {
        [Fact]
        public void Constructor_Id_ThrowsArgumentException()
        {
            var id = Guid.Empty;
            var skills = new List<Skill>();

            Assert.Throws<ArgumentException>(() =>
                new SkillsProfileSection(
                    id,
                    Guid.NewGuid(),
                    skills));
        }

        [Fact]
        public void Constructor_CvId_ThrowsArgumentException()
        {
            var cvId = Guid.Empty;
            var skills = new List<Skill>();

            Assert.Throws<ArgumentException>(() =>
                new SkillsProfileSection(
                    Guid.NewGuid(),
                    cvId,
                    skills));
        }

        [Fact]
        public void Constructor_IvalidJobs_ThrowsArgumentNullException()
        {
            List<Skill>? skills = null;

            Assert.Throws<ArgumentNullException>(() =>
                new SkillsProfileSection(
                    Guid.NewGuid(),
                    Guid.NewGuid(),
                    skills!));
        }

        [Fact]
        public void Constructor_ValidParams_AreAssignedCorrectly()
        {
            // Arrange / Act
            var id = Guid.NewGuid();
            var cvId = Guid.NewGuid();
            var skills = new List<Skill>();

            var ret = new SkillsProfileSection(
                    id,
                    cvId,
                    skills);

            // Assert
            Assert.Equal(id, ret.Id);
            Assert.Equal(cvId, ret.CvId);
            Assert.Equal(skills, ret.Skills);
        }
    }
}
