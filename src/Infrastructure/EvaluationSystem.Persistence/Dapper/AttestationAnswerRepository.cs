using Dapper;
using System.Collections.Generic;
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

        public List<AttestationAnswer> GetAll(int questionId)
        {
            string query = @"SELECT * FROM AttestationAnswer WHERE IdQuestion = @questionId";
            var result = Connection.Query<AttestationAnswer>(query, new { questionId = questionId }, Transaction);
            return (List<AttestationAnswer>)result;
        }
    }
}
