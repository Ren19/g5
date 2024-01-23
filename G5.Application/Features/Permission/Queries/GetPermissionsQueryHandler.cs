using AutoMapper;
using Employee.Application.Mappers;
using G5.Application.Dtos;
using G5.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G5.Application.Features.Permission.Queries
{
    public class GetPermissionsQueryHandler : IRequestHandler<GetPermissionsQuery, IEnumerable<PermissionDto>>
    {
        private readonly IPermissionRepository _permissionRepository;

        public GetPermissionsQueryHandler(IPermissionRepository permissionRepository)
        {
            _permissionRepository = permissionRepository;
        }


        public async Task<IEnumerable<PermissionDto>> Handle(GetPermissionsQuery request, CancellationToken cancellationToken)
        {
            var permissions = await _permissionRepository.GetPermissionsByEmployeeAsync(request.EmployeeId);
            var permissionResponse = PermissionMapper.Mapper.Map<IEnumerable<PermissionDto>>(permissions);
            return permissionResponse;
        }
    }
}
