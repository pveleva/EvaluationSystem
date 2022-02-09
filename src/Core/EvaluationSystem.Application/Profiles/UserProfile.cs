using AutoMapper;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Models.Users;

namespace EvaluationSystem.Application.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, ExposeUserDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Microsoft.Graph.User, UsersFromAzure>()
                    .ForMember(q => q.Name, opts => opts.MapFrom(qd => qd.DisplayName))
                    .ForMember(q => q.Email, opts => opts.MapFrom(t => t.UserPrincipalName)).ReverseMap();
        }
    }
}
