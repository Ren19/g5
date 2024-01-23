using G5.Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G5.Application.Features.Permission.Queries
{
    public class GetPermissionsQuery : IRequest<IEnumerable<PermissionDto>>
    {
        public int EmployeeId { get; set; }
    }
}
