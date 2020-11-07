// <copyright file="UpdateEducationSectionValidator.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Domain.Command.CvSections.Education.Update
{
    using Chest.Core.Command;

    using FluentValidation;

    /// <summary>
    /// Create Section Validator.
    /// </summary>
    public class UpdateEducationSectionValidator : CommandValidator<UpdateEducationSection>
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

            this.RuleFor(x => x.EducationEstablishments)
                .NotEmpty()
                .NotNull()
                .WithMessage("A education establishment must be provided");
        }
    }
}
