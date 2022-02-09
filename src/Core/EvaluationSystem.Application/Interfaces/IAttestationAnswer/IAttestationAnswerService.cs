using System.Collections.Generic;
using EvaluationSystem.Application.Answers;

namespace EvaluationSystem.Application.Interfaces.IAttestationAnswer
{
    public interface IAttestationAnswerService
    {
        List<AnswerDto> GetAll(int questionId);
        AnswerDto GetByID(int questionId, int answerId);
        AnswerDto Create(int questionId, AnswerDto answerDto);
        void Delete(int answerId);
    }
}
