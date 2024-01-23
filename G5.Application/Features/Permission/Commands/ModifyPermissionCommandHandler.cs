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
    public class ModifyPermissionCommandHandler : IRequestHandler<ModifyPermissionCommand, PermissionDto>
    {
        private readonly IPermissionRepository _permissionRepository;

        public ModifyPermissionCommandHandler(IPermissionRepository permissionRepository)
        {
            _permissionRepository = permissionRepository;
        }

        public async Task<PermissionDto> Handle(ModifyPermissionCommand request, CancellationToken cancellationToken)
        {
            ModifyPermissionCommandValidator validations = new ModifyPermissionCommandValidator();
            var result = await validations.ValidateAsync(request);

            if (result.Errors.Any())
            {
                throw new CustomExceptions("Update permission is not vaild", result.Errors.Select(messageErr => messageErr.ErrorMessage).ToList());
            }
            var em = await _permissionRepository.GetByIdAsync(request.PermissionId);
            if (em == null)
            {
                throw new CustomExceptions("Update permission is not vaild", new List<string> { $"No permission found with the id {request.PermissionId}" });
            }
            var emt = await _permissionRepository.GetPermissionsByEmployeeAndTypeAsync(request.EmployeeId, request.PermissionTypeId);
            if (emt != null && emt.Any())
            {
                throw new CustomExceptions("Update permission is not vaild", new List<string> { $"No permission found with the employee id {request.EmployeeId} and type id {request.PermissionTypeId}" });
            }
            em.UpdatedAt = DateTime.UtcNow;
            em.UpdatedBy = "ADMIN";

            var permissionEntity = PermissionMapper.Mapper.Map<ModifyPermissionCommand, G5.Domain.Entities.Permission>(request, em);
            var newPermission = await _permissionRepository.UpdateAsync(permissionEntity);

            return PermissionMapper.Mapper.Map<PermissionDto>(newPermission);
        }
    }
}
