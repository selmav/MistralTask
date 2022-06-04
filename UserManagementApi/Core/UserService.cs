using AutoMapper;
using Core.Models;
using Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public UserService(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<UserDto>> GetAllUsers()
        {
            var users = await _userRepository.GetAll();
            var userDtos = new List<UserDto>();
            
            foreach(var user in users)
            {
                var u = _mapper.Map<UserDto>(user);
                userDtos.Add(u);
            }

            return userDtos;
        }
    }
}
