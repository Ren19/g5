using G5.API;
using G5.Application.Dtos;
using G5.Application.Exceptions;
using G5.Application.Features.Permission.Commands;
using G5.Application.Features.Permission.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace G5.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PermissionController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PermissionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("RequestPermission")]
        public async Task<IActionResult> RequestPermission([FromBody] RequestPermissionCommand request)
        {
            var response = new ResponsAPI<PermissionDto>();
            try
            {
                response.Data = await _mediator.Send(request);
            }
            catch (CustomExceptions ex)
            {
                ex.ErrorMessages.ForEach(messag => response.AddError(messag));
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost("ModifyPermission")]
        public async Task<IActionResult> ModifyPermission([FromBody] ModifyPermissionCommand request)
        {
            var response = new ResponsAPI<PermissionDto>();
            try
            {
                response.Data = await _mediator.Send(request);
            }
            catch (CustomExceptions ex)
            {
                ex.ErrorMessages.ForEach(messag => response.AddError(messag));
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet("GetPermissions")]
        public async Task<IActionResult> GetPermissions(int employeeId)
        {
            var response = new ResponsAPI<List<PermissionDto>>();
            try
            {
                var query = new GetPermissionsQuery { EmployeeId = employeeId };
                var result = await _mediator.Send(query);
                if (result == null)
                {
                    response.AddError($"No permissions found with the id {employeeId}");
                    return NotFound(response);
                }
                response.Data = result.ToList();
            }
            catch (Exception ex)
            {
                response.AddError(ex.Message);
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
