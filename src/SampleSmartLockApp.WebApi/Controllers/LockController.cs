using SampleSmartLockApp.Application.Features.Locks.Commands.Create;
using SampleSmartLockApp.Application.Features.Locks.Commands.Delete;
using SampleSmartLockApp.Application.Features.Locks.Commands.Update;
using SampleSmartLockApp.Application.Features.Locks.Queries.GetAll;
using SampleSmartLockApp.Application.Features.Locks.Queries.OpenLock;

namespace SampleSmartLockApp.WebApi.Controllers;

[Authorize]
[ApiVersion("1.0")]
public class LockController : BaseApiController
{
    [HttpGet(nameof(GetAll))]
    [ProducesResponseType(typeof(ApiPagedResponse<IEnumerable<GetAllLocksViewModel>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll([FromQuery] GetAllLocksRequestParameter requestParameter)
    {
        var query = new GetAllLocksQuery(requestParameter.PageNumber, requestParameter.PageSize);
        return Ok(await Mediator.Send(query));
    }

    [HttpGet(nameof(OpenLock))]
    [ProducesResponseType(typeof(ApiPagedResponse<string>), StatusCodes.Status200OK)]
    public async Task<IActionResult> OpenLock(Guid lockId)
    {
        var query = new OpenLockByIdQuery(lockId);
        return Ok(await Mediator.Send(query));
    }

    [HttpGet(nameof(GetById))]
    [ProducesResponseType(typeof(ApiResponse<GetLockByIdViewModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var query = new GetLockByIdQuery(id);
        return Ok(await Mediator.Send(query));
    }

    [Authorize(Roles = "Administrator")]
    [HttpPost(nameof(Create))]
    [ProducesResponseType(typeof(ApiResponse<Guid>), StatusCodes.Status201Created)]
    public async Task<IActionResult> Create(CreateLockCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [Authorize(Roles = "Administrator")]
    [HttpPut(nameof(Update))]
    [ProducesResponseType(typeof(ApiResponse<Guid>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Update(UpdateLockCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [Authorize(Roles = "Administrator")]
    [HttpDelete(nameof(Delete))]
    [ProducesResponseType(typeof(ApiResponse<Guid>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Delete(DeleteLockCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
}
