using SampleSmartLockApp.Application.Features.AccessPermissionHistories.Queries;

namespace SampleSmartLockApp.WebApi.Controllers;

[Authorize(Roles = "Administrator, Director")]
[ApiVersion("1.0")]
public class AccessPermissionHistoryController : BaseApiController
{
    [HttpGet(nameof(GetAll))]
    [ProducesResponseType(typeof(ApiPagedResponse<IEnumerable<GetAllAccessPermissionsHistoryViewModel>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll([FromQuery] GetAllAccessPermissionsHistoryRequestParameter requestParameter)
    {
        var query = new GetAllAccessPermissionsHistoryQuery(requestParameter.PageNumber, requestParameter.PageSize);
        return Ok(await Mediator.Send(query));
    }
}