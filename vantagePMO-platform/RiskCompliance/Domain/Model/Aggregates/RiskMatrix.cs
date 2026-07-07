using vantagePMO_platform.RiskCompliance.Domain.Model.ValueObjects;

namespace vantagePMO_platform.RiskCompliance.Domain.Model.Aggregates;

public class RiskMatrix
{
    protected RiskMatrix()
    {
        Segment = string.Empty;
        Environment = string.Empty;
        HeatmapCells = new List<HeatmapCell>();
    }

    public RiskMatrix(string segment, string environment, IEnumerable<HeatmapCell> heatmapCells) : this()
    {
        Segment = segment;
        Environment = environment;
        HeatmapCells = heatmapCells.ToList();
    }

    public int Id { get; private set; }
    public string Segment { get; private set; }
    public string Environment { get; private set; }
    public List<HeatmapCell> HeatmapCells { get; private set; }
}
