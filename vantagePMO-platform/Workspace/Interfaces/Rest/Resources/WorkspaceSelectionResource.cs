namespace vantagePMO_platform.Workspace.Interfaces.Rest.Resources;

public record WorkspaceSelectionResource(int Id, int UserId, string SelectedRole);

public record CreateWorkspaceSelectionResource(int UserId, string SelectedRole);
