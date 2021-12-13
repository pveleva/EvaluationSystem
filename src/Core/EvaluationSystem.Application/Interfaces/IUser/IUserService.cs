using System.Collections.Generic;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Models.Users;

namespace EvaluationSystem.Application.Interfaces.IUser
{
    public interface IUserService
    {
        public IEnumerable<User> GetAll();
        public IEnumerable<ExposeUserDto> GetUsersToEvaluate();
    }
}
