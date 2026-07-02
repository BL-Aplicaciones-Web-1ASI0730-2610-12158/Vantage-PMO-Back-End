namespace vantagePMO_platform.TaskCollaboration.Domain.Model.Aggregates;

public class Board
{
    protected Board()
    {
        Name = string.Empty;
        Description = string.Empty;
    }

    public Board(string name, int projectsActive, string description)
    {
        Name = name;
        ProjectsActive = projectsActive;
        Description = description;
    }

    public int Id { get; private set; }
    public string Name { get; private set; }
    public int ProjectsActive { get; private set; }
    public string Description { get; private set; }
}
