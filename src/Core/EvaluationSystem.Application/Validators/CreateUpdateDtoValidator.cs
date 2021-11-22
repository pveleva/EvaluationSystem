﻿using EvaluationSystem.Application.Answers;
using FluentValidation;

namespace EvaluationSystem.Application.Validators
{
    public class CreateUpdateDtoValidator : AbstractValidator<CreateUpdateAnswerDto>
    {
        public CreateUpdateDtoValidator()
        {
            RuleFor(answer => answer.IsDefault)
                 .NotEmpty()
                 .NotNull()
                 .WithMessage($"Property value {nameof(CreateUpdateAnswerDto.IsDefault)} could not be empty or null!");

            RuleFor(answer => answer.Position)
                 .NotEmpty()
                 .NotNull()
                 .WithMessage($"Property value {nameof(CreateUpdateAnswerDto.Position)} could not be empty or null!");

            RuleFor(answer => answer.AnswerText)
                .NotEmpty()
                .NotNull()
                .MinimumLength(1)
                .MaximumLength(255)
                .WithMessage("Answer could not be empty or null! It has to be wih at least 1 characters and maximum length of 255 characters."); //може ли да имаме празно текстово поле?
        }
    }
}
