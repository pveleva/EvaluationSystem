using FluentValidation;
using EvaluationSystem.Application.Questions;

namespace EvaluationSystem.Application.Validators
{
    public class QuestionValidator : AbstractValidator<CreateModuleQuestionDto>
    {
        public QuestionValidator()
        {
            RuleFor(question => question.Type).NotEmpty().NotNull().IsInEnum().WithMessage("Question type is not valid!");

            RuleFor(question => question.AnswerText.Count).GreaterThan(0);
        }
    }
}
