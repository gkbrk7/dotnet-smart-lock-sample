using SampleSmartLockApp.Application.Features.Users.Commands;
using SampleSmartLockApp.Application.Features.Users.GetAllUserRoles.Queries;
using SampleSmartLockApp.Application.Features.Users.Queries.GetAllUserRoles;
using SampleSmartLockApp.Application.Features.Users.Queries.GetAllUsers;

namespace SampleSmartLockApp.WebApi.Controllers;

[Authorize(Roles = "Administrator, Director, OfficeManager")]
[ApiVersion("1.0")]
public class UserController : BaseApiController
{
    // [TestAuthorize(20)]
    [HttpGet(nameof(GetAll))]
    [ProducesResponseType(typeof(ApiPagedResponse<IEnumerable<GetAllUsersViewModel>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll([FromQuery] GetAllUsersRequestParameter requestParameter)
    {
        var query = new GetAllUsersQuery(requestParameter.PageNumber, requestParameter.PageSize);
        return Ok(await Mediator.Send(query));
    }

    [HttpGet(nameof(GetAllRoles))]
    [ProducesResponseType(typeof(ApiResponse<GetAllUserRolesViewModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllRoles()
    {
        var query = new GetAllUserRolesQuery();
        return Ok(await Mediator.Send(query));
    }

    [HttpPost(nameof(AddUserToRole))]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status201Created)]
    public async Task<IActionResult> AddUserToRole(AddUserToRoleCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
}
