using AutoMapper;
using System.Linq;
using Azure.Identity;
using Microsoft.Graph;
using System.Threading.Tasks;
using System.Collections.Generic;
using EvaluationSystem.Application.Models.Users;
using EvaluationSystem.Application.Interfaces.IUser;

namespace EvaluationSystem.Application.Services.Dapper
{
    public class UserService : IUserService
    {
        private readonly string[] scopes = new[] { "https://graph.microsoft.com/.default" };
        private const string tenantId = "50ae1bf7-d359-4aff-91ac-b084dc52111e";
        private const string clientId = "dc32305c-c493-44e0-9654-0de398e76d50";
        private const string clientSecret = "1m57Q~ClngoPOBs-AQzcLuRnrQIXYyoX5-yLQ";

        private IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IUser _currentUser;
        public UserService(IMapper mapper, IUserRepository userRepository, IUser currentUser)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _currentUser = currentUser;
        }
        public async Task<List<UserDto>> GetAll()
        {
            var clientSecretCredential = new ClientSecretCredential(tenantId, clientId, clientSecret);
            var graphClient = new GraphServiceClient(clientSecretCredential, scopes);
            var allUsersFormAzure = await UpdatingUserInDB(graphClient);

            var users = _userRepository.GetList().OrderBy(x => x.Email).ToList();
            return _mapper.Map<List<UserDto>>(users);
        }

        private async Task<List<UsersFromAzure>> UpdatingUserInDB(GraphServiceClient graphClient)
        {
            var users = await graphClient.Users
                     .Request()
                     .Filter("(accountEnabled eq true)")
                     .GetAsync();

            var allUsers = new List<Microsoft.Graph.User>();
            while (true)
            {
                foreach (var user in users.CurrentPage)
                {
                    if (user.UserPrincipalName.EndsWith("@vsgbg.com") && !user.UserPrincipalName.EndsWith("#EXT#@vsgbg.com"))
                    {
                        allUsers.Add(user);
                    }
                }
                if (users.NextPageRequest == null)
                {
                    break;
                }
                users = await users.NextPageRequest.GetAsync();
            }

            var allUsersFormAzure = _mapper.Map<List<UsersFromAzure>>(allUsers);
            var usersFromDB = _userRepository.GetList();

            foreach (var userFromAzure in allUsersFormAzure)
            {
                var user = _userRepository.GetUserByEmail(userFromAzure.Email);
                if (user == null)
                {
                    var userName = userFromAzure.Name;
                    user = new Domain.Entities.User { Name = userName, Email = userFromAzure.Email };
                    _userRepository.Create(user);
                }
            }
            return allUsersFormAzure;
        }
        public IEnumerable<ExposeUserDto> GetUsersToEvaluate()
        {
            var users = _userRepository.GetUsersToEvaluate(_currentUser.Email);
            return _mapper.Map<IEnumerable<ExposeUserDto>>(users);
        }
    }
}
