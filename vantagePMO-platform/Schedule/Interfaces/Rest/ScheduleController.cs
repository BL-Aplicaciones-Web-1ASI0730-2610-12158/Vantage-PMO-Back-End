using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using vantagePMO_platform.Schedule.Application.CommandServices;
using vantagePMO_platform.Schedule.Application.QueryServices;
using vantagePMO_platform.Schedule.Interfaces.Rest.Resources;
using vantagePMO_platform.Schedule.Interfaces.Rest.Transform;

namespace vantagePMO_platform.Schedule.Interfaces.Rest;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ScheduleController : ControllerBase
{
    private readonly IScheduleCommandService _commandService;
    private readonly IScheduleQueryService _queryService;

    public ScheduleController(IScheduleCommandService commandService, IScheduleQueryService queryService)
    {
        _commandService = commandService;
        _queryService = queryService;
    }

    [HttpPost]
    public async Task<ActionResult<ScheduleResource>> CreateSchedule(
        [FromBody] CreateScheduleResource resource,
        CancellationToken cancellationToken = default)
    {
        var command = CreateScheduleCommandFromResourceAssembler.ToCommand(resource);
        var id = await _commandService.CreateScheduleAsync(command, cancellationToken);

        var createdResource = await _queryService.GetScheduleByIdAsync(id, cancellationToken);
        return CreatedAtAction(nameof(GetScheduleById), new { id }, createdResource);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ScheduleResource>> GetScheduleById(
        int id,
        CancellationToken cancellationToken = default)
    {
        var resource = await _queryService.GetScheduleByIdAsync(id, cancellationToken);
        if (resource == null)
            return NotFound();

        return Ok(resource);
    }

    [HttpGet("user/{userId}")]
    public async Task<ActionResult<IEnumerable<ScheduleResource>>> GetSchedulesByUserId(
        int userId,
        CancellationToken cancellationToken = default)
    {
        var resources = await _queryService.GetSchedulesByUserIdAsync(userId, cancellationToken);
        return Ok(resources);
    }

    [HttpGet("user/{userId}/range")]
    public async Task<ActionResult<IEnumerable<ScheduleResource>>> GetSchedulesByDateRange(
        int userId,
        [FromQuery] DateTime startDate,
        [FromQuery] DateTime endDate,
        CancellationToken cancellationToken = default)
    {
        var resources = await _queryService.GetSchedulesByUserIdAndDateRangeAsync(userId, startDate, endDate,
            cancellationToken);
        return Ok(resources);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSchedule(
        int id,
        [FromBody] UpdateScheduleResource resource,
        CancellationToken cancellationToken = default)
    {
        var command = UpdateScheduleCommandFromResourceAssembler.ToCommand(id, resource);
        var success = await _commandService.UpdateScheduleAsync(command, cancellationToken);

        if (!success)
            return NotFound();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSchedule(
        int id,
        CancellationToken cancellationToken = default)
    {
        var success = await _commandService.DeleteScheduleAsync(id, cancellationToken);

        if (!success)
            return NotFound();

        return NoContent();
    }
}
