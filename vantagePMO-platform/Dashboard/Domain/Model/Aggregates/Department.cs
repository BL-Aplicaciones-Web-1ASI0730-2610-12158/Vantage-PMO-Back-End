namespace vantagePMO_platform.Dashboard.Domain.Model.Aggregates;

/// <summary>
///     Department capacity metric shown on the home dashboard.
/// </summary>
public class Department
{
    protected Department()
    {
        Name = string.Empty;
    }

    public Department(string name, int percent)
    {
        Name = name;
        Percent = percent;
    }

    public int Id { get; private set; }
    public string Name { get; private set; }
    public int Percent { get; private set; }
}
