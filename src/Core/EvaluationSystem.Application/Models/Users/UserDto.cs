using EvaluationSystem.Application.Interfaces.IUser;

namespace EvaluationSystem.Application.Models.Users
{
    public class UserDto : IUser
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
    }
}
