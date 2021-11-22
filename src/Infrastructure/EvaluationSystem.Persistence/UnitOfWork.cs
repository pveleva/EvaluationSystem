using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using EvaluationSystem.Persistence.Dapper;
using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Application.Answers.Dapper;
using EvaluationSystem.Application.Interfaces.IQuestion;

namespace EvaluationSystem.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDbConnection _dbConnection;
        private IDbTransaction _dbTransaction;
        private IConfiguration _configuration;

        private IAnswerRepository _answerRepository;
        private IQuestionRepository _questionRepository;
        private bool _isDisposed;

        public UnitOfWork(IConfiguration configuration)
        {
            _configuration = configuration;
            _dbConnection = new SqlConnection(configuration.GetConnectionString("EvaluationSystemDBConnection"));
            _dbConnection.Open();
            _dbTransaction = _dbConnection.BeginTransaction();
        }

        public IAnswerRepository Answer
        {
            get { return _answerRepository ?? (_answerRepository = new AnswerRepository(_configuration)); }
        }

        public IQuestionRepository Question
        { get { return _questionRepository ?? (_questionRepository = new QuestionRepository(_configuration)); }
        }

        public void Commit()
        {
            try
            {
                _dbTransaction.Commit();
            }
            catch (Exception)
            {
                _dbTransaction.Rollback();
                throw;
            }
            finally
            {
                _dbTransaction.Dispose();
                _dbTransaction = _dbConnection.BeginTransaction();
                ResetRepositories();
            }
        }

        private void ResetRepositories()
        {
            _answerRepository = null;
            _questionRepository = null;
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
                if (disposing)
                {

                    if (_dbTransaction != null)
                    {
                        _dbTransaction.Dispose();
                        _dbTransaction = null;
                    }
                    if (_dbConnection != null)
                    {
                        _dbConnection.Dispose();
                        _dbConnection = null;
                    }
                }
                _isDisposed = true;
            }
        }

        UnitOfWork()
        {
            Dispose(false);
        }
    }
}
