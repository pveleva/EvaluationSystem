using EvaluationSystem.Domain.Entities;
using System.Collections.Generic;

namespace EvaluationSystem.Application.Answers
{
    public interface IAnswerService
    {
        List<AnswerDto> GetAllAnswers(int questionId);
        AnswerDto GetAnswerById(int questionId, int answerId);
        AnswerDto CreateAnswer(CreateAnswerDto answer);
        AnswerDto UpdateAnswer(UpdateAnswerDto answer);
        void DeleteAnswer(int questionId, int answerId);
    }
}
