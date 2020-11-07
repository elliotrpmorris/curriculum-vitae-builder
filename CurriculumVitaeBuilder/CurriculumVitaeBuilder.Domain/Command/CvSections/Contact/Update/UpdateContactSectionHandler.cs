// <copyright file="UpdateContactSectionHandler.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Domain.Command.CvSections.Contact.Update
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
    public class UpdateContactSectionHandler : ICommandHandler<UpdateContactSection>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateContactSectionHandler"/> class.
        /// </summary>
        /// <param name="cvSectionReader">The CV section reader.</param>
        /// <param name="cvSectionWriter">The CV section writer.</param>
        /// <param name="cvReader">The CV reader.</param>
        public UpdateContactSectionHandler(
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
        public async Task Handle(UpdateContactSection command, CommandMetadata metadata)
        {
            var cvExists = await
               this.CvReader.GetCvExistsAsync(command.CvId);

            if (!cvExists)
            {
                throw new InvalidCommandException(
                  metadata.CommandName,
                  typeof(UpdateContactSection).Name,
                  $"CV does not exist for user: {command.UserId}.");
            }

            var section = await
                this.CvSectionReader.GetSectionByCvAsync(command.CvId);

            if (section == null)
            {
                throw new InvalidCommandException(
                  metadata.CommandName,
                  typeof(UpdateContactSection).Name,
                  $"CV section doesn't exists.");
            }

            await this.CvSectionWriter.UpdateAsync(
                new ContactSection(
                    section.Id,
                    section.CvId,
                    command.ContactDetails));
        }
    }
}
