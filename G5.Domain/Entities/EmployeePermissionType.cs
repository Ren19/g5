using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G5.Domain.Entities
{
    public class EmployeePermissionType
    {
        [Key]

        public int EmployeeId { get; set; }
        public int PermissionTypeId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
        public Employee Employee { get; set; }
        public PermissionType PermissionType { get; set; }
    }

}
