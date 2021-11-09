using EvaluationSystem.Application.Questions;
using FluentValidation;

namespace EvaluationSystem.Application.Validators
{
    public class QuestionValidator : AbstractValidator<CreateQuestionDto>
    {
        public QuestionValidator()
        {
            RuleFor(question => question.Type).NotEmpty().NotNull().IsInEnum().WithMessage("Question type is not valid!");

            RuleFor(question => question.Answers.Count).GreaterThan(0);
        }
    }
}
