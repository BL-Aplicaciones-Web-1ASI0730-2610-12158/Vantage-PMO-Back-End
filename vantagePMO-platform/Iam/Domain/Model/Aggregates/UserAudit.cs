using vantagePMO_platform.Shared.Domain.Model.Entities;

namespace vantagePMO_platform.Iam.Domain.Model.Aggregates;

public partial class User : IAuditableEntity
{
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}