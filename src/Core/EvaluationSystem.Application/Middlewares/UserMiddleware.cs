using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using EvaluationSystem.Application.Interfaces.IUser;
using EvaluationSystem.Domain.Entities;

namespace EvaluationSystem.Application.Middlewares
{
    public class UserMiddleware
    {
        private readonly RequestDelegate _next;
        private IUserRepository _userRepository;
        public UserMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context, IUserRepository userRepository, IUser currentUser)
        {
            _userRepository = userRepository;
            
            var userEmail = context.User.Claims.FirstOrDefault(c => c.Type == "preferred_username")?.Value;

            var user = _userRepository.GetList().FirstOrDefault(u => u.Email == userEmail);

            if (user == null)
            {
                var username = context.User.Claims.FirstOrDefault(c => c.Type == "name")?.Value;
                user = new User { Name = username, Email = userEmail };
                var id = userRepository.Create(user);
                user.Id = id;
            }

            currentUser.Id = user.Id;
            currentUser.Name = user.Name;
            currentUser.Email = user.Email;

            await _next.Invoke(context);
        }
    }
}
