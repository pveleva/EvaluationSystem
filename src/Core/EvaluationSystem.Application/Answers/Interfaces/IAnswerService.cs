using EvaluationSystem.Domain.Entities;
using System.Collections.Generic;

namespace EvaluationSystem.Application.Answers
{
    public interface IAnswerService
    {
        AnswerDto GetAnswerById(int questionId, int answerId);
        Answer CreateAnswer(CreateAnswerDto answer);
        string UpdateAnswer(UpdateAnswerDto answer);
        string DeleteAnswer(int id);
    }
}
