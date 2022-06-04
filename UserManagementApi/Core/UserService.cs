using AutoMapper;
using Common;
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

        public async Task<PagedList<UserDto>> GetAllUsers(Pagination pagination)
        {
            var users = await _userRepository.GetUsers(pagination);
            var userDtos = new List<UserDto>();
            
            foreach(var user in users)
            {
                var u = _mapper.Map<UserDto>(user);
                userDtos.Add(u);
            }

            return new PagedList<UserDto>
            {
                Results = userDtos,
                Page = pagination.Page,
                PageSize = pagination.PageSize,
                TotalPages = pagination.TotalPages
            };
        }
    }
}
