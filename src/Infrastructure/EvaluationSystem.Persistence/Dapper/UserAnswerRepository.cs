using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Application.Interfaces.IUserAnswer;

namespace EvaluationSystem.Persistence.Dapper
{
    public class UserAnswerRepository : BaseRepository<UserAnswer>, IUserAnswerRepository
    {
        public UserAnswerRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
    }
}
