// <copyright file="DeleteContactDetailHandler.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Domain.Command.CvSections.Contact.DeleteContactDetail
{
    using System;
    using System.Threading.Tasks;

    using Chest.Core.Command;
    using Chest.Core.Exceptions;

    using CurriculumVitaeBuilder.Domain.Data;
    using CurriculumVitaeBuilder.Domain.Data.CvSections;
    using CurriculumVitaeBuilder.Domain.Data.CvSections.Contact;

    /// <summary>
    /// Delete Contact Detail Handler.
    /// </summary>
    public class DeleteContactDetailHandler : ICommandHandler<DeleteContactDetail>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteContactDetailHandler"/> class.
        /// </summary>
        /// <param name="cvSectionContentWriter">The CV section content writer.</param>
        /// <param name="cvSectionReader">The CV section reader.</param>
        /// <param name="cvReader">The CV reader.</param>
        public DeleteContactDetailHandler(
            ICvSectionContentWriter<ContactSection> cvSectionContentWriter,
            ICvSectionReader<ContactSection> cvSectionReader,
            ICvReader cvReader)
        {
            this.CvSectionContentWriter = cvSectionContentWriter
                ?? throw new ArgumentNullException(nameof(cvSectionContentWriter));

            this.CvSectionReader = cvSectionReader
                ?? throw new ArgumentNullException(nameof(cvSectionReader));

            this.CvReader = cvReader
                ?? throw new ArgumentNullException(nameof(cvReader));
        }

        private ICvSectionContentWriter<ContactSection> CvSectionContentWriter { get; }

        private ICvSectionReader<ContactSection> CvSectionReader { get; }

        private ICvReader CvReader { get; }

        /// <inheritdoc/>
        public async Task Handle(DeleteContactDetail command, CommandMetadata metadata)
        {
            var cvExists = await
                this.CvReader.GetCvExistsAsync(command.CvId);

            if (!cvExists)
            {
                throw new InvalidCommandException(
                  metadata.CommandName,
                  typeof(DeleteContactDetail).Name,
                  $"CV does not exist with cvId: {command.CvId}.");
            }

            var section = await
              this.CvSectionReader.GetSectionByCvAsync(command.CvId);

            if (section == null)
            {
                throw new InvalidCommandException(
                  metadata.CommandName,
                  typeof(DeleteContactDetail).Name,
                  $"CV section doesn't exist.");
            }

            await this.CvSectionContentWriter.DeleteAsync(
                command.CvId,
                command.ContactDetail);
        }
    }
}
