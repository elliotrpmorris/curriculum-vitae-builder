// <copyright file="EducationEstablishmentTests.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Domain.Tests.Data.CvSections
{
    using System;

    using CurriculumVitaeBuilder.Domain.Data.CvSections.Education;

    using Xunit;

    public class EducationEstablishmentTests
    {
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Constructor_InvalidName_ThrowsArgumentException(string name)
        {
            Assert.Throws<ArgumentException>(() =>
                new EducationEstablishment(
                    name,
                    DateTime.Now,
                    DateTime.Now));
        }

        [Fact]
        public void Constructor_InvallidStart_ThrowsArgumentException()
        {
            DateTime time = default;

            Assert.Throws<ArgumentException>(() =>
                new EducationEstablishment(
                    "Test",
                    time,
                    DateTime.Now));
        }

        [Fact]
        public void Constructor_InvallidEnd_ThrowsArgumentException()
        {
            DateTime time = default;

            Assert.Throws<ArgumentException>(() =>
                new EducationEstablishment(
                    "Test",
                    DateTime.Now,
                    time));
        }

        [Fact]
        public void Constructor_StartGreaterThanEnd_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() =>
                new EducationEstablishment(
                    "Test",
                    DateTime.Now.AddDays(1),
                    DateTime.Now));
        }

        [Fact]
        public void Constructor_ValidParams_AreAssignedCorrectly()
        {
            // Arrange / Act
            var start = DateTime.Now;
            var end = DateTime.Now;

            var ret = new EducationEstablishment(
                    "Test",
                    start,
                    end);

            // Assert
            Assert.Equal("Test", ret.Name);
            Assert.Equal(start, ret.Start);
            Assert.Equal(end, ret.End);
        }
    }
}
