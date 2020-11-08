// <copyright file="UpdateContactSectionHandleTests.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Domain.Tests.Command.CvSections.Contact.Update
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Chest.Core.Command;
    using Chest.Core.Exceptions;

    using CurriculumVitaeBuilder.Domain.Command.CvSections.Contact.Update;
    using CurriculumVitaeBuilder.Domain.Data;
    using CurriculumVitaeBuilder.Domain.Data.CvSections;
    using CurriculumVitaeBuilder.Domain.Data.CvSections.Contact;

    using NSubstitute;
    using NSubstitute.ReturnsExtensions;

    using Xunit;

    public class UpdateContactSectionHandleTests
    {
        [Fact]
        public async void Handle_CvDoesntExist_Throws()
        {
            // Arrange
            var cvSectionReader = Substitute.For<ICvSectionReader<ContactSection>>();
            var cvSectionWriter = Substitute.For<ICvSectionWriter<ContactSection>>();
            var cvReader = Substitute.For<ICvReader>();

            var handler = new UpdateContactSectionHandler(
                cvSectionReader,
                cvSectionWriter,
                cvReader);

            var metadata = new CommandMetadata("UpdateContactSection", DateTime.UtcNow, Guid.NewGuid().ToString());

            var command = new UpdateContactSection(
                Guid.NewGuid(),
                Guid.NewGuid(),
                new Dictionary<string, string>()
                {
                    { "test", "value" },
                });

            cvReader
                .GetCvExistsAsync(command.CvId)
                .Returns(false);

            // Act
            // Assert
            var ex = await Assert.ThrowsAsync<InvalidCommandException>(
                async () => await handler.Handle(command, metadata));

            Assert.Contains(
                ex.ValidationErrors.SelectMany(v => v.Value),
                v => v == $"CV does not exist for user: {command.UserId}.");
        }

        [Fact]
        public async void Handle_CvSectionDoesntExist_Throws()
        {
            // Arrange
            var cvSectionReader = Substitute.For<ICvSectionReader<ContactSection>>();
            var cvSectionWriter = Substitute.For<ICvSectionWriter<ContactSection>>();
            var cvReader = Substitute.For<ICvReader>();

            var handler = new UpdateContactSectionHandler(
                cvSectionReader,
                cvSectionWriter,
                cvReader);

            var metadata = new CommandMetadata("UpdateContactSection", DateTime.UtcNow, Guid.NewGuid().ToString());

            var command = new UpdateContactSection(
                Guid.NewGuid(),
                Guid.NewGuid(),
                new Dictionary<string, string>()
                {
                    { "test", "value" },
                });

            cvReader
                .GetCvExistsAsync(command.CvId)
                .Returns(true);

            cvSectionReader
                .GetSectionByCvAsync(command.CvId)
                .ReturnsNull();

            // Act
            // Assert
            var ex = await Assert.ThrowsAsync<InvalidCommandException>(
                async () => await handler.Handle(command, metadata));

            Assert.Contains(
                ex.ValidationErrors.SelectMany(v => v.Value),
                v => v == "CV section doesn't exists.");
        }

        [Fact]
        public async void Handle_ValidCommand_UpdatesContactSection()
        {
            // Arrange
            var cvSectionReader = Substitute.For<ICvSectionReader<ContactSection>>();
            var cvSectionWriter = Substitute.For<ICvSectionWriter<ContactSection>>();
            var cvReader = Substitute.For<ICvReader>();

            var handler = new UpdateContactSectionHandler(
                cvSectionReader,
                cvSectionWriter,
                cvReader);

            var metadata = new CommandMetadata("UpdateContactSection", DateTime.UtcNow, Guid.NewGuid().ToString());

            var command = new UpdateContactSection(
                Guid.NewGuid(),
                Guid.NewGuid(),
                new Dictionary<string, string>()
                {
                    { "test", "value" },
                });

            cvReader
                .GetCvExistsAsync(command.CvId)
                .Returns(true);

            cvSectionReader
                .GetSectionByCvAsync(command.CvId)
                .Returns(new ContactSection(
                    Guid.NewGuid(),
                    command.CvId,
                    new Dictionary<string, string>()
                    {
                        { "test", "value" },
                    }));

            // Act
            // Assert
            await handler.Handle(command, metadata);

            var expectedArgs =
                Arg.Is<ContactSection>(u =>
                u.CvId == command.CvId &&
                u.ContactDetails == command.ContactDetails);

            await cvSectionWriter.Received().UpdateAsync(expectedArgs);
        }
    }
}
