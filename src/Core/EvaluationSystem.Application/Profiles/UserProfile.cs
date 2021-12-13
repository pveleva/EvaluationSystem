using AutoMapper;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Models.Users;

namespace EvaluationSystem.Application.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, ExposeUserDto>();
        }
    }
}
