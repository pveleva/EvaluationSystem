using Dapper;
using System.Collections.Generic;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Application.Models.Forms;
using EvaluationSystem.Application.Interfaces.IAttestationForm;

namespace EvaluationSystem.Persistence.Dapper
{
    public class AttestationFormRepository : BaseRepository<AttestationForm>, IAttestationFormRepository
    {
        public AttestationFormRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public List<GetFormModuleQuestionAnswerDto> GetAll()
        {
            string query = @"SELECT f.Id AS IdForm, f.[Name] AS NameForm, fm.Position AS ModulePosition, m.Id AS IdModule, m.[Name] AS NameModule, mq.Position AS QuestionPosition, 
                                    q.Id AS IdQuestion, q.[Name] AS NameQuestion, q.[Type], a.Id AS IdAnswer, a.IsDefault, a.AnswerText AS AnswerText 
                                            FROM FormTemplate AS f
                                            LEFT JOIN FormModule AS fm ON f.Id = fm.IdForm
                                            LEFT JOIN ModuleTemplate AS m ON fm.IdModule = m.Id
                                            LEFT JOIN ModuleQuestion AS mq ON m.Id = mq.IdModule
                                            LEFT JOIN QuestionTemplate AS q ON q.Id = mq.IdQuestion
                                            LEFT JOIN AnswerTemplate AS a ON a.IdQuestion = q.Id";
            return Connection.Query<GetFormModuleQuestionAnswerDto>(query, null, Transaction).AsList();
        }

        public List<GetFormModuleQuestionAnswerDto> GetByIDFromRepo(int id)
        {
            string query = @"SELECT f.Id AS IdForm, f.[Name] AS NameForm, fm.Position AS ModulePosition, m.Id AS IdModule, m.[Name] AS NameModule, mq.Position AS QuestionPosition, 
                                    q.Id AS IdQuestion, q.[Name] AS NameQuestion, q.[Type], a.Id AS IdAnswer, a.IsDefault, a.AnswerText AS AnswerText 
                                            FROM FormTemplate AS f
                                            LEFT JOIN FormModule AS fm ON f.Id = fm.IdForm
                                            LEFT JOIN ModuleTemplate AS m ON fm.IdModule = m.Id
                                            LEFT JOIN ModuleQuestion AS mq ON m.Id = mq.IdModule
                                            LEFT JOIN QuestionTemplate AS q ON q.Id = mq.IdQuestion
                                            LEFT JOIN AnswerTemplate AS a ON a.IdQuestion = q.Id
                                            WHERE f.Id = @Id";
            return Connection.Query<GetFormModuleQuestionAnswerDto>(query, new { Id = id }, Transaction).AsList();
        }
        public void DeleteFromRepo(int id)
        {
            string delete = @"DELETE FROM AttestationFormModule WHERE IdForm = @Id";
            Connection.Execute(delete, new { Id = id }, Transaction);

            Connection.Delete<AttestationForm>(id, Transaction);
        }
    }
}
