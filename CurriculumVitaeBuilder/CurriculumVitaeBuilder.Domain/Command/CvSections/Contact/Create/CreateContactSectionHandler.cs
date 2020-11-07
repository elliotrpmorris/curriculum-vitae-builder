// <copyright file="CreateContactSectionHandler.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Domain.Command.CvSections.Contact.Create
{
    using System;
    using System.Threading.Tasks;

    using Chest.Core.Command;
    using Chest.Core.Exceptions;

    using CurriculumVitaeBuilder.Domain.Data.CvSections;
    using CurriculumVitaeBuilder.Domain.Data.CvSections.Contact;

    /// <summary>
    /// Create Section Handler.
    /// </summary>
    public class CreateContactSectionHandler : ICommandHandler<CreateContactSection>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateContactSectionHandler"/> class.
        /// </summary>
        /// <param name="cvSectionReader">The CV section reader.</param>
        /// <param name="cvSectionWriter">The CV section writer.</param>
        public CreateContactSectionHandler(
            ICvSectionReader<ContactSection> cvSectionReader,
            ICvSectionWriter<ContactSection> cvSectionWriter)
        {
            this.CvSectionReader = cvSectionReader
                ?? throw new ArgumentNullException(nameof(cvSectionReader));

            this.CvSectionWriter = cvSectionWriter
                ?? throw new ArgumentNullException(nameof(cvSectionWriter));
        }

        private ICvSectionReader<ContactSection> CvSectionReader { get; }

        private ICvSectionWriter<ContactSection> CvSectionWriter { get; }

        /// <inheritdoc/>
        public async Task Handle(CreateContactSection command, CommandMetadata metadata)
        {
            if (command.UserId == default)
            {
                throw new InvalidCommandException(
                    metadata.CommandName,
                    typeof(CreateContactSection).Name,
                    $"User Id must be set on the command.");
            }

            if (command.CvId == default)
            {
                throw new InvalidCommandException(
                   metadata.CommandName,
                   typeof(CreateContactSection).Name,
                   $"CV Id must be set on the command.");
            }

            var exists = await
                this.CvSectionReader.GetSectionExistsAsync(command.CvId);

            if (exists)
            {
                throw new InvalidCommandException(
                  metadata.CommandName,
                  typeof(CreateContactSection).Name,
                  $"CV section already exists.");
            }

            await this.CvSectionWriter.AddAsync(
                new ContactSection(
                    Guid.NewGuid(),
                    command.CvId,
                    command.ContactDetails));
        }
    }
}
