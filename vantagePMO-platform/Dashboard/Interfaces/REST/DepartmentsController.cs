using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using vantagePMO_platform.Dashboard.Domain.Model.Queries;
using vantagePMO_platform.Dashboard.Domain.Services;
using vantagePMO_platform.Dashboard.Interfaces.REST.Resources;
using vantagePMO_platform.Dashboard.Interfaces.REST.Transform;
using vantagePMO_platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;

namespace vantagePMO_platform.Dashboard.Interfaces.REST;

[AllowAnonymous]
[ApiController]
[Route("api/v1/departments")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Home dashboard departments")]
public class DepartmentsController(IDepartmentQueryService departmentQueryService) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation(Summary = "Get all departments", OperationId = "GetAllDepartments")]
    [SwaggerResponse(StatusCodes.Status200OK, "Departments found.", typeof(IEnumerable<DepartmentResource>))]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var departments = await departmentQueryService.Handle(new GetAllDepartmentsQuery(), cancellationToken);
        return Ok(DepartmentResourceFromEntityAssembler.ToResourcesFromEntities(departments));
    }
}
