// <copyright file="JobHistorySectionTests.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Domain.Tests.Data.CvSections
{
    using System;
    using System.Collections.Generic;

    using CurriculumVitaeBuilder.Domain.Data.CvSections;
    using CurriculumVitaeBuilder.Domain.Data.CvSections.JobHistory;

    using Xunit;

    public class JobHistorySectionTests
    {
        [Fact]
        public void Constructor_Id_ThrowsArgumentException()
        {
            var id = Guid.Empty;
            var jobs = new List<Job>();

            Assert.Throws<ArgumentException>(() =>
                new JobHistorySection(
                    id,
                    Guid.NewGuid(),
                    jobs));
        }

        [Fact]
        public void Constructor_CvId_ThrowsArgumentException()
        {
            var cvId = Guid.Empty;
            var jobs = new List<Job>();

            Assert.Throws<ArgumentException>(() =>
                new JobHistorySection(
                    Guid.NewGuid(),
                    cvId,
                    jobs));
        }

        [Fact]
        public void Constructor_IvalidJobs_ThrowsArgumentNullException()
        {
            List<Job>? jobs = null;

            Assert.Throws<ArgumentNullException>(() =>
                new JobHistorySection(
                    Guid.NewGuid(),
                    Guid.NewGuid(),
                    jobs!));
        }

        [Fact]
        public void Constructor_ValidParams_AreAssignedCorrectly()
        {
            // Arrange / Act
            var id = Guid.NewGuid();
            var cvId = Guid.NewGuid();
            var job = new List<Job>();

            var ret = new JobHistorySection(
                    id,
                    cvId,
                    job);

            // Assert
            Assert.Equal(id, ret.Id);
            Assert.Equal(cvId, ret.CvId);
            Assert.Equal(job, ret.Jobs);
        }
    }
}
