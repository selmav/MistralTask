using AutoMapper;
using Core.Models;
using Data;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core
{
    public class PermissionService : IPermissionService
    {
        private readonly IPermissionRepository _permissionRepository;
        private readonly IMapper _mapper;

        public PermissionService(IMapper mapper, IPermissionRepository permissionRepository)
        {
            _mapper = mapper;
            _permissionRepository = permissionRepository;
        }

        public async Task<IEnumerable<PermissionDto>> GetAllPermissions()
        {
            var permissions = await _permissionRepository.GetAllPermissions();
            return _mapper.Map<IEnumerable<PermissionDto>>(permissions);
        }

        public async Task<IEnumerable<PermissionDto>> GetUserPermissions(int? userId)
        {
            if (!userId.HasValue || userId.Value < 1)
            {
                throw new ArgumentException("Invalid user id");
            }

            var userPermissions = (await _permissionRepository.GetUserPermissions(userId.Value))
                .Select(item => item.Permission);

            return _mapper.Map<IEnumerable<PermissionDto>>(userPermissions);
        }

        public async Task<IEnumerable<int>> UpdateUserPermissions(UserPermissionDto userPermissions)
        {
            if (userPermissions.UserId < 1 || userPermissions.PermissionIds == null)
            {
                throw new ArgumentException("Invalid request");
            }

            var currentPermissions = (await _permissionRepository.GetUserPermissions(userPermissions.UserId))
                                     .Select(p => p.PermissionId);

            var removed = currentPermissions.Except(userPermissions.PermissionIds);
            currentPermissions = currentPermissions.Intersect(userPermissions.PermissionIds);

            var newPermissions = userPermissions.PermissionIds.Except(currentPermissions);

            await _permissionRepository.InsertPermissions(userPermissions.UserId, newPermissions);
            await _permissionRepository.RemoveUserPermissions(userPermissions.UserId, removed);
            await _permissionRepository.SaveAsync();

            return userPermissions.PermissionIds;
        }
    }
}
