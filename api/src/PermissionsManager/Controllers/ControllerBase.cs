using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace PermissionsManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class ControllerBase : Microsoft.AspNetCore.Mvc.ControllerBase
    {
        private IMediator? _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<IMediator>();
    }
}
