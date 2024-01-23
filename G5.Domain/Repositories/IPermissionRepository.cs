using G5.Domain.Entities;
using G5.Domain.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G5.Domain.Repositories
{
    public interface IPermissionRepository : IRepository<G5.Domain.Entities.Permission>
    {
        Task<List<Permission>> GetPermissionsByEmployeeAsync(int employeeId);
        Task<List<Permission>> GetPermissionsByEmployeeAndTypeAsync(int employeeId, int permissionTypeId);
        Task RequestPermissionAsync(Permission permission);
        Task ModifyPermissionAsync(Permission permission);
        Task DeletePermissionAsync(int permissionId);
    }
}
