// <copyright file="UpdateContactSectionValidator.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Domain.Command.CvSections.Contact.Update
{
    using Chest.Core.Command;

    using FluentValidation;

    /// <summary>
    /// Create Section Validator.
    /// </summary>
    public class UpdateContactSectionValidator : CommandValidator<UpdateContactSection>
    {
        /// <inheritdoc/>
        public override void ConfigureRules()
        {
            this.RuleFor(x => x.UserId)
                .NotEmpty()
                .WithMessage("A valid user identifier must be provided");

            this.RuleFor(x => x.CvId)
                .NotEmpty()
                .WithMessage("A valid cv identifier must be provided");

            this.RuleFor(x => x.ContactDetails)
                .NotEmpty()
                .NotNull()
                .WithMessage("A contact detail must be provided");
        }
    }
}
