using EvaluationSystem.Application.Questions;
using EvaluationSystem.Domain.Entities;
using FluentValidation;

namespace EvaluationSystem.Application.Validators
{
    public class QuestionValidator : AbstractValidator<QuestionDto>
    {
        public QuestionValidator()
        {
            RuleFor(question => question.Type).NotEmpty().NotNull().IsInEnum().WithMessage("Question type is not valid!");

            RuleFor(question => question.IAnswers.Count).GreaterThan(0);
        }
    }
}
