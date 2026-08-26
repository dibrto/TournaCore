namespace TournaCore.API.Common;

public static class ErrorCodes {
    // General 1000-1099
    public const int ValidationError = 1001;
    public const int InternalServerError = 1002;

    // User 1100-1199
    public const int InvalidCredentials = 1101;
    public const int EmailAlreadyExists = 1102;
    public const int UserNotFound = 1103;
    public const int UserRoleNotFound = 1104;
}