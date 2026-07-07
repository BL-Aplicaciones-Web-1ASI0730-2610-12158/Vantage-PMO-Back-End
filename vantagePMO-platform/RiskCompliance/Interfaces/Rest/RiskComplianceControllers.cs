using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using vantagePMO_platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;
using vantagePMO_platform.RiskCompliance.Application.QueryServices;
using vantagePMO_platform.RiskCompliance.Domain.Model.Queries;
using vantagePMO_platform.RiskCompliance.Interfaces.Rest.Resources;
using vantagePMO_platform.RiskCompliance.Interfaces.Rest.Transform;

namespace vantagePMO_platform.RiskCompliance.Interfaces.Rest;

[AllowAnonymous]
[ApiController]
[Route("api/v1/risks")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Risk register")]
public class RisksController(IRiskComplianceQueryService riskComplianceQueryService) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation(Summary = "Get all risks", OperationId = "GetAllRisks")]
    [SwaggerResponse(StatusCodes.Status200OK, "Risks found.", typeof(IEnumerable<RiskResource>))]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var risks = await riskComplianceQueryService.Handle(new GetAllRisksQuery(), cancellationToken);
        return Ok(RiskComplianceResourceFromEntityAssembler.ToResources(risks));
    }
}

[AllowAnonymous]
[ApiController]
[Route("api/v1/risk-matrix")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Risk matrix")]
public class RiskMatrixController(IRiskComplianceQueryService riskComplianceQueryService) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation(Summary = "Get risk matrices", OperationId = "GetAllRiskMatrices")]
    [SwaggerResponse(StatusCodes.Status200OK, "Risk matrices found.", typeof(IEnumerable<RiskMatrixResource>))]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var matrices = await riskComplianceQueryService.Handle(new GetAllRiskMatricesQuery(), cancellationToken);
        return Ok(RiskComplianceResourceFromEntityAssembler.ToResources(matrices));
    }
}

[AllowAnonymous]
[ApiController]
[Route("api/v1/compliance-metrics")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Compliance metrics")]
public class ComplianceMetricsController(IRiskComplianceQueryService riskComplianceQueryService) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation(Summary = "Get compliance metrics", OperationId = "GetAllComplianceMetrics")]
    [SwaggerResponse(StatusCodes.Status200OK, "Compliance metrics found.", typeof(IEnumerable<ComplianceMetricsResource>))]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var metrics = await riskComplianceQueryService.Handle(new GetAllComplianceMetricsQuery(), cancellationToken);
        return Ok(RiskComplianceResourceFromEntityAssembler.ToResources(metrics));
    }
}
