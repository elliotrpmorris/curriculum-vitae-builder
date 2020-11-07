// <copyright file="DeleteContactSectionHandler.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Domain.Command.CvSections.Contact.Delete
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
    public class DeleteContactSectionHandler : ICommandHandler<DeleteContactSection>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteContactSectionHandler"/> class.
        /// </summary>
        /// <param name="cvSectionReader">The CV section reader.</param>
        /// <param name="cvSectionWriter">The CV section writer.</param>
        public DeleteContactSectionHandler(
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
        public async Task Handle(DeleteContactSection command, CommandMetadata metadata)
        {
            if (command.UserId == default)
            {
                throw new InvalidCommandException(
                    metadata.CommandName,
                    typeof(DeleteContactSection).Name,
                    $"User Id must be set on the command.");
            }

            if (command.CvId == default)
            {
                throw new InvalidCommandException(
                   metadata.CommandName,
                   typeof(DeleteContactSection).Name,
                   $"CV Id must be set on the command.");
            }

            var section = await
               this.CvSectionReader.GetSectionByCvAsync(command.CvId);

            if (section == null)
            {
                throw new InvalidCommandException(
                  metadata.CommandName,
                  typeof(DeleteContactSection).Name,
                  $"CV section doesn't exists.");
            }

            await this.CvSectionWriter.DeleteAsync(section);
        }
    }
}
