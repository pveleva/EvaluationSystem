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
        public Question GetQuestionById(int questionId)
        {
            return data.questionData.FirstOrDefault(x => x.Id == questionId);
        }
        public Answer GetQuestionAnswer(int questionId, int answerId)
        {
            return data.questionData.FirstOrDefault(x => x.Id == questionId).Answers.FirstOrDefault(x => x.Id == answerId);
        }

        public void AddQuestionToDatabase(Question question)
        {
            data.questionData.Add(question);
        }
        public bool UpdateQuestion(Question question)
        {
            Question questionToUpdate = data.questionData.FirstOrDefault(x => x.Id == question.Id);
            if (questionToUpdate != null)
            {
                questionToUpdate = question;
                return true;
            }
            return false;
        }

        public bool DeleteQuestion(int id)
        {
            Question question = data.questionData.FirstOrDefault(x => x.Id == id);
            if (question != null)
            {
                data.questionData.Remove(question);
                return true;
            }
            return false;
        }

        public string DeleteQuestionAnswer(int questionId, int answerId)
        {
            Question question = data.questionData.FirstOrDefault(x => x.Id == questionId);
            if (question != null)
            {
                Answer answer = question.Answers.FirstOrDefault(x => x.Id == answerId);
                if (answer != null)
                {
                    question.Answers.Remove(answer);
                    return "Answer successfully deleted!";
                }
                return $"Answer with id: {answerId} does not exist!";
            }
            return $"Question with id: {questionId} does not exist!";
        }


    }
}
