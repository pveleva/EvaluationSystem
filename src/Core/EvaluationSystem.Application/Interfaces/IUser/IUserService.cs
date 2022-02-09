using System.Threading.Tasks;
using System.Collections.Generic;
using EvaluationSystem.Application.Models.Users;

namespace EvaluationSystem.Application.Interfaces.IUser
{
    public interface IUserService
    {
        Task<List<UserDto>> GetAll();
        public IEnumerable<ExposeUserDto> GetUsersToEvaluate();
    }
}
