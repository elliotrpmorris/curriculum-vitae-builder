// <copyright file="ContactSectionTests.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Domain.Tests.Data.CvSections
{
    using System;
    using System.Collections.Generic;

    using CurriculumVitaeBuilder.Domain.Data.CvSections.Contact;

    using Xunit;

    public class ContactSectionTests
    {
        [Fact]
        public void Constructor_Id_ThrowsArgumentException()
        {
            var id = Guid.Empty;
            var details = new Dictionary<string, string>();

            Assert.Throws<ArgumentException>(() =>
                new ContactSection(
                    id,
                    Guid.NewGuid(),
                    details));
        }

        [Fact]
        public void Constructor_CvId_ThrowsArgumentException()
        {
            var cvId = Guid.Empty;
            var details = new Dictionary<string, string>();

            Assert.Throws<ArgumentException>(() =>
                new ContactSection(
                    Guid.NewGuid(),
                    cvId,
                    details));
        }

        [Fact]
        public void Constructor_ConactDetails_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new ContactSection(
                    Guid.NewGuid(),
                    Guid.NewGuid(),
                    null));
        }

        [Fact]
        public void Constructor_ValidParams_AreAssignedCorrectly()
        {
            // Arrange / Act
            var id = Guid.NewGuid();
            var cvId = Guid.NewGuid();
            var details = new Dictionary<string, string>();

            var ret = new ContactSection(
                    id,
                    cvId,
                    details);

            // Assert
            Assert.Equal(id, ret.Id);
            Assert.Equal(cvId, ret.CvId);
            Assert.Equal(details, ret.ContactDetails);
        }
    }
}
