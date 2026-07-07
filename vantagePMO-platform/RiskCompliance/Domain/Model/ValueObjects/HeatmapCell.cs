namespace vantagePMO_platform.RiskCompliance.Domain.Model.ValueObjects;

public record HeatmapCell(string Impact, string Likelihood, int Value, string Level);
