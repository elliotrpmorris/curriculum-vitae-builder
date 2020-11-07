// <copyright file="DeleteEducationEstablishmentValidator.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Domain.Command.CvSections.Education.DeleteEducationEstablishment
{
    using Chest.Core.Command;

    using FluentValidation;

    /// <summary>
    /// Delete Contact Detail Validator.
    /// </summary>
    public class DeleteEducationEstablishmentValidator : CommandValidator<DeleteEducationEstablishment>
    {
        /// <inheritdoc/>
        public override void ConfigureRules()
        {
            this.RuleFor(x => x.CvId)
               .NotEmpty()
               .WithMessage("A valid cv identifier must be provided");

            this.RuleFor(x => x.EducationEstablishment)
               .NotEmpty()
               .WithMessage("A valid education establishment name must be provided");
        }
    }
}
