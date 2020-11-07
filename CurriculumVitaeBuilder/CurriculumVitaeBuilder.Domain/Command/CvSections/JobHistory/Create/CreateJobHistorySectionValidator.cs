// <copyright file="CreateJobHistorySectionValidator.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Domain.Command.CvSections.JobHistory.Create
{
    using Chest.Core.Command;

    using FluentValidation;

    /// <summary>
    /// Create Section Validator.
    /// </summary>
    public class CreateJobHistorySectionValidator : CommandValidator<CreateJobHistorySection>
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

            this.RuleFor(x => x.Jobs)
                .NotEmpty()
                .WithMessage("Jobs must be provided");
        }
    }
}
