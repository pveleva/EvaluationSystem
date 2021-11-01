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
            return data.answerData.Where(x => x.QuestionId == questionId).ToList().FirstOrDefault(x => x.Id == answerId);
        }


        public void AddAnswerToDatabase(Answer answer)
        {
            data.answerData.Add(answer);
        }

        public bool UpdateAnswer(Answer answer)
        {
            Answer answerToUpdate = data.answerData.FirstOrDefault(x => x.Id == answer.Id);
            if (answerToUpdate != null)
            {
                answerToUpdate = answer;
                return true;
            }
            return false;
        }

        public bool DeleteAnswer(int id)
        {
            Answer answer = data.answerData.FirstOrDefault(x => x.Id == id);
            if (answer != null)
            {
                data.answerData.Remove(answer);
                return true;
            }
            return false;
        }
    }
}
