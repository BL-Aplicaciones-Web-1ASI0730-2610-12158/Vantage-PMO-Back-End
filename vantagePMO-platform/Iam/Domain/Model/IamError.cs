namespace vantagePMO_platform.Iam.Domain.Model;

public enum IamError
{
    None,
    UserNotFound,
    UsernameAlreadyTaken,
    EmailAlreadyRegistered,
    InvalidProfileData,
    InvalidDateOfBirth,
    PasswordMismatch,
    InvalidCredentials,
    OperationCancelled,
    DatabaseError,
    InternalServerError,
    ExternalServiceError
}