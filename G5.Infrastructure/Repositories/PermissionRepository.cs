using Employee.Infrastructure.Repositories.Base;
using G5.Domain.Entities;
using G5.Domain.Repositories;
using G5.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace G5.Infrastructure.Repositories
{
    public class PermissionRepository : Repository<G5.Domain.Entities.Permission>, IPermissionRepository
    {
        public PermissionRepository(G5Context context) : base(context) { }

        public async Task<List<Permission>> GetPermissionsByEmployeeAsync(int employeeId)
        {
            
            return await _context.Permissions
                .Include(p => p.Employee)
                .Include(p => p.PermissionType)
                .Where(p => p.EmployeeId == employeeId)
                .ToListAsync();
        }

        public async Task<List<Permission>> GetPermissionsByEmployeeAndTypeAsync(int employeeId, int permissionTypeId)
        {
            return await _context.Permissions
                .Include(p => p.Employee)
                .Include(p => p.PermissionType)
                .Where(p => p.EmployeeId == employeeId && p.PermissionTypeId == permissionTypeId)
                .ToListAsync();
        }

        public async Task RequestPermissionAsync(Permission permission)
        {
            _context.Permissions.Add(permission);
            await _context.SaveChangesAsync();
        }

        public async Task ModifyPermissionAsync(Permission permission)
        {
            _context.Entry(permission).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeletePermissionAsync(int permissionId)
        {
            var permission = await _context.Permissions.FindAsync(permissionId);
            _context.Permissions.Remove(permission);
            await _context.SaveChangesAsync();
        }
    }
}
