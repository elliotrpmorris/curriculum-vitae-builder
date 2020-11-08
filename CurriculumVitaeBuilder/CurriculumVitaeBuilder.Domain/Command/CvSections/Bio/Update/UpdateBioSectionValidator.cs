﻿// <copyright file="UpdateBioSectionValidator.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Domain.Command.CvSections.Bio.Update
{
    using Chest.Core.Command;

    using FluentValidation;

    /// <summary>
    /// Create Section Validator.
    /// </summary>
    public class UpdateBioSectionValidator : CommandValidator<UpdateBioSection>
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

            this.RuleFor(x => x.City)
                .NotEmpty()
                .WithMessage("A city must be provided");

            this.RuleFor(x => x.FullName)
                .NotEmpty()
                .WithMessage("A full name must be provided");
        }
    }
}
