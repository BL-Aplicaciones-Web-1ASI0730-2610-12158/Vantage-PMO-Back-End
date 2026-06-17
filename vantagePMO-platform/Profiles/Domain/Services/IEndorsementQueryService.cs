using vantagePMO_platform.Profiles.Domain.Model.Aggregates;
using vantagePMO_platform.Profiles.Domain.Model.Queries;

namespace vantagePMO_platform.Profiles.Domain.Services;

public interface IEndorsementQueryService
{
    Task<IReadOnlyList<Endorsement>> Handle(GetEndorsementsByUserIdQuery query, CancellationToken cancellationToken = default);
}
