// <copyright file="DeleteSkillValidator.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Domain.Command.CvSections.SkillsProfile.DeleteSkill
{
    using Chest.Core.Command;

    using FluentValidation;

    /// <summary>
    /// Delete Skill Validator.
    /// </summary>
    public class DeleteSkillValidator : CommandValidator<DeleteSkill>
    {
        /// <inheritdoc/>
        public override void ConfigureRules()
        {
            this.RuleFor(x => x.CvId)
                .NotEmpty()
                .WithMessage("A valid cv identifier must be provided");

            this.RuleFor(x => x.Skill)
                .NotEmpty()
                .WithMessage("A valid skill name must be provided");
        }
    }
}
