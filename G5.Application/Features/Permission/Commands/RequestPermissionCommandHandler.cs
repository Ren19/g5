using Employee.Application.Features.Employees.Commands.CreateEmployee;
using Employee.Application.Mappers;
using G5.Application.Dtos;
using G5.Application.Exceptions;
using G5.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G5.Application.Features.Permission.Commands
{
    public class RequestPermissionCommandHandler : IRequestHandler<RequestPermissionCommand, PermissionDto>
    {
        private readonly IPermissionRepository _permissionRepository;

        public RequestPermissionCommandHandler(IPermissionRepository permissionRepository)
        {
            _permissionRepository = permissionRepository;
        }

        public async Task<PermissionDto> Handle(RequestPermissionCommand request, CancellationToken cancellationToken)
        {
            RequestPermissionCommandValidator validations = new RequestPermissionCommandValidator();
            var result = await validations.ValidateAsync(request);

            if (result.Errors.Any())
            {
                throw new CustomExceptions("Permission is not valid", result.Errors.Select(err => err.ErrorMessage).ToList());
            }

            var emt = await _permissionRepository.GetPermissionsByEmployeeAndTypeAsync(request.EmployeeId, request.PermissionTypeId);
            if (emt != null && emt.Any())
            {
                throw new CustomExceptions("Update permission is not vaild", new List<string> { $"No permission found with the employee id {request.EmployeeId} and type id { request.PermissionTypeId }"  });
            }

            var permissionEntity = PermissionMapper.Mapper.Map<G5.Domain.Entities.Permission>(request);

            permissionEntity.CreatedAt = DateTime.UtcNow;
            permissionEntity.CreatedBy = "ADMIN";

            var newPermission = await _permissionRepository.AddAsync(permissionEntity);
            var permissionResponse = PermissionMapper.Mapper.Map<PermissionDto>(newPermission);

            return permissionResponse;
        }
    }
}
