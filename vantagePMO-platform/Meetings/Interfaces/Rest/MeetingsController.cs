using System.Net.Mime;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using vantagePMO_platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;
using vantagePMO_platform.Meetings.Application.CommandServices;
using vantagePMO_platform.Meetings.Application.QueryServices;
using vantagePMO_platform.Meetings.Domain.Model.Commands;
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

    [HttpGet("{id:int}")]
    [SwaggerOperation(Summary = "Get meeting by id", OperationId = "GetMeetingById")]
    [SwaggerResponse(StatusCodes.Status200OK, "Meeting found.", typeof(MeetingResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Meeting not found.")]
    public async Task<IActionResult> GetMeetingById(int id)
    {
        var meeting = await meetingQueryService.Handle(new GetMeetingByIdQuery(id));
        if (meeting is null)
            return NotFound();

        return Ok(MeetingResourceFromEntityAssembler.ToResourceFromEntity(meeting));
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
        return CreatedAtAction(nameof(GetMeetingById), new { id = meeting.Id }, createdResource);
    }

    [HttpPost("{meetingId:int}/agreements/{agreementId:int}/convert-to-task")]
    [SwaggerOperation(Summary = "Convert a meeting agreement into a collaboration task",
        OperationId = "ConvertMeetingAgreementToTask")]
    [SwaggerResponse(StatusCodes.Status200OK, "Agreement converted.", typeof(ConvertAgreementResultResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Meeting or agreement not found.")]
    public async Task<IActionResult> ConvertAgreementToTask(int meetingId, int agreementId)
    {
        var (meeting, taskId, taskRef) = await meetingCommandService.ConvertAgreementToTaskAsync(
            new ConvertMeetingAgreementCommand(meetingId, agreementId));

        if (meeting is null || taskId is null || taskRef is null)
            return NotFound();

        return Ok(new ConvertAgreementResultResource(
            MeetingResourceFromEntityAssembler.ToResourceFromEntity(meeting),
            taskId.Value,
            taskRef));
    }

    [HttpGet("{id:int}/minutes/export")]
    [SwaggerOperation(Summary = "Export meeting minutes", OperationId = "ExportMeetingMinutes")]
    [SwaggerResponse(StatusCodes.Status200OK, "Minutes exported.")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Meeting not found.")]
    public async Task<IActionResult> ExportMeetingMinutes(
        int id,
        [FromQuery] string format = "csv",
        [FromQuery] bool attendees = true,
        [FromQuery] bool agenda = true,
        [FromQuery] bool minutes = true,
        [FromQuery] bool agreements = true)
    {
        var meeting = await meetingQueryService.Handle(new GetMeetingByIdQuery(id));
        if (meeting is null)
            return NotFound();

        var csv = MeetingMinutesExportBuilder.BuildCsv(
            meeting, attendees, agenda, minutes, agreements);

        var normalizedFormat = format.Trim().ToLowerInvariant();
        var fileName = $"meeting-{id}-minutes.{normalizedFormat}";

        if (normalizedFormat is "json")
        {
            return Ok(new
            {
                meeting = MeetingResourceFromEntityAssembler.ToResourceFromEntity(meeting),
                exportOptions = new { attendees, agenda, minutes, agreements }
            });
        }

        return File(
            Encoding.UTF8.GetBytes(csv),
            normalizedFormat is "excel" or "xlsx"
                ? "application/vnd.ms-excel"
                : "text/csv",
            fileName);
    }
}
