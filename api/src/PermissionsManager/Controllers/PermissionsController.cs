using Microsoft.AspNetCore.Mvc;
using PermissionsManager.Application.Permissions.Commands;
using PermissionsManager.Application.Permissions.Dtos;
using PermissionsManager.Application.Permissions.Queries;

namespace PermissionsManager.Controllers
{
    public class PermissionsController : ControllerBase
    {
        [HttpGet]
        public Task<List.PermissionsEnvelope> List([FromQuery] List.Query request)
        {
            return Mediator.Send(request);
        }

        [HttpGet("{id}")]
        public async Task<PermissionDto?> Details(int id, [FromQuery] Details.Query request)
        {
            request.Id = id;

            var result = await Mediator.Send(request);
            if (result == null) Response.StatusCode = StatusCodes.Status404NotFound;

            return result;
        }

        [HttpPost]
        public Task<PermissionDto> Create(Create.Command request)
        {
            return Mediator.Send(request);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Update.Command request)
        {
            if (id < 1) throw new BadHttpRequestException("Invalid entity id", 400);

            request.Id = id;

            var result = await Mediator.Send(request);
            if (result == null) Response.StatusCode = StatusCodes.Status404NotFound;

            return NoContent();
        }
    }
}
