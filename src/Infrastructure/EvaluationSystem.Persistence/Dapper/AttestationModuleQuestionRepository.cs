using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Application.Interfaces.IAttestationModuleQuestion;

namespace EvaluationSystem.Persistence.Dapper
{
    public class AttestationModuleQuestionRepository : BaseRepository<AttestationModuleQuestion>, IAttestationModuleQuestionRepository
    {
        public AttestationModuleQuestionRepository(IUnitOfWork unitOfWork)
             : base(unitOfWork)
        {
        }
    }
}