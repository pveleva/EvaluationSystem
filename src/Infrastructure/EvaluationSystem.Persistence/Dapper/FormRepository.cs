using System;
using Dapper;
using System.Data;
using System.Collections.Generic;
using EvaluationSystem.Domain.Entities;
using Microsoft.Extensions.Configuration;
using EvaluationSystem.Application.Models.Forms;
using EvaluationSystem.Application.Interfaces.IForm;

namespace EvaluationSystem.Persistence.Dapper
{
    public class FormRepository : BaseRepository<FormTemplate>, IFormRepository
    {
        public FormRepository(IConfiguration configuration)
            : base(configuration)
        {
        }

        public List<GetFormModuleQuestionAnswerDto> GetAll()
        {
            using (IDbConnection dbConnection = Connection)
            {
                string query = @"SELECT f.Id AS IdForm, f.[Name] AS NameForm, m.Id AS IdModule, m.[Name] AS NameModule, 
                                        q.Id AS IdQuestion, q.[Name] AS NameQuestion, a.Id AS IdAnswer, a.AnswerText AS AnswerText 
                                            FROM FormTemplate AS f
                                            LEFT JOIN FormModule AS fm ON f.Id = fm.IdForm
                                            LEFT JOIN ModuleTemplate AS m ON fm.IdModule = m.Id
                                            LEFT JOIN ModuleQuestion AS mq ON m.Id = mq.IdModule
                                            LEFT JOIN QuestionTemplate AS q ON q.Id = mq.IdQuestion
                                            LEFT JOIN AnswerTemplate AS a ON a.IdQuestion = q.Id";
                return dbConnection.Query<GetFormModuleQuestionAnswerDto>(query).AsList();
            }
        }

        public List<GetFormModuleQuestionAnswerDto> GetByIDFromRepo(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string query = @"SELECT f.Id AS IdForm, f.[Name] AS NameForm, m.Id AS IdModule, m.[Name] AS NameModule, 
                                        q.Id AS IdQuestion, q.[Name] AS NameQuestion, a.Id AS IdAnswer, a.AnswerText AS AnswerText 
                                            FROM FormTemplate AS f
                                            LEFT JOIN FormModule AS fm ON f.Id = fm.IdForm
                                            LEFT JOIN ModuleTemplate AS m ON fm.IdModule = m.Id
                                            LEFT JOIN ModuleQuestion AS mq ON m.Id = mq.IdModule
                                            LEFT JOIN QuestionTemplate AS q ON q.Id = mq.IdQuestion
                                            LEFT JOIN AnswerTemplate AS a ON a.IdQuestion = q.Id
                                            WHERE f.Id = @Id";
                return dbConnection.Query<GetFormModuleQuestionAnswerDto>(query, new { Id = id }).AsList();
            }
        }

        public void DeleteFromRepo(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string delete = @"DELETE FROM FormModule WHERE IdForm = @Id";
                dbConnection.Execute(delete, new { Id = id });

                dbConnection.Delete<FormTemplate>(id);
            }
        }

    }
}
