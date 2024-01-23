using G5.Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G5.Application.Features.Permission.Commands
{
    public class RequestPermissionCommand : IRequest<PermissionDto>
    {
        public int EmployeeId { get; set; }
        public int PermissionTypeId { get; set; }
        public string Description { get; set; }
    }
}
