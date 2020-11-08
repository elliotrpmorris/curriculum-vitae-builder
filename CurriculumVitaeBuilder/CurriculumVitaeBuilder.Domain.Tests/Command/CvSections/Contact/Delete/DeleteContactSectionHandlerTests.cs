// <copyright file="DeleteContactSectionHandlerTests.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Domain.Tests.Command.CvSections.Contact.Delete
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Chest.Core.Command;
    using Chest.Core.Exceptions;

    using CurriculumVitaeBuilder.Domain.Command.CvSections.Contact.Create;
    using CurriculumVitaeBuilder.Domain.Command.CvSections.Contact.Delete;
    using CurriculumVitaeBuilder.Domain.Data;
    using CurriculumVitaeBuilder.Domain.Data.CvSections;
    using CurriculumVitaeBuilder.Domain.Data.CvSections.Contact;

    using NSubstitute;
    using NSubstitute.ReturnsExtensions;
    using Xunit;

    public class DeleteContactSectionHandlerTests
    {
        [Fact]
        public async void Handle_CvDoesntExist_Throws()
        {
            // Arrange
            var cvSectionReader = Substitute.For<ICvSectionReader<ContactSection>>();
            var cvSectionWriter = Substitute.For<ICvSectionWriter<ContactSection>>();
            var cvReader = Substitute.For<ICvReader>();

            var handler = new DeleteContactSectionHandler(
                cvSectionReader,
                cvSectionWriter,
                cvReader);

            var metadata = new CommandMetadata("DeleteContactSection", DateTime.UtcNow, Guid.NewGuid().ToString());  

            var command = new DeleteContactSection(
                Guid.NewGuid(),
                Guid.NewGuid());

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

            var handler = new DeleteContactSectionHandler(
                cvSectionReader,
                cvSectionWriter,
                cvReader);

            var metadata = new CommandMetadata("DeleteContactSection", DateTime.UtcNow, Guid.NewGuid().ToString());

            var command = new DeleteContactSection(
                Guid.NewGuid(),
                Guid.NewGuid());

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
                v => v == "CV section doesn't exist.");
        }

        [Fact]
        public async void Handle_ValidCommand_DeletesContactSection()
        {
            // Arrange
            var cvSectionReader = Substitute.For<ICvSectionReader<ContactSection>>();
            var cvSectionWriter = Substitute.For<ICvSectionWriter<ContactSection>>();
            var cvReader = Substitute.For<ICvReader>();

            var handler = new DeleteContactSectionHandler(
                cvSectionReader,
                cvSectionWriter,
                cvReader);

            var metadata = new CommandMetadata("DeleteContactSection", DateTime.UtcNow, Guid.NewGuid().ToString());

            var command = new DeleteContactSection(
                Guid.NewGuid(),
                Guid.NewGuid());

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
                        { "test", "test" },
                    }));

            // Act
            // Assert
            await handler.Handle(command, metadata);

            var expectedArgs =
                Arg.Is<ContactSection>(u =>
                u.CvId == command.CvId);

            await cvSectionWriter.Received().DeleteAsync(expectedArgs);
        }
    }
}
