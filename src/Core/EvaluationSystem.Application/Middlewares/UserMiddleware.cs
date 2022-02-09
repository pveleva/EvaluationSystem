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
        public UserMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context, IUserRepository userRepository, IUser currentUser)
        {
            var userEmail = context.User.Claims.FirstOrDefault(c => c.Type == "preferred_username").Value;

            var user = userRepository.GetUserByEmail(userEmail);
            if (user == null)
            {
                var userName = context.User.Claims.FirstOrDefault(n => n.Type == "name").Value;
                user = new User { Name = userName, Email = userEmail };
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
