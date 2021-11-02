using EvaluationSystem.Application.Answers;
using EvaluationSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EvaluationSystem.Persistence.Repositories
{
    public class AnswerRepository : IAnswerRepository
    {
        private Database data = new Database();

        public Answer GetAnswerById(int questionId, int answerId)
        {
            return data.answerData.FirstOrDefault(a => a.QuestionId == questionId && a.Id == answerId);
        }
        public List<Answer> GetAllAnswers(int questionId)
        {
            return data.answerData.Where(a => a.QuestionId == questionId).ToList();
        }

        public void AddAnswerToDatabase(Answer answer)
        {
            data.answerData.Add(answer);
        }

        public Answer UpdateAnswer(Answer answer)
        {
            Answer answerToUpdate = data.answerData.FirstOrDefault(a => a.Id == answer.Id);
            return answerToUpdate = answer;
        }

        public void DeleteAnswer(int questionId, int answerId)
        {
            Answer answer = data.answerData.FirstOrDefault(a => a.QuestionId == questionId && a.Id == answerId);
            data.answerData.Remove(answer);
        }
    }
}
