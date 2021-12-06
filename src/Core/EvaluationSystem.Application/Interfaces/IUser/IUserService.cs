using System.Collections.Generic;
using EvaluationSystem.Domain.Entities;

namespace EvaluationSystem.Application.Interfaces.IUser
{
    public interface IUserService
    {
        public IEnumerable<User> GetAll();
    }
}
