using System.Collections.Generic;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Models.Users;

namespace EvaluationSystem.Application.Interfaces.IUser
{
    public interface IUserRepository : IGenericRepository<User>
    {
        public List<ExposeUserDto> GetUsersToEvaluate(string email);
    }
}
