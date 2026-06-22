namespace vantagePMO_platform.Analytics.Domain.Model.ValueObjects;

public record PortfolioRoi(int Percentage, string EfficiencyLabel, long Target, long Projected);
