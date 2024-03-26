using Confluent.Kafka;
using G5.API;
using G5.Application.Dtos;
using G5.Application.Exceptions;
using G5.Application.Features.Permission.Commands;
using G5.Application.Features.Permission.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace G5.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PermissionController : ControllerBase
    {
        private readonly IProducer<string, string> producer;
        private readonly ILogger<PermissionController> _logger;
        private readonly IMediator _mediator;
        public PermissionController(IMediator mediator, ILogger<PermissionController> logger)
        {
            var producerConfig = new ProducerConfig
            {
                BootstrapServers = "localhost:9092",
                ClientId = "operaciones-producer",
            };

            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            producer = new ProducerBuilder<string, string>(producerConfig)
            .SetValueSerializer(Serializers.Utf8)
            .Build();
        }

        [HttpPost("RequestPermission")]
        public async Task<IActionResult> RequestPermission([FromBody] RequestPermissionCommand request)
        {
            var response = new ResponsAPI<PermissionDto>();
            try
            {
                response.Data = await _mediator.Send(request);

                var operacionDto = new OperacionDto
                {
                    Id = Guid.NewGuid(),
                    NameOperation = "request",
                };
                var serializedOperacion = JsonConvert.SerializeObject(operacionDto);
                await producer.ProduceAsync("operaciones-topic", new Message<string, string>
                {
                    Key = operacionDto.Id.ToString(),
                    Value = serializedOperacion
                });

                //_logger.LogInformation($"RequestPermission - OK: {JsonConvert.SerializeObject(response)}");
            }
            catch (CustomExceptions ex)
            {
                ex.ErrorMessages.ForEach(messag => response.AddError(messag));
                _logger.LogError($"RequestPermission - Error: {ex.Message}");
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

                var operacionDto = new OperacionDto
                {
                    Id = Guid.NewGuid(),
                    NameOperation = "modify",
                };
                var serializedOperacion = JsonConvert.SerializeObject(operacionDto);
                await producer.ProduceAsync("operaciones-topic", new Message<string, string>
                {
                    Key = operacionDto.Id.ToString(),
                    Value = serializedOperacion
                });

                _logger.LogInformation($"ModifyPermission - OK: {JsonConvert.SerializeObject(response)}");
            }
            catch (CustomExceptions ex)
            {
                ex.ErrorMessages.ForEach(messag => response.AddError(messag));
                _logger.LogError($"ModifyPermission - Error: {ex.Message}");
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

                var operacionDto = new OperacionDto
                {
                    Id = Guid.NewGuid(),
                    NameOperation = "get",
                };
                var serializedOperacion = JsonConvert.SerializeObject(operacionDto);
                await producer.ProduceAsync("operaciones-topic", new Message<string, string>
                {
                    Key = operacionDto.Id.ToString(),
                    Value = serializedOperacion
                });

                _logger.LogInformation($"GetPermissions - OK: {JsonConvert.SerializeObject(response)}");
            }
            catch (Exception ex)
            {
                response.AddError(ex.Message);
                _logger.LogError($"GetPermissions - Error: {ex.Message}");
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
