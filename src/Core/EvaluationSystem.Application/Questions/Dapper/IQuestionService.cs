using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EvaluationSystem.Application.Questions.Dapper
{
    public interface IQuestionService
    {
        List<QuestionDto> GetAllQuestions();
        QuestionDto GetQuestionById(int id);
        QuestionDto CreateQuestion(CreateQuestionDto question);
        QuestionDto UpdateQuestion(int id, UpdateQuestionDto questionDto);
        void DeleteQuestion(int id);
    }
}
