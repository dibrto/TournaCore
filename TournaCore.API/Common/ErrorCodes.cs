namespace TournaCore.API.Common;
public record AppError (
    int HttpCode,
    int ErrorCode,
    string ErrorMessage
);

public static class ErrorCodes {
    // General 1000-1099
    public static readonly AppError ValidationError = new(422, 1001, "Validation error");
    public const int InternalServerError = 1002;

    // User 1100-1199
    public static readonly AppError InvalidCredentials = new(401, 1101, "Invalid credentials");
    public static readonly AppError EmailAlreadyExists = new(409, 1102, "Email already exists");
    public static readonly AppError UserNotFound = new(404, 1103, "User doesn't exist");
    public static readonly AppError UserRoleNotFound = new(404, 1104, "User role doesn't exist");
    public static readonly AppError CannotModifyAdmin = new(403, 1105, "Admin role cannot be changed");
    public static readonly AppError CannotAssignAdminRole = new(403, 1106, "Admin role cannot be assigned");

    // Tournament 2000 - 2999
    public static readonly AppError TournamentNotFound = new(404, 2001, "Tournament doens't exist");
    public static readonly AppError NotTournamentOwner = new(401, 2002, "You are not allowed to modify this tournament");
}