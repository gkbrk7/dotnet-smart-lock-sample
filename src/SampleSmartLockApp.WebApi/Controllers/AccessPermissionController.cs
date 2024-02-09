using SampleSmartLockApp.Application.Features.AccessPermissions.Commands.Create;
using SampleSmartLockApp.Application.Features.AccessPermissions.Commands.Delete;
using SampleSmartLockApp.Application.Features.AccessPermissions.Commands.Update;
using SampleSmartLockApp.Application.Features.AccessPermissions.Queries.GetAllAccessPermissions;
using SampleSmartLockApp.Application.Features.AccessPermissions.Queries.GetAllAccessPermissionsByUserId;

namespace SampleSmartLockApp.WebApi.Controllers;

[Authorize(Roles = "Administrator, Director, OfficeManager")]
[ApiVersion("1.0")]
public class AccessPermissionController : BaseApiController
{
    [HttpGet(nameof(GetAll))]
    [ProducesResponseType(typeof(ApiPagedResponse<IEnumerable<GetAllAccessPermissionsViewModel>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll([FromQuery] GetAllAccessPermissionsRequestParameter requestParameter)
    {
        var query = new GetAllAccessPermissionsQuery(requestParameter.PageNumber, requestParameter.PageSize);
        return Ok(await Mediator.Send(query));
    }

    [HttpGet(nameof(GetAllByUserId))]
    [ProducesResponseType(typeof(ApiPagedResponse<IEnumerable<GetAllAccessPermissionsByUserIdViewModel>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllByUserId([FromQuery] GetAllAccessPermissionsByUserIdRequestParameter requestParameter)
    {
        var query = new GetAllAccessPermissionsByUserIdQuery(requestParameter.UserId, requestParameter.PageNumber, requestParameter.PageSize);
        return Ok(await Mediator.Send(query));
    }

    [HttpPost(nameof(GrantLockAccessToUser))]
    [ProducesResponseType(typeof(ApiResponse<GrantLockAccessToUserViewModel>), StatusCodes.Status201Created)]
    public async Task<IActionResult> GrantLockAccessToUser(GrantLockAccessToUserCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut(nameof(UpdateLockAccessForUser))]
    [ProducesResponseType(typeof(ApiResponse<UpdateLockAccessForUserViewModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateLockAccessForUser(UpdateLockAccessForUserCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpDelete(nameof(RevokeLockAccessFromUser))]
    [ProducesResponseType(typeof(ApiResponse<RevokeLockAccessFromUserViewModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> RevokeLockAccessFromUser(RevokeLockAccessFromUserCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
}
