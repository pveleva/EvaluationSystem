using EvaluationSystem.Application.Answers;
using FluentValidation;

namespace EvaluationSystem.Application.Validators
{
    public class AnswerValidator : AbstractValidator<AnswerDto>
    {
        public AnswerValidator()
        {
            RuleFor(answer => answer.Content)
                .NotEmpty()
                .NotNull()
                .MinimumLength(5)
                .MaximumLength(255)
                .WithMessage("Answer could not be empty or null!"); //може ли да имаме празно текстово поле?
        }
    }
}
