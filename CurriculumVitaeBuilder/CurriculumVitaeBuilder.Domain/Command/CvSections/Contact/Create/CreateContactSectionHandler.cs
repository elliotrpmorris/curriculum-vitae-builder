// <copyright file="CreateContactSectionHandler.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Domain.Command.CvSections.Contact.Create
{
    using System;
    using System.Threading.Tasks;

    using Chest.Core.Command;
    using Chest.Core.Exceptions;

    using CurriculumVitaeBuilder.Domain.Data;
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
        /// <param name="cvReader">The CV reader.</param>
        public CreateContactSectionHandler(
            ICvSectionReader<ContactSection> cvSectionReader,
            ICvSectionWriter<ContactSection> cvSectionWriter,
            ICvReader cvReader)
        {
            this.CvSectionReader = cvSectionReader
                ?? throw new ArgumentNullException(nameof(cvSectionReader));

            this.CvSectionWriter = cvSectionWriter
                ?? throw new ArgumentNullException(nameof(cvSectionWriter));

            this.CvReader = cvReader
                ?? throw new ArgumentNullException(nameof(cvReader));
        }

        private ICvSectionReader<ContactSection> CvSectionReader { get; }

        private ICvSectionWriter<ContactSection> CvSectionWriter { get; }

        private ICvReader CvReader { get; }

        /// <inheritdoc/>
        public async Task Handle(CreateContactSection command, CommandMetadata metadata)
        {
            var cvExists = await
                this.CvReader.GetCvExistsAsync(command.CvId);

            if (!cvExists)
            {
                throw new InvalidCommandException(
                  metadata.CommandName,
                  typeof(CreateContactSection).Name,
                  $"CV does not exist for user: {command.UserId}.");
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
