using vantagePMO_platform.Profiles.Domain.Model.Queries;
using vantagePMO_platform.Profiles.Domain.Repositories;
using vantagePMO_platform.Profiles.Domain.Services;

namespace vantagePMO_platform.Profiles.Application.Internal.QueryServices;

public class EndorsementQueryService(IEndorsementRepository endorsementRepository) : IEndorsementQueryService
{
    public async Task<IReadOnlyList<Domain.Model.Aggregates.Endorsement>> Handle(
        GetEndorsementsByUserIdQuery query,
        CancellationToken cancellationToken = default)
    {
        return await endorsementRepository.FindByUserIdAsync(query.UserId, cancellationToken);
    }
}
