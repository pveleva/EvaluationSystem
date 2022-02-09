using System.Collections.Generic;

namespace EvaluationSystem.Application.Answers.Dapper
{
    public interface IAnswerService
    {
        List<AnswerDto> GetAll(int questionId);
        AnswerDto GetByID(int questionId, int answerId);
        AnswerDto Create(int questionId, AnswerDto answerDto);
        AnswerDto Update(int questionId, int answerId, CreateUpdateAnswerDto answerDto);
        void Delete(int answerId);
    }
}
