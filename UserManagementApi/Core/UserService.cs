using AutoMapper;
using Common;
using Core.Models;
using Data;
using Data.Entities;
using System;
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

        public async Task<PagedList<UserDto>> GetAllUsers(Pagination pagination, Ordering ordering)
        {
            var users = await _userRepository.GetUsers(pagination, ordering);
            var userDtos = new List<UserDto>();
         
            // todo: automapper can map list
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

        public async Task<UserDto> AddUser(UserDto userDto)
        {
            if (userDto == null)
            {
                throw new ArgumentNullException();
            }

            var user = _mapper.Map<User>(userDto);

            user = await _userRepository.AddUser(user);

            return _mapper.Map<UserDto>(user);
        }
    }
}
