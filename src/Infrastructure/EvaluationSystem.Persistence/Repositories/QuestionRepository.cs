using EvaluationSystem.Application.Questions;
using EvaluationSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EvaluationSystem.Persistence.Repositories
{
    public class QuestionRepository : IQuestionRepository
    {
        private Database data = new Database();
        public List<Question> GetAllQuestions()
        {
            return data.questionData.ToList();
        }

        public Question GetQuestionById(int questionId)
        {
            return data.questionData.FirstOrDefault(x => x.Id == questionId);
        }

        public void AddQuestionToDatabase(Question question)
        {
            data.questionData.Add(question);
        }
        public void UpdateQuestion(Question question)
        {
            Question questionToUpdate = data.questionData.FirstOrDefault(x => x.Id == question.Id);
            questionToUpdate = question;
        }

        public void DeleteQuestion(int id)
        {
            Question question = data.questionData.FirstOrDefault(x => x.Id == id);
            data.questionData.Remove(question);
        }

    }
}
