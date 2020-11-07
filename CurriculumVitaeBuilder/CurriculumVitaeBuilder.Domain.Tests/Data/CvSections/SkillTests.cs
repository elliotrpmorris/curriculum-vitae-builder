// <copyright file="SkillTests.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Domain.Tests.Data.CvSections
{
    using System;

    using CurriculumVitaeBuilder.Domain.Data.CvSections.SkillsProfile;

    using Xunit;

    public class SkillTests
    {
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Constructor_InvalidName_ThrowsArgumentException(string name)
        {
            Assert.Throws<ArgumentException>(() =>
                new Skill(
                    name,
                    "Test desc",
                    null));
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Constructor_InvalidDescription_ThrowsArgumentException(string description)
        {
            Assert.Throws<ArgumentException>(() =>
                new Skill(
                    "Test",
                    description,
                    null));
        }

        [Fact]
        public void Constructor_ValidParams_AreAssignedCorrectly()
        {
            // Arrange / Act
            var ret = new Skill(
                    "Test",
                    "Test desc",
                    null);

            // Assert
            Assert.Equal("Test", ret.Name);
            Assert.Equal("Test desc", ret.Description);
            Assert.Null(ret.AchievedAt);
        }
    }
}
