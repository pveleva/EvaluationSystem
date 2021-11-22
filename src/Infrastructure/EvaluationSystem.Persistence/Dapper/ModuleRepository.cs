using Dapper;
using System.Data;
using System.Collections.Generic;
using EvaluationSystem.Domain.Entities;
using Microsoft.Extensions.Configuration;
using EvaluationSystem.Application.Models.Modules;
using EvaluationSystem.Application.Interfaces.IModule;

namespace EvaluationSystem.Persistence.Dapper
{
    public class ModuleRepository : BaseRepository<ModuleTemplate>, IModuleRepository
    {
        public ModuleRepository(IConfiguration configuration)
            : base(configuration)
        {
        }

        public List<GetModuleQuestionAnswerDto> GetAll()
        {
            using (IDbConnection dbConnection = Connection)
            {
                string query = @"SELECT m.Id AS IdModule, m.[Name] AS NameModule, q.Id AS IdQuestion, q.[Name] AS NameQuestion, 
                                        a.Id AS IdAnswer, a.AnswerText AS AnswerText FROM ModuleTemplate AS m
                                             LEFT JOIN ModuleQuestion AS mq ON m.Id = mq.IdModule
                                             LEFT JOIN QuestionTemplate AS q ON q.Id = mq.IdQuestion
                                             LEFT JOIN AnswerTemplate AS a ON a.IdQuestion = q.Id";
                return dbConnection.Query<GetModuleQuestionAnswerDto>(query).AsList();
            }
        }

        public List<GetModuleQuestionAnswerDto> GetByIDFromRepo(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string query = @"SELECT m.Id AS IdModule, m.[Name] AS NameModule, q.Id AS IdQuestion, q.[Name] AS NameQuestion, 
                                        a.Id AS IdAnswer, a.AnswerText AS AnswerText FROM ModuleTemplate AS m
                                             LEFT JOIN ModuleQuestion AS mq ON m.Id = mq.IdModule
                                             LEFT JOIN QuestionTemplate AS q ON q.Id = mq.IdQuestion
                                             LEFT JOIN AnswerTemplate AS a ON a.IdQuestion = q.Id
                                             WHERE m.Id = @Id";
                return dbConnection.Query<GetModuleQuestionAnswerDto>(query, new { Id = id }).AsList();
            }
        }
        public void DeleteFromRepo(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string delete = @"DELETE FROM ModuleQuestion WHERE IdModule = @Id";
                dbConnection.Execute(delete, new { Id = id });

                dbConnection.Delete<ModuleTemplate>(id);
            }
        }
    }
}
