using SampleSmartLockApp.Application.Features.Offices.Commands.Create;
using SampleSmartLockApp.Application.Features.Offices.Commands.Delete;
using SampleSmartLockApp.Application.Features.Offices.Commands.Update;
using SampleSmartLockApp.Application.Features.Offices.Queries.GetAll;

namespace SampleSmartLockApp.WebApi.Controllers;

[Authorize]
[ApiVersion("1.0")]
public class OfficeController : BaseApiController
{
    [HttpGet(nameof(GetAll))]
    [ProducesResponseType(typeof(ApiPagedResponse<IEnumerable<GetAllOfficesViewModel>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll([FromQuery] GetAllOfficesRequestParameter requestParameter)
    {
        var query = new GetAllOfficesQuery(requestParameter.PageNumber, requestParameter.PageSize);
        return Ok(await Mediator.Send(query));
    }

    [HttpGet(nameof(GetById))]
    [ProducesResponseType(typeof(ApiResponse<GetOfficeByIdViewModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetById([FromQuery] GetOfficeByIdQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [Authorize(Roles = "Administrator")]
    [HttpPost(nameof(Create))]
    [ProducesResponseType(typeof(ApiResponse<Guid>), StatusCodes.Status201Created)]
    public async Task<IActionResult> Create(CreateOfficeCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [Authorize(Roles = "Administrator")]
    [HttpPut(nameof(Update))]
    [ProducesResponseType(typeof(ApiResponse<Guid>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Update(UpdateOfficeCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [Authorize(Roles = "Administrator")]
    [HttpDelete(nameof(Delete))]
    [ProducesResponseType(typeof(ApiResponse<Guid>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Delete(DeleteOfficeCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
}
