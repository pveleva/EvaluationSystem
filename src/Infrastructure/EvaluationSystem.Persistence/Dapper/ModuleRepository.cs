using Dapper;
using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Application.Interfaces.IModule;
using EvaluationSystem.Application.Models.Modules;
using EvaluationSystem.Domain.Entities;
using System.Collections.Generic;

namespace EvaluationSystem.Persistence.Dapper
{
    public class ModuleRepository : BaseRepository<ModuleTemplate>, IModuleRepository
    {
        public ModuleRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public List<GetModuleQuestionAnswerDto> GetAll()
        {
            string query = @"SELECT m.Id AS IdModule, m.[Name] AS NameModule, q.Id AS IdQuestion, q.[Name] AS NameQuestion, 
                                        a.Id AS IdAnswer, a.AnswerText AS AnswerText FROM ModuleTemplate AS m
                                             LEFT JOIN ModuleQuestion AS mq ON m.Id = mq.IdModule
                                             LEFT JOIN QuestionTemplate AS q ON q.Id = mq.IdQuestion
                                             LEFT JOIN AnswerTemplate AS a ON a.IdQuestion = q.Id";
            return Connection.Query<GetModuleQuestionAnswerDto>(query, null, Transaction).AsList();
        }

        public List<GetModuleQuestionAnswerDto> GetByIDFromRepo(int id)
        {
            string query = @"SELECT m.Id AS IdModule, m.[Name] AS NameModule, q.Id AS IdQuestion, q.[Name] AS NameQuestion, 
                                        a.Id AS IdAnswer, a.AnswerText AS AnswerText FROM ModuleTemplate AS m
                                             LEFT JOIN ModuleQuestion AS mq ON m.Id = mq.IdModule
                                             LEFT JOIN QuestionTemplate AS q ON q.Id = mq.IdQuestion
                                             LEFT JOIN AnswerTemplate AS a ON a.IdQuestion = q.Id
                                             WHERE m.Id = @Id";
            return Connection.Query<GetModuleQuestionAnswerDto>(query, new { Id = id }, Transaction).AsList();
        }
        public void DeleteFromRepo(int id)
        {
            string deleteFM = @"DELETE FROM FormModule WHERE IdModule = @Id";
            Connection.Execute(deleteFM, new { Id = id }, Transaction);

            string deleteMQ = @"DELETE FROM ModuleQuestion WHERE IdModule = @Id";
            Connection.Execute(deleteMQ, new { Id = id }, Transaction);

            Connection.Delete<ModuleTemplate>(id, Transaction);
        }
    }
}
