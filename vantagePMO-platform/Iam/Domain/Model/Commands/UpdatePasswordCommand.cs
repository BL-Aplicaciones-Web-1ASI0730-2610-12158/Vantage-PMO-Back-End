namespace vantagePMO_platform.Iam.Domain.Model.Commands;

public record UpdatePasswordCommand(int UserId, string Password);
