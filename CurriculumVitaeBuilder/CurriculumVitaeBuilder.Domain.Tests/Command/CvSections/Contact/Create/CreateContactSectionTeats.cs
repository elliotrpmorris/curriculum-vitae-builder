// <copyright file="CreateContactSectionTeats.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Domain.Tests.Command.CvSections.Contact.Create
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Chest.Core.Command;
    using Chest.Core.Exceptions;

    using CurriculumVitaeBuilder.Domain.Command.CvSections.Contact.Create;
    using CurriculumVitaeBuilder.Domain.Data;
    using CurriculumVitaeBuilder.Domain.Data.CvSections;
    using CurriculumVitaeBuilder.Domain.Data.CvSections.Contact;

    using NSubstitute;

    using Xunit;

    public class CreateContactSectionTeats
    {
        [Fact]
        public async void Handle_CvDoesntExist_Throws()
        {
            // Arrange
            var cvSectionReader = Substitute.For<ICvSectionReader<ContactSection>>();
            var cvSectionWriter = Substitute.For<ICvSectionWriter<ContactSection>>();
            var cvReader = Substitute.For<ICvReader>();

            var handler = new CreateContactSectionHandler(
                cvSectionReader,
                cvSectionWriter,
                cvReader);

            var metadata = new CommandMetadata("CreateContactSection", DateTime.UtcNow, Guid.NewGuid().ToString());

            var contactDetails = new Dictionary<string, string>();

            var command = new CreateContactSection(
                Guid.NewGuid(),
                Guid.NewGuid(),
                contactDetails);

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

            var handler = new CreateContactSectionHandler(
                cvSectionReader,
                cvSectionWriter,
                cvReader);

            var metadata = new CommandMetadata("CreateContactSection", DateTime.UtcNow, Guid.NewGuid().ToString());

            var contactDetails = new Dictionary<string, string>();

            var command = new CreateContactSection(
                Guid.NewGuid(),
                Guid.NewGuid(),
                contactDetails);

            cvReader
                .GetCvExistsAsync(command.CvId)
                .Returns(true);

            cvSectionReader
               .GetSectionExistsAsync(command.CvId)
               .Returns(true);

            // Act
            // Assert
            var ex = await Assert.ThrowsAsync<InvalidCommandException>(
                async () => await handler.Handle(command, metadata));

            Assert.Contains(
                ex.ValidationErrors.SelectMany(v => v.Value),
                v => v == "CV section already exists.");
        }

        [Fact]
        public async void Handle_ValidCommand_CreatesContactSection()
        {
            // Arrange
            var cvSectionReader = Substitute.For<ICvSectionReader<ContactSection>>();
            var cvSectionWriter = Substitute.For<ICvSectionWriter<ContactSection>>();
            var cvReader = Substitute.For<ICvReader>();

            var handler = new CreateContactSectionHandler(
                cvSectionReader,
                cvSectionWriter,
                cvReader);

            var metadata = new CommandMetadata("CreateContactSection", DateTime.UtcNow, Guid.NewGuid().ToString());

            var contactDetails = new Dictionary<string, string>();

            var command = new CreateContactSection(
                Guid.NewGuid(),
                Guid.NewGuid(),
                contactDetails);

            cvReader
                .GetCvExistsAsync(command.CvId)
                .Returns(true);

            cvSectionReader
              .GetSectionExistsAsync(command.CvId)
              .Returns(false);

            // Act
            // Assert
            await handler.Handle(command, metadata);

            var expectedArgs =
                Arg.Is<ContactSection>(u =>
                u.CvId == command.CvId &&
                u.ContactDetails == command.ContactDetails);

            await cvSectionWriter.Received().AddAsync(expectedArgs);
        }
    }
}
