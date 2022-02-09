using Dapper;
using System.Collections.Generic;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Application.Models.Forms;
using EvaluationSystem.Application.Interfaces.IAttestationModule;

namespace EvaluationSystem.Persistence.Dapper
{
    public class AttestationModuleRepository : BaseRepository<AttestationModule>, IAttestationModuleRepository
    {
        public AttestationModuleRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public List<GetFormModuleQuestionAnswerDto> GetAll()
        {
            string query = @"SELECT fm.IdForm AS IdForm, fm.Position AS ModulePosition, m.Id AS IdModule, m.[Name] AS NameModule, m.Description, mq.Position AS QuestionPosition, 
                                    q.Id AS IdQuestion, q.[Name] AS NameQuestion, q.[Type], a.Id AS IdAnswer, a.IsDefault, a.AnswerText AS AnswerText 
                                            FROM AttestationFormModule AS fm 
											LEFT JOIN AttestationModule AS m ON fm.IdModule = m.Id
                                            LEFT JOIN AttestationModuleQuestion AS mq ON m.Id = mq.IdModule
                                            LEFT JOIN AttestationQuestion AS q ON q.Id = mq.IdQuestion
                                            LEFT JOIN AttestationAnswer AS a ON a.IdQuestion = q.Id";
            return Connection.Query<GetFormModuleQuestionAnswerDto>(query, null, Transaction).AsList();
        }

        public List<GetFormModuleQuestionAnswerDto> GetByIDFromRepo(int id)
        {
            string query = @"SELECT fm.IdForm AS IdForm, fm.Position AS ModulePosition, m.Id AS IdModule, m.[Name] AS NameModule, m.Description, mq.Position AS QuestionPosition, 
                                    q.Id AS IdQuestion, q.[Name] AS NameQuestion, q.[Type], a.Id AS IdAnswer, a.IsDefault, a.AnswerText AS AnswerText 
                                            FROM AttestationFormModule AS fm 
											LEFT JOIN AttestationModule AS m ON fm.IdModule = m.Id
                                            LEFT JOIN AttestationModuleQuestion AS mq ON m.Id = mq.IdModule
                                            LEFT JOIN AttestationQuestion AS q ON q.Id = mq.IdQuestion
                                            LEFT JOIN AttestationAnswer AS a ON a.IdQuestion = q.Id
                                            WHERE m.Id = @Id";
            return Connection.Query<GetFormModuleQuestionAnswerDto>(query, new { Id = id }, Transaction).AsList();
        }
        public void DeleteFromRepo(int id)
        {
            string deleteFM = @"DELETE FROM AttestationFormModule WHERE IdModule = @Id";
            Connection.Execute(deleteFM, new { Id = id }, Transaction);

            string deleteMQ = @"DELETE FROM AttestationModuleQuestion WHERE IdModule = @Id";
            Connection.Execute(deleteMQ, new { Id = id }, Transaction);

            Connection.Delete<AttestationModule>(id, Transaction);
        }
    }
}
