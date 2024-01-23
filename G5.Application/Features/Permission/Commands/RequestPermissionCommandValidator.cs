using FluentValidation;
using G5.Application.Features.Permission.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Application.Features.Employees.Commands.CreateEmployee
{
    public class RequestPermissionCommandValidator : AbstractValidator<RequestPermissionCommand>
    {
        public RequestPermissionCommandValidator()
        {
            RuleFor(e => e.EmployeeId)
            .NotNull().WithMessage("EmployeeId cannot be null")
            .NotEqual(0).WithMessage("EmployeeId cannot be equal to zero")
            .GreaterThan(0).WithMessage("EmployeeId must be greater than zero");

            RuleFor(e => e.PermissionTypeId)
            .NotNull().WithMessage("PermissionTypeId cannot be null")
            .NotEqual(0).WithMessage("PermissionTypeId cannot be equal to zero")
            .GreaterThan(0).WithMessage("PermissionTypeId must be greater than zero");

            RuleFor(e => e.Description)
            .NotEmpty().WithMessage("The description must be at least 2 character long")
            .MaximumLength(100).WithMessage("Description must exceed 100 characters");
        }
    }
}
