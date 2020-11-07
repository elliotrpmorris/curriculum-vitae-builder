// <copyright file="CreateUserValidator.cs" company="BJSS">
// Copyright (c) BJSS. All rights reserved.
// </copyright>

namespace CurriculumVitaeBuilder.Domain.Command.User.Create
{
    using Chest.Core.Command;

    using FluentValidation;

    /// <summary>
    /// Create user validator.
    /// </summary>
    public class CreateUserValidator : CommandValidator<CreateUser>
    {
        /// <inheritdoc/>
        public override void ConfigureRules()
        {
            this.RuleFor(x => x.UserName)
                .NotEmpty()
                .WithMessage("A valid username must be provided");
        }
    }
}
