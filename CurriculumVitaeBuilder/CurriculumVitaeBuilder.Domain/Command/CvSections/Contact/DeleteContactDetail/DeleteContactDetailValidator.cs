// <copyright file="DeleteContactDetailValidator.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Domain.Command.CvSections.Contact.DeleteContactDetail
{
    using Chest.Core.Command;

    using FluentValidation;

    /// <summary>
    /// Delete Contact Detail Validator.
    /// </summary>
    public class DeleteContactDetailValidator : CommandValidator<DeleteContactDetail>
    {
        /// <inheritdoc/>
        public override void ConfigureRules()
        {
            this.RuleFor(x => x.CvId)
                .NotEmpty()
                .WithMessage("A valid cv identifier must be provided");

            this.RuleFor(x => x.ContactDetail)
                .NotEmpty()
                .WithMessage("A valid contact detail name must be provided");
        }
    }
}
