using System.Collections.Generic;
using EvaluationSystem.Domain.Entities;

namespace EvaluationSystem.Application.Interfaces.IUser
{
    public interface IUserRepository : IGenericRepository<User>
    {
        public List<User> GetUsersToEvaluate(string email);
    }
}
