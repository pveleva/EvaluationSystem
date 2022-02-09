using System;
using System.Data;
using Microsoft.EntityFrameworkCore;
using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Application.Answers.Dapper;
using EvaluationSystem.Application.Interfaces.IForm;
using EvaluationSystem.Application.Interfaces.IModule;
using EvaluationSystem.Application.Interfaces.IQuestion;
using EvaluationSystem.Application.Interfaces.IFormModule;
using EvaluationSystem.Application.Interfaces.IModuleQuestion;

namespace EvaluationSystem.Persistence.EF
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private bool _isDisposed;
        private DbContext _dbContext;

        private IDbConnection _connection;
        private IDbTransaction _transaction;

        public UnitOfWork(DbContext dbContext, IAnswerRepository answerRepository, IQuestionRepository questionRepository, IModuleRepository moduleRepository,
            IFormRepository formRepository, IModuleQuestionRepository moduleQuestionRepository, IFormModuleRepository formModuleRepository)
        {
            _dbContext = dbContext;
            AnswerRepository = answerRepository;
            QuestionRepository = questionRepository;
            ModuleRepository = moduleRepository;
            FormRepository = formRepository;
            ModuleQuestionRepository = moduleQuestionRepository;
            FormModuleRepository = formModuleRepository;
        }

        public IAnswerRepository AnswerRepository { get; set; }
        public IQuestionRepository QuestionRepository { get; set; }
        public IModuleRepository ModuleRepository { get; set; }
        public IFormRepository FormRepository { get; set; }
        public IModuleQuestionRepository ModuleQuestionRepository { get; set; }
        public IFormModuleRepository FormModuleRepository { get; }

        public IDbConnection Connection => _connection;

        public IDbTransaction Transaction => _transaction;

        public void Begin()
        {
            _transaction = _connection.BeginTransaction();
            //_transaction = _dbContext.Database.BeginTransaction();
        }

        public void Commit()
        {
            _dbContext.SaveChanges();
        }
        public void Rollback()
        {
            _transaction.Rollback();
            Dispose();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (!disposing || _dbContext == null)
                    return;

                _dbContext.Dispose();
                _dbContext = null;
            }

            _isDisposed = true;
        }
    }
}
