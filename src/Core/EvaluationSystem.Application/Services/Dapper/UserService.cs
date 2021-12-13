using AutoMapper;
using System.Collections.Generic;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Models.Users;
using EvaluationSystem.Application.Interfaces.IUser;

namespace EvaluationSystem.Application.Services.Dapper
{
    public class UserService : IUserService
    {
        private IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IUser _currentUser;
        public UserService(IMapper mapper, IUserRepository userRepository, IUser currentUser)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _currentUser = currentUser;
        }
        public IEnumerable<User> GetAll()
        {
            return _userRepository.GetList();
        }
        public IEnumerable<ExposeUserDto> GetUsersToEvaluate()
        {
            var users = _userRepository.GetUsersToEvaluate(_currentUser.Email);
            return _mapper.Map<IEnumerable<ExposeUserDto>>(users);
        }
    }
}
