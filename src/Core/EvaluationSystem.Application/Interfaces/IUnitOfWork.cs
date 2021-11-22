using EvaluationSystem.Application.Answers.Dapper;
using EvaluationSystem.Application.Interfaces.IQuestion;
using System;

namespace EvaluationSystem.Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IAnswerRepository Answer { get; }
        IQuestionRepository Question { get; }
        void Commit();
    }
}
