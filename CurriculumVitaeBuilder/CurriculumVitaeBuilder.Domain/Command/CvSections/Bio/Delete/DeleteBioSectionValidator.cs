﻿// <copyright file="DeleteBioSectionValidator.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Domain.Command.CvSections.Bio.Delete
{
    using Chest.Core.Command;

    using FluentValidation;

    /// <summary>
    /// Delete Section Validator.
    /// </summary>
    public class DeleteBioSectionValidator : CommandValidator<DeleteBioSection>
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
        }
    }
}
