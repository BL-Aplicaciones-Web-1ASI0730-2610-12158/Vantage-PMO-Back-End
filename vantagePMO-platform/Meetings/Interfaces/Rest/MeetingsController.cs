using Microsoft.AspNetCore.Mvc;
using vantagePMO_platform.Meetings.Application.CommandServices;
using vantagePMO_platform.Meetings.Application.QueryServices;
using vantagePMO_platform.Meetings.Domain.Model.Queries;
using vantagePMO_platform.Meetings.Interfaces.Rest.Resources;
using vantagePMO_platform.Meetings.Interfaces.Rest.Transform;

namespace vantagePMO_platform.Meetings.Interfaces.Rest;

/// <summary>
/// Expone los endpoints REST de Meetings consumidos por el front-end (Vue):
/// GET  /api/v1/meetings  -> getAllMeetings
/// POST /api/v1/meetings  -> createMeeting(meeting)
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
public class MeetingsController(
    IMeetingCommandService meetingCommandService,
    IMeetingQueryService meetingQueryService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllMeetings()
    {
        var meetings = await meetingQueryService.Handle(new GetAllMeetingsQuery());
        var resources = meetings.Select(MeetingResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    [HttpPost]
    public async Task<IActionResult> CreateMeeting([FromBody] CreateMeetingResource resource)
    {
        var command = CreateMeetingCommandFromResourceAssembler.ToCommandFromResource(resource);
        var meeting = await meetingCommandService.Handle(command);

        if (meeting is null)
            return BadRequest("No se pudo crear la reunión.");

        var createdResource = MeetingResourceFromEntityAssembler.ToResourceFromEntity(meeting);
        return CreatedAtAction(nameof(GetAllMeetings), new { id = meeting.Id }, createdResource);
    }
}
