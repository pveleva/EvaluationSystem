using System.Collections.Generic;
using EvaluationSystem.Domain.Entities;

namespace EvaluationSystem.Application.Interfaces.IAttestationAnswer
{
    public  interface IAttestationAnswerRepository : IGenericRepository<AttestationAnswer>
    {
        public List<AttestationAnswer> GetAll(int questionId);
    }
}
