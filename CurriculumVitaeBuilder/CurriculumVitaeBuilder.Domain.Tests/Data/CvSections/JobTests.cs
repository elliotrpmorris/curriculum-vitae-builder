// <copyright file="JobTests.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Domain.Tests.Data.CvSections
{
    using System;

    using CurriculumVitaeBuilder.Domain.Data.CvSections.JobHistory;

    using Xunit;

    public class JobTests
    {
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Constructor_InvalidEmployer_ThrowsArgumentException(string employer)
        {
            Assert.Throws<ArgumentException>(() =>
                new Job(
                    employer,
                    DateTime.Now,
                    DateTime.Now,
                    "Test title",
                    "Test desc"));
        }

        [Fact]
        public void Constructor_InvalidStart_ThrowsArgumentException()
        {
            DateTime start = default;

            Assert.Throws<ArgumentException>(() =>
                new Job(
                    "Test",
                    start,
                    DateTime.Now,
                    "Test title",
                    "Test desc"));
        }

        [Fact]
        public void Constructor_InvalidEnd_ThrowsArgumentException()
        {
            DateTime end = default;

            Assert.Throws<ArgumentException>(() =>
                new Job(
                    "Test",
                    DateTime.Now,
                    end,
                    "Test title",
                    "Test desc"));
        }

        [Fact]
        public void Constructor_StartGreaterThanEnd_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() =>
                new Job(
                    "Test",
                    DateTime.Now.AddDays(1),
                    DateTime.Now,
                    "Test title",
                    "Test desc"));
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Constructor_InvalidJobTitle_ThrowsArgumentException(string jobTitle)
        {
            Assert.Throws<ArgumentException>(() =>
                new Job(
                    "Test",
                    DateTime.Now,
                    DateTime.Now,
                    jobTitle,
                    "Test desc"));
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Constructor_Invalid_ThrowsArgumentException(string description)
        {
            Assert.Throws<ArgumentException>(() =>
                new Job(
                    "Test",
                    DateTime.Now,
                    DateTime.Now,
                    "Test title",
                    description));
        }

        [Fact]
        public void Constructor_ValidParams_AreAssignedCorrectly()
        {
            // Arrange / Act
            var start = DateTime.Now;
            var end = DateTime.Now;

            var ret = new Job(
                    "Test",
                    start,
                    end,
                    "Test title",
                    "Test desc");

            // Assert
            Assert.Equal("Test", ret.Employer);
            Assert.Equal(start, ret.Start);
            Assert.Equal(end, ret.End);
            Assert.Equal("Test title", ret.JobTitle);
            Assert.Equal("Test desc", ret.Description);
        }
    }
}
