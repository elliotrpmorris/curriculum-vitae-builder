// <copyright file="EducationSectionTests.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Domain.Tests.Data.CvSections
{
    using System;
    using System.Collections.Generic;

    using CurriculumVitaeBuilder.Domain.Data.CvSections.Education;

    using Xunit;

    public class EducationSectionTests
    {
        [Fact]
        public void Constructor_Id_ThrowsArgumentException()
        {
            var id = Guid.Empty;
            var educationEstablishments = new List<EducationEstablishment>();

            Assert.Throws<ArgumentException>(() =>
                new EducationSection(
                    id,
                    Guid.NewGuid(),
                    educationEstablishments));
        }

        [Fact]
        public void Constructor_CvId_ThrowsArgumentException()
        {
            var cvId = Guid.Empty;
            var educationEstablishments = new List<EducationEstablishment>();

            Assert.Throws<ArgumentException>(() =>
                new EducationSection(
                    Guid.NewGuid(),
                    cvId,
                    educationEstablishments));
        }

        [Fact]
        public void Constructor_InvalidEducationEstablishment_ThrowsArgumentNullException()
        {
            List<EducationEstablishment>? educationEstablishments = null;

            Assert.Throws<ArgumentNullException>(() =>
                new EducationSection(
                    Guid.NewGuid(),
                    Guid.NewGuid(),
                    educationEstablishments!));
        }

        [Fact]
        public void Constructor_ValidParams_AreAssignedCorrectly()
        {
            // Arrange / Act
            var id = Guid.NewGuid();
            var cvId = Guid.NewGuid();
            var educationEstablishments = new List<EducationEstablishment>();

            var ret = new EducationSection(
                    id,
                    cvId,
                    educationEstablishments);

            // Assert
            Assert.Equal(id, ret.Id);
            Assert.Equal(cvId, ret.CvId);
            Assert.Equal(educationEstablishments, ret.EducationEstablishments);
        }
    }
}
