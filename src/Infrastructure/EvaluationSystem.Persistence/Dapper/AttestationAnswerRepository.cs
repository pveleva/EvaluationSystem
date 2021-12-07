using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Application.Interfaces.IAttestationAnswer;

namespace EvaluationSystem.Persistence.Dapper
{
    public class AttestationAnswerRepository : BaseRepository<AttestationAnswer>, IAttestationAnswerRepository
    {
        public AttestationAnswerRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
    }
}
