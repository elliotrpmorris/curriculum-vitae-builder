// <copyright file="CreateEducationSectionValidator.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Domain.Command.CvSections.Education.Create
{
    using Chest.Core.Command;

    using FluentValidation;

    /// <summary>
    /// Create Section Validator.
    /// </summary>
    public class CreateEducationSectionValidator : CommandValidator<CreateEducationSection>
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
                .WithMessage("Education establishments must be provided");
        }
    }
}
