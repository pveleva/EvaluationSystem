using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Domain.Entities;
using System.Collections.Generic;

namespace EvaluationSystem.Application.Answers.Dapper
{
    public interface IAnswerRepository : IGenericRepository<AnswerTemplate>
    {
        public List<AnswerTemplate> GetAll(int questionId);
    }
}
