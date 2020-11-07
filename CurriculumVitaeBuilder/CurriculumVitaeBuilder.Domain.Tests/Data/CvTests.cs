// <copyright file="CvTests.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Domain.Tests.Data
{
    using System;

    using CurriculumVitaeBuilder.Domain.Data;

    using Xunit;

    public class CvTests
    {
        [Fact]
        public void Constructor_Id_ThrowsArgumentException()
        {
            var id = Guid.Empty;

            Assert.Throws<ArgumentException>(() =>
                new Cv(
                    id,
                    Guid.NewGuid()));
        }

        [Fact]
        public void Constructor_UserId_ThrowsArgumentException()
        {
            var userId = Guid.Empty;

            Assert.Throws<ArgumentException>(() =>
                new Cv(
                    Guid.NewGuid(),
                    userId));
        }

        [Fact]
        public void Constructor_ValidParams_AreAssignedCorrectly()
        {
            // Arrange / Act
            var id = Guid.NewGuid();
            var userId = Guid.NewGuid();

            var ret = new Cv(
                id,
                userId);

            // Assert
            Assert.Equal(id, ret.Id);
            Assert.Equal(userId, ret.UserId);
        }
    }
}
