using System.Collections.Generic;

namespace EvaluationSystem.Application.Answers.Dapper
{
    public interface IAnswerService
    {
        List<AnswerDto> GetAll(int questionId);
        AnswerDto GetById(int questionId, int answerId);
        AnswerDto CreateAnswer(int questionId, CreateUpdateAnswerDto answerDto);
        AnswerDto UpdateAnswer(int questionId, int answerId, CreateUpdateAnswerDto answerDto);
        void DeleteAnswer(int answerId);
    }
}
