// <copyright file="BioSectionTests.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Domain.Tests.Data.CvSections
{
    using System;

    using CurriculumVitaeBuilder.Domain.Data.CvSections.Bio;

    using Xunit;

    public class BioSectionTests
    {
        [Fact]
        public void Constructor_Id_ThrowsArgumentException()
        {
            var id = Guid.Empty;

            Assert.Throws<ArgumentException>(() =>
                new BioSection(
                    id,
                    Guid.NewGuid(),
                    "Test Name",
                    "Test City"));
        }

        [Fact]
        public void Constructor_CvId_ThrowsArgumentException()
        {
            var cvId = Guid.Empty;

            Assert.Throws<ArgumentException>(() =>
                new BioSection(
                    Guid.NewGuid(),
                    cvId,
                    "Test Name",
                    "Test City"));
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Constructor_InvalidFullName_ThrowsArgumentException(string fullName)
        {
            Assert.Throws<ArgumentException>(() =>
                new BioSection(
                    Guid.NewGuid(),
                    Guid.NewGuid(),
                    fullName,
                    "Test City"));
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Constructor_InvalidCity_ThrowsArgumentException(string city)
        {
            Assert.Throws<ArgumentException>(() =>
                new BioSection(
                    Guid.NewGuid(),
                    Guid.NewGuid(),
                    "Test Name",
                    city));
        }

        [Fact]
        public void Constructor_ValidParams_AreAssignedCorrectly()
        {
            // Arrange / Act
            var id = Guid.NewGuid();
            var cvId = Guid.NewGuid();

            var ret = new BioSection(
                    id,
                    cvId,
                    "Test Name",
                    "Test City");

            // Assert
            Assert.Equal(id, ret.Id);
            Assert.Equal(cvId, ret.CvId);
            Assert.Equal("Test Name", ret.FullName);
            Assert.Equal("Test City", ret.City);
        }
    }
}
