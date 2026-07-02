using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using vantagePMO_platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;
using vantagePMO_platform.Meetings.Application.CommandServices;
using vantagePMO_platform.Meetings.Application.QueryServices;
using vantagePMO_platform.Meetings.Domain.Model.Queries;
using vantagePMO_platform.Meetings.Interfaces.Rest.Resources;
using vantagePMO_platform.Meetings.Interfaces.Rest.Transform;

namespace vantagePMO_platform.Meetings.Interfaces.Rest;

[AllowAnonymous]
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Meeting management")]
public class MeetingsController(
    IMeetingCommandService meetingCommandService,
    IMeetingQueryService meetingQueryService) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation(Summary = "Get all meetings", OperationId = "GetAllMeetings")]
    [SwaggerResponse(StatusCodes.Status200OK, "Meetings found.", typeof(IEnumerable<MeetingResource>))]
    public async Task<IActionResult> GetAllMeetings()
    {
        var meetings = await meetingQueryService.Handle(new GetAllMeetingsQuery());
        return Ok(MeetingResourceFromEntityAssembler.ToResourcesFromEntities(meetings));
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Create a meeting", OperationId = "CreateMeeting")]
    [SwaggerResponse(StatusCodes.Status201Created, "Meeting created.", typeof(MeetingResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid meeting data.")]
    public async Task<IActionResult> CreateMeeting([FromBody] CreateMeetingResource resource)
    {
        var command = CreateMeetingCommandFromResourceAssembler.ToCommandFromResource(resource);
        var meeting = await meetingCommandService.Handle(command);

        if (meeting is null)
            return BadRequest("Could not create meeting.");

        var createdResource = MeetingResourceFromEntityAssembler.ToResourceFromEntity(meeting);
        return CreatedAtAction(nameof(GetAllMeetings), new { id = meeting.Id }, createdResource);
    }
}
