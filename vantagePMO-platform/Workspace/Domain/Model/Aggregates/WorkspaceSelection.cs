namespace vantagePMO_platform.Workspace.Domain.Model.Aggregates;

/// <summary>
///     Stores the workspace segment selected by a user during sign-up.
/// </summary>
public class WorkspaceSelection
{
    protected WorkspaceSelection()
    {
        SelectedRole = string.Empty;
    }

    public WorkspaceSelection(int userId, string selectedRole)
    {
        if (userId <= 0)
            throw new ArgumentException("User id must be greater than zero.", nameof(userId));

        if (string.IsNullOrWhiteSpace(selectedRole))
            throw new ArgumentException("Selected role is required.", nameof(selectedRole));

        UserId = userId;
        SelectedRole = selectedRole.Trim();
    }

    public int Id { get; private set; }

    public int UserId { get; private set; }

    public string SelectedRole { get; private set; }

    public void UpdateRole(string selectedRole)
    {
        if (string.IsNullOrWhiteSpace(selectedRole))
            throw new ArgumentException("Selected role is required.", nameof(selectedRole));

        SelectedRole = selectedRole.Trim();
    }
}
