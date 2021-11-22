using System;
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

        public List<GetModulesDto> GetAll()
        {
            using (IDbConnection dbConnection = Connection)
            {
                string query = @"SELECT m.Id, m.[Name], q.Id, q.[Name], q.[Type], q.IsReusable, a.Id, a.AnswerText, a.Position, a.IsDefault FROM ModuleTemplate AS m
                                        LEFT JOIN ModuleQuestion AS mq ON m.Id = mq.IdModule
                                        LEFT JOIN QuestionTemplate AS q ON q.Id = mq.IdQuestion
                                        LEFT JOIN AnswerTemplate AS a ON a.IdQuestion = q.Id";
                return dbConnection.Query<GetModulesDto>(query).AsList();
            }
        }

        public List<GetModulesDto> GetByIDFromRepo(int id)
        {
            throw new NotImplementedException();
        }
        public void DeleteFromRepo(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string delete = @"DELETE FROM FormModule WHERE IdModule = @Id";
                dbConnection.Execute(delete, new { Id = id });

                dbConnection.Delete<ModuleTemplate>(id);
            }
        }
    }
}
