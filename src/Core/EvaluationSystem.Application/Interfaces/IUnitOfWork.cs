using EvaluationSystem.Application.Answers.Dapper;
using EvaluationSystem.Application.Questions.Dapper;
using System;
using System.Threading.Tasks;

namespace EvaluationSystem.Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IAnswerRepository Answer { get; }
        IQuestionRepository Question { get; }
        void Commit();
    }
}
