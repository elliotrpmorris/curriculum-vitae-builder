// <copyright file="UserTests.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Domain.Tests.Data
{
    using System;

    using CurriculumVitaeBuilder.Domain.Data.User;

    using Xunit;

    public class UserTests
    {
        [Fact]
        public void Constructor_Id_ThrowsArgumentException()
        {
            var id = Guid.Empty;

            Assert.Throws<ArgumentException>(() =>
                new User(
                    id,
                    "test",
                    DateTime.Now));
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Constructor_InvalidUserName_ThrowsArgumentException(string userName)
        {
            Assert.Throws<ArgumentException>(() =>
                new User(
                    Guid.NewGuid(),
                    userName,
                    DateTime.Now));
        }

        [Fact]
        public void Constructor_InvalidCreatedAt_ThrowsArgumentException()
        {
            DateTime createdAt = default;

            Assert.Throws<ArgumentException>(() =>
                new User(
                    Guid.NewGuid(),
                    "test",
                    createdAt));
        }

        [Fact]
        public void Constructor_ValidParams_AreAssignedCorrectly()
        {
            // Arrange / Act
            var id = Guid.NewGuid();
            var createdAt = DateTime.Now;

            var ret = new User(
                    id,
                    "test",
                    createdAt);

            // Assert
            Assert.Equal(id, ret.Id);
            Assert.Equal("test", ret.UserName);
            Assert.Equal(createdAt, ret.CreatedAt);
        }
    }
}
