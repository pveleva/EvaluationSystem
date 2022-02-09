using System.Collections.Generic;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Interfaces;

namespace EvaluationSystem.Application.Answers.Dapper
{
    public interface IAnswerRepository : IGenericRepository<AnswerTemplate>
    {
        public List<AnswerTemplate> GetAll(int questionId);
    }
}
