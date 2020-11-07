// <copyright file="DeleteJobValidator.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Domain.Command.CvSections.JobHistory.DeleteJob
{
    using Chest.Core.Command;

    using FluentValidation;

    /// <summary>
    /// Delete Job Validator.
    /// </summary>
    public class DeleteJobValidator : CommandValidator<DeleteJob>
    {
        /// <inheritdoc/>
        public override void ConfigureRules()
        {
            this.RuleFor(x => x.CvId)
                .NotEmpty()
                .WithMessage("A valid cv identifier must be provided");

            this.RuleFor(x => x.Employer)
               .NotEmpty()
               .WithMessage("A valid employer must be provided");

            this.RuleFor(x => x.JobTitle)
              .NotEmpty()
              .WithMessage("A valid job title must be provided");
        }
    }
}
