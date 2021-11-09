using System.Collections.Generic;

namespace EvaluationSystem.Application.Answers.Dapper
{
    public interface IAnswerService
    {
        IEnumerable<AnswerDto> GetAllAnswers(int questionId);
        AnswerDto GetAnswerById(int questionId, int answerId);
        AnswerDto CreateAnswer(int questionId, CreateUpdateAnswerDto answerDto);
        AnswerDto UpdateAnswer(int questionId, int answerId, CreateUpdateAnswerDto answerDto);
        void DeleteAnswer(int questionId, int answerId);
    }
}
